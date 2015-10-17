using PEToolkit.Controls;
using PEViewer.Memory_Tools;
using PEViewer.PE;
using PEViewer.PE.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEToolkit.Forms
{
    public partial class formModuleView : Form
    {
        int ProcessID;

        public object DllInject { get; private set; }

        public formModuleView(int pID, string name)
        {
            InitializeComponent();
            ProcessID = pID;
            this.Text += string.Format(" ({0})", name);
        }

        private void formModuleView_Load(object sender, EventArgs e)
        {
            PopulateList();
        }

        void PopulateList()
        {
            lvModules.Items.Clear();
            IntPtr pHandle = PELoader.OpenProcessHandle(ProcessID);
            if (pHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to load process");
                this.DialogResult = DialogResult.OK;
                return;
            }

            int size = 0;
            if (!EnumProcessModulesEx(pHandle, null, 0, out size, 0x01))
            {
                MessageBox.Show("Failed to get module count");
                this.DialogResult = DialogResult.OK;
                return;
            }

            int ModuleCount = size / Marshal.SizeOf(typeof(IntPtr));
            IntPtr[] modules = new IntPtr[ModuleCount];

            if (!EnumProcessModulesEx(pHandle, modules, size, out size, 0x01))
            {
                MessageBox.Show("Failed to get modules");
                this.DialogResult = DialogResult.OK;
                return;
            }

            foreach (IntPtr m in modules)
            {
                lvModules.Items.Add(new ModuleListViewItem(ProcessID, m));
            }

            PELoader.CloseProcessHandle(pHandle);
        }

        

        private void dumpSelectedModulesToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (lvModules.SelectedItems.Count < 1)
                return;

            if (lvModules.SelectedItems.Count == 1)
            {
                ModuleListViewItem item = (ModuleListViewItem)lvModules.SelectedItems[0];
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = string.Format("Module dump|*{0}", Path.GetExtension(item.ModulePath));
                    if(sfd.ShowDialog() == DialogResult.OK)
                    {
                        DumpModule(item.ModuleInfomation, sfd.FileName);
                        MessageBox.Show("Done.");
                    }
                }
            }
            else
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    if(fbd.ShowDialog() == DialogResult.OK)
                    {
                        foreach(ListViewItem i in lvModules.SelectedItems)
                        {
                            ModuleListViewItem item = (ModuleListViewItem)i;
                            DumpModule(item.ModuleInfomation, Path.Combine(fbd.SelectedPath, Path.GetFileName(item.ModulePath)));
                        }
                        MessageBox.Show("Done.");
                    }
                }
            }
        }

        void DumpModule(PEInfomation procPE, string path)
        {

            byte[] buffer = new byte[procPE.Overview.SizeOfImage];

            IntPtr procHandle = procPE.GetProcessHandle();
            ReadProcessMemory(procHandle, procPE.ModuleBaseAddress, buffer, Convert.ToInt32(procPE.Overview.SizeOfHeaders), 0);

            foreach (IMAGE_SECTION_HEADER section in procPE.Sections)
            {
                if (section.SizeOfRawData == 0)
                    continue;

                byte[] sData = new byte[section.SizeOfRawData];
                ReadProcessMemory(procHandle, new IntPtr(procPE.Overview.ImageBase + section.VirtualAddress), sData, sData.Length, 0);

                Buffer.BlockCopy(sData, 0, buffer, Convert.ToInt32(section.PointerToRawData), sData.Length);
            }

            File.WriteAllBytes(path, buffer);
            procPE.CloseProcessHandle();
        }

        [DllImport("psapi.dll")]
        private static extern bool EnumProcessModulesEx(IntPtr hProc, IntPtr[] lphModule, int size, out int sizeNeeded, uint flags);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr handle, IntPtr address, byte[] buffer, int blen, int w0);

        private void unloadSelectedModulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem i in lvModules.SelectedItems)
            {
                ModuleListViewItem item = (ModuleListViewItem)i;
                IntPtr pHandle = PELoader.OpenProcessHandle(ProcessID);
                DllInjector.UnloadDll(pHandle, item.ModuleInfomation.ModuleBaseAddress);
                PELoader.CloseProcessHandle(pHandle);
                PopulateList();
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopulateList();
        }
    }
}
