using PEViewer.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEViewer.Forms
{
    public partial class formLoadProcess : Form
    {
        public int SelectedProcessID { get; private set; }
        public ProcessModule SelectedModule { get; private set; }
        public string ProcessName { get; set; }
        public bool SelectModule { get; private set; }

        public formLoadProcess()
        {
            InitializeComponent();
            SelectModule = true;
        }
        public formLoadProcess(bool selectModule)
        {
            InitializeComponent();
            SelectModule = selectModule;
        }

        private void formLoadProcess_Load(object sender, EventArgs e)
        {
            PopulateList();
        }
        void PopulateList()
        {
            lvProcessList.Items.Clear();

            Process[] procList = Process.GetProcesses();
            foreach(Process p in procList)
            {
                lvProcessList.Items.Add(new ProcessListViewItem(p));
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopulateList();
        }

        private void lvProcessList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvProcessList.SelectedItems.Count > 0)
            {
                ProcessListViewItem i = (ProcessListViewItem)lvProcessList.SelectedItems[0];
                nudProcessID.Value = i.SelectedProcess.Id;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process p = Process.GetProcessById((int)nudProcessID.Value);
            SelectedProcessID = p.Id;
            ProcessName = p.ProcessName;

            if (SelectModule)
            {
                using (formModuleSelect mod = new formModuleSelect(p))
                {
                    if (mod.ShowDialog() != DialogResult.OK)
                        return;
                    SelectedModule = mod.SelectedProcessModule;
                }
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
