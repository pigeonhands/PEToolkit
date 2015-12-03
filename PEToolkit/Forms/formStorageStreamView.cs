using PEToolkit.Controls;
using PEViewer.PE;
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
    public partial class formStorageStreamView : Form
    {
        PEInfomation LoadedPE;
        public formStorageStreamView(PEInfomation _pe)
        {
            InitializeComponent();
            LoadedPE = _pe;
            foreach (var section in LoadedPE.NetStructures.StorageStreamHeaders)
            {
                lvSections.Items.Add(new NetStorageListViewItem(section));
            }
        }

        private void formStorageStreamView_Load(object sender, EventArgs e)
        {

        }

        private void dumpStorageStreamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvSections.SelectedItems.Count < 1)
                return;
            string dumpPath = string.Empty;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog() != DialogResult.OK)
                    return;
                dumpPath = sfd.FileName;
            }
            NetStorageListViewItem i = (NetStorageListViewItem)lvSections.SelectedItems[0];

            byte[] stream = LoadedPE.ReadStorageStream(i.Header);
            try
            {
                File.WriteAllBytes(dumpPath, stream);
                MessageBox.Show("Done.");
            }
            catch
            {
                MessageBox.Show("Failed.");
            }

        }

       

        private void lvSections_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvSections.SelectedItems.Count < 1)
                return;
            NetStorageListViewItem i = (NetStorageListViewItem)lvSections.SelectedItems[0];
            using (formViewStorageStream vs = new formViewStorageStream(LoadedPE, i.Header))
            {
                vs.ShowDialog();
            }
        }


        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr process, IntPtr baseAddress, byte[] buffer, int bufferSize, int bytesRead);
    }
}
