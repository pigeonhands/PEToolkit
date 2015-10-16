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

namespace PEViewer.Forms
{
    public partial class formSectionView : Form
    {
        PEInfomation LoadedPE = null;
        public formSectionView(PEInfomation info)
        {
            InitializeComponent();
            LoadedPE = info;

            foreach(var section in LoadedPE.Sections)
            {
                ListViewItem i = new ListViewItem(new string(section.Name));
                i.SubItems.Add(string.Format("0x{0:x2}", section.VirtualSize));
                i.SubItems.Add(string.Format("0x{0:x2}", section.VirtualAddress));
                i.SubItems.Add(string.Format("0x{0:x2}", section.SizeOfRawData));
                i.SubItems.Add(string.Format("0x{0:x2}", section.PointerToRawData));
                lvSections.Items.Add(i);
            }

        }

        private void formSectionView_Load(object sender, EventArgs e)
        {

        }
    }
}
