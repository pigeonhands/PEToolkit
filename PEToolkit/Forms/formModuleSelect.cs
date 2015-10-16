using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEViewer.Forms
{
    public partial class formModuleSelect : Form
    {
        public ProcessModule SelectedProcessModule { get; private set; }
        ProcessModule[] Modules = null;
        Process targetProcess = null;
        public formModuleSelect(Process p)
        {
            targetProcess = p;
            InitializeComponent();
        }

        private void formModuleSelect_Load(object sender, EventArgs e)
        {
            this.Text += string.Format(" (PID: {0})", targetProcess.Id);

            try
            {
                ProcessModuleCollection col = targetProcess.Modules;
                Modules = new ProcessModule[col.Count];
                for (int i = 0; i < Modules.Length; i++)
                {
                    Modules[i] = col[i];
                    cbModule.Items.Add(Modules[i].ModuleName);
                }
                if (cbModule.Items.Count > 0)
                    cbModule.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("32bit processes only");
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedProcessModule = Modules[cbModule.SelectedIndex];
            this.DialogResult = DialogResult.OK;
        }
    }
}
