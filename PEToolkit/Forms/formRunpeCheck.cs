using PEViewer.PE;
using PEViewer.PE.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEViewer.Forms
{
    public partial class formRunpeCheck : Form
    {
        string WindowText = string.Empty;
        public formRunpeCheck()
        {
            InitializeComponent();
            WindowText = this.Text;
        }

        private void formRunpeCheck_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbProcessList.Items.Clear();
            lvFileList.Items.Clear();
            ProcessModule moduleToScan = null;
            int pid = 0;
            using (formLoadProcess procLoadForm = new formLoadProcess())
            {
                if (procLoadForm.ShowDialog() != DialogResult.OK)
                    return;
                this.Text = string.Format("{0} ({1})", WindowText, procLoadForm.ProcessName);
                moduleToScan = procLoadForm.SelectedModule;
                pid = procLoadForm.SelectedProcessID;
            }

            string modulePath = moduleToScan.FileName;
            PEInfomation procPE = PELoader.Load(pid, moduleToScan);
            PEInfomation filePE = PELoader.Load(modulePath);
            int unmachedValues = 0;

            unmachedValues += ScanType<IMAGE_FILE_HEADER>(procPE.FileHeader, filePE.FileHeader, "File Header");
            unmachedValues += ScanType<IMAGE_OPTIONAL_HEADER32>(procPE.OptionalHeader32, filePE.OptionalHeader32, "Optional Header");
            int sectionAmmount = Math.Min(Convert.ToInt32(procPE.Overview.NumberOfSections), Convert.ToInt32(filePE.Overview.NumberOfSections));

            for(int i = 0; i < sectionAmmount; i++)
            {
                unmachedValues += ScanType<IMAGE_SECTION_HEADER>(procPE.Sections[i], filePE.Sections[i], string.Format("Section {0}", i+1));
            }

            Color tColor = Color.Green;
            string warningText = "No RunPE Found (0 Unmached values)";

            if(unmachedValues == 1)
            {
                tColor = Color.DarkTurquoise;
                warningText = string.Format("Possable RunPe ({0} Unmaching values)", unmachedValues);
            }

            if (unmachedValues > 1)
            {
                tColor = Color.Red;
                warningText = string.Format("Possable RunPe ({0} Unmaching values)", unmachedValues);
            }

            lbRunpeStatus.Text = warningText;
            lbRunpeStatus.ForeColor = tColor;
        }

        int ScanType<T>(T procPE, T filePE, string str)
        {
            Type scanType = typeof(T);

            int TunmachedValues = 0;

            foreach (FieldInfo f in scanType.GetFields())
            {
                object oProc = f.GetValue(procPE);
                object oFile = f.GetValue(filePE);
                ListViewItem pI = new ListViewItem(f.Name);
                ListViewItem fI = new ListViewItem(f.Name);

                pI.SubItems.Add(str);
                fI.SubItems.Add(str);

                pI.SubItems.Add(oProc.ToString());
                fI.SubItems.Add(oFile.ToString());

                if(oProc.ToString() != oFile.ToString())
                {
                    pI.ForeColor = Color.Red;
                    fI.ForeColor = Color.Red;
                    TunmachedValues++;
                }
                

                lbProcessList.Items.Add(pI);
                lvFileList.Items.Add(fI);
            }
            return TunmachedValues;
        }
    }
}
