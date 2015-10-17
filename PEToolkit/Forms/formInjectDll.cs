using PEViewer.Memory_Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEViewer.Forms
{
    public partial class formInjectDll : Form
    {
        public formInjectDll()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Dll|*.dll";
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    tbDllPath.Text = ofd.FileName;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tbDllPath.Text == string.Empty)
                return;
            if(!File.Exists(tbDllPath.Text))
            {
                MessageBox.Show("Invalid File");
                return;
            }

            using (formLoadProcess proc = new formLoadProcess(false))
            {
                if(proc.ShowDialog() == DialogResult.OK)
                {
                    string message = string.Empty;
                    bool success = false;

                    IntPtr handle = DllInjector.Inject(proc.SelectedProcessID, tbDllPath.Text, out success, cbWaitForHandle.Checked);

                    if(success)
                    {
                        message = "Injected Successfully.";
                        if (handle != IntPtr.Zero)
                            message += string.Format("{0}Dll Handle: 0x{1:x2}", Environment.NewLine, handle.ToInt32());
                    }
                    else
                    {
                        message = "Failed to inejct dll.";
                    }

                    MessageBox.Show(message);
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void formInjectDll_Load(object sender, EventArgs e)
        {

        }
    }
}
