using PEToolkit.Forms;
using PEToolkit.Memory_Tools;
using PEViewer.Controls;
using PEViewer.PE;
using PEViewer.PE.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEViewer.Forms
{
    public partial class mainWindow : Form
    {
        PEInfomation LoadedPE = null;
        string LoadedWindowTest = string.Empty;
        public mainWindow()
        {
            InitializeComponent();
            LoadedWindowTest = this.Text;
        }

        private void mainWindow_Load(object sender, EventArgs e)
        {

        }

        void PopulateInfo<Struct>(Struct sInfo, bool displayOffsets = true, bool useFileHeaderOffset = true, int uoffset = 0)
        {
            if (LoadedPE == null)
                return;

            tsNetStructures.Visible = LoadedPE.IsNet;

            lvInfo.Items.Clear();
            this.Text = string.Format("{0} ({1})", LoadedWindowTest, LoadedPE.PESource);
            Type t = sInfo.GetType();
            int offset = useFileHeaderOffset ? Convert.ToInt32(LoadedPE.Overview.FileHeaderPointer) : uoffset;

            foreach (FieldInfo f in t.GetFields())
            {
                ListViewItem i;
                if (f.FieldType == typeof(char[]))
                {
                    char[] value = (char[])f.GetValue(sInfo);
                    if (value == null)
                        value = new char[0];
                    i = new ListViewItem(f.Name);
                    i.SubItems.Add(new string(value));
                    if (displayOffsets)
                        i.SubItems.Add(string.Format("0x{0:x2}", offset));
                    else
                        i.SubItems.Add("");
                    i.SubItems.Add((value.Length * Marshal.SizeOf(typeof(char))).ToString());
                    i.SubItems.Add("String");
                    offset += value.Length;
                    lvInfo.Items.Add(i);
                    continue;
                }
                int fieldSize = 0;
                if(f.FieldType.IsArray)
                {
                    Array val = (Array)f.GetValue(sInfo);
                    i = new ArrayListViewItem(f.Name, val);
                    fieldSize = val.Length * Marshal.SizeOf(f.FieldType.GetElementType());
                }
                else
                {
                    i = new ListViewItem(f.Name);
                    fieldSize = Marshal.SizeOf(f.FieldType);
                }
                if(f.FieldType.IsArray)
                    i.SubItems.Add(string.Format("{0}[{1}]", f.FieldType.GetElementType().Name, ((Array)f.GetValue(sInfo)).Length));
                else
                    i.SubItems.Add(string.Format("0x{0:x2}", f.GetValue(sInfo)));
                if (displayOffsets)
                    i.SubItems.Add(string.Format("0x{0:x2}", offset));
                else
                    i.SubItems.Add("");
                i.SubItems.Add(fieldSize.ToString());
                i.SubItems.Add(f.FieldType.Name);

                lvInfo.Items.Add(i);
                
                offset += fieldSize;
            }
        }

        private void overviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedPE == null) return;
            lbCurrentSection.Text = "Overview";
            PopulateInfo(LoadedPE.Overview, false);
        }

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Executable|*.exe|Library|*.dll";
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadedPE = PELoader.Load(ofd.FileName);
                    LoadedPE.PESource = ofd.FileName;
                    lbCurrentSection.Text = "Overview";
                    PopulateInfo(LoadedPE.Overview, false);
                }
            }
        }

        private void dOSHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedPE == null) return;
            lbCurrentSection.Text = "DOS Header";
            PopulateInfo(LoadedPE.DosHeader, true, false);
        }

        private void imageHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedPE == null) return;
            lbCurrentSection.Text = "File Header";
            PopulateInfo(LoadedPE.FileHeader);
        }

        private void optionalPEHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedPE == null) return;
            lbCurrentSection.Text = "Optional PE Header";
            PopulateInfo(LoadedPE.OptionalHeader32);
        }

        private void dataDirectoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedPE == null) return;
            lbCurrentSection.Text = "Data Directories";
            PopulateInfo(LoadedPE.DataDirectories);
        }

        private void sectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lvInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(lvInfo.SelectedItems.Count > 0)
            {
                ListViewItem i = lvInfo.SelectedItems[0];

                Type t = i.GetType();
                if(t == typeof(ArrayListViewItem))
                {
                    using (formArrayValues av = new formArrayValues(i.Text, ((ArrayListViewItem)i).ArrayValue))
                    {
                        av.ShowDialog();
                    }
                }

            }
        }

        private void structuresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedPE == null)
                return;
            using (formSectionView sec = new formSectionView(LoadedPE))
            {
                sec.ShowDialog();
            }
        }

        private void processToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (formLoadProcess procLoadForm = new formLoadProcess())
            {
                if(procLoadForm.ShowDialog() == DialogResult.OK)
                {
                    LoadedPE = PELoader.Load(procLoadForm.SelectedProcessID, procLoadForm.SelectedModule);
                    LoadedPE.PESource = string.Format("Process: {0}", procLoadForm.ProcessName);
                    lbCurrentSection.Text = "Overview";
                    PopulateInfo(LoadedPE.Overview, false);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void generateStructuresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (formGenerateStructure str = new formGenerateStructure())
            {
                str.ShowDialog();
            }
        }

        private void checkForRunPEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (formRunpeCheck rpe = new formRunpeCheck())
            {
                rpe.ShowDialog();
            }
        }

        private void injectDllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (formInjectDll dll = new formInjectDll())
            {
                dll.ShowDialog();
            }
        }

        private void resourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedPE == null) return;

            IntPtr handle = LoadedPE.LoadModule();

            using (formNativeresources resources = new formNativeresources(handle))
            {
                resources.ShowDialog();
            }
            LoadedPE.UnloadModule();
        }

        private void unloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (formLoadProcess procLoadForm = new formLoadProcess(false))
            {
                if (procLoadForm.ShowDialog() == DialogResult.OK)
                {
                    using (formModuleView dlls = new formModuleView(procLoadForm.SelectedProcessID, procLoadForm.ProcessName))
                    {
                        if(dlls.ShowDialog() == DialogResult.Yes)
                        {
                            LoadedPE = dlls.LoadInfomation;
                            LoadedPE.PESource = string.Format("Process: {0}", procLoadForm.ProcessName);
                            lbCurrentSection.Text = "Overview";
                            PopulateInfo(LoadedPE.Overview, false);
                        }
                    }
                }
            }
        }

        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {

        }

        private void cOR20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedPE == null) return;
            lbCurrentSection.Text = "COR20 Header";
            PopulateInfo(LoadedPE.NetStructures.COR20Header, true, false, LoadedPE.NetStructures.NetOffsets.COR20RawAddress);
        }

        private void metaDataHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedPE == null) return;
            lbCurrentSection.Text = "MetaData Header";
            PopulateInfo(LoadedPE.NetStructures.MetaDataHeader, true, false, LoadedPE.NetStructures.NetOffsets.MetaDataRawAddress);
        }

        private void dataStreamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedPE != null && LoadedPE.IsNet)
            {
                using (formStorageStreamView sec = new formStorageStreamView(LoadedPE))
                {
                    sec.ShowDialog();
                }
            }
        }

        private void tableStreamHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadedPE == null) return;
            lbCurrentSection.Text = "Table Stream Header";
            PopulateInfo(LoadedPE.NetStructures.TableStreamHeader, true, false, LoadedPE.NetStructures.NetOffsets.RawAddressOfTableStreams);
        }
    }
}
