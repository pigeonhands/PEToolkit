using PEViewer.PE;
using PEViewer.PE.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEViewer.Forms
{
    public partial class formDumpModule : Form
    {
        public formDumpModule()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(tbDumpLocation.Text == string.Empty)
            {
                MessageBox.Show("Select a dump location.");
                return;
            }
            string path = tbDumpLocation.Text;
            int pId = 0;
            ProcessModule module = null;
            using (formLoadProcess fProc = new formLoadProcess())
            {
                if (fProc.ShowDialog() != DialogResult.OK)
                    return;
                pId = fProc.SelectedProcessID;
                module = fProc.SelectedModule;
            }

            PEInfomation procPE = PELoader.Load(pId, module);

            byte[] buffer = new byte[procPE.Overview.SizeOfImage];

            IntPtr procHandle = procPE.GetHandle();
            ReadProcessMemory(procHandle, module.BaseAddress, buffer, Convert.ToInt32(procPE.Overview.SizeOfHeaders), 0);

            foreach (IMAGE_SECTION_HEADER section in procPE.Sections)
            {
                if (section.SizeOfRawData == 0)
                    continue;

                byte[] sData = new byte[section.SizeOfRawData];
                ReadProcessMemory(procHandle, new IntPtr(procPE.Overview.ImageBase + section.VirtualAddress), sData, sData.Length, 0);

                Buffer.BlockCopy(sData, 0, buffer, Convert.ToInt32(section.PointerToRawData), sData.Length);
            }

            File.WriteAllBytes(path, buffer);
            procPE.CloseHandle();
            MessageBox.Show("Done");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Executable|*.exe|Library|*.dll";
                if (sfd.ShowDialog() != DialogResult.OK)
                    return;
                tbDumpLocation.Text = sfd.FileName;
            }

            
        }

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr handle, IntPtr address, byte[] buffer, int blen, int w0);
    }
}
