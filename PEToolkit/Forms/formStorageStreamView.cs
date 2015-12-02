using PEViewer.PE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEToolkit.Forms
{
    public partial class formStorageStreamView : Form
    {
        public formStorageStreamView(PEInfomation LoadedPE)
        {
            InitializeComponent();

            foreach (var section in LoadedPE.NetStructures.StorageStreamHeaders)
            {
                ListViewItem i = new ListViewItem(new string(section.rcName));
                i.SubItems.Add(string.Format("0x{0:x2}", section.iOffset));
                i.SubItems.Add(string.Format("0x{0:x2}", section.iSize));
                lvSections.Items.Add(i);
            }
        }

        private void formStorageStreamView_Load(object sender, EventArgs e)
        {

        }
    }
}
