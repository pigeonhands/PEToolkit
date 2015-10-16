using PEViewer.PE.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEViewer.Forms
{
    public partial class formGenerateStructure : Form
    {
        Type[] PossableStructures = null;
        public formGenerateStructure()
        {
            InitializeComponent();

            PossableStructures = new Type[]
            {
                typeof(IMAGE_DOS_HEADER),
                typeof(IMAGE_FILE_HEADER),
                typeof(IMAGE_OPTIONAL_HEADER32),
                typeof(IMAGE_DATA_DIRECTORIES),
                typeof(IMAGE_SECTION_HEADER)
            };

            foreach(Type t in PossableStructures)
            {
                cbStructure.Items.Add(t.Name);
            }
            cbStructure.SelectedIndex = 0;
            SelectStruct(PossableStructures[0]);
        }

        void SelectStruct(Type t)
        {
            clbStructureSelect.Items.Clear();
            foreach(FieldInfo f in t.GetFields())
            {
                clbStructureSelect.Items.Add(f.Name, false);
            }
        }

        private void formGenerateStructure_Load(object sender, EventArgs e)
        {

        }

        private void rbCustom_CheckedChanged(object sender, EventArgs e)
        {
            clbStructureSelect.Enabled = rbCustom.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rbComplete.Checked)
            {
                using (formBuildStructure fbs = new formBuildStructure(PossableStructures[cbStructure.SelectedIndex]))
                {
                    fbs.ShowDialog();
                }
            }
            else
            {
                string[] fields = new string[clbStructureSelect.CheckedItems.Count];
                for(int i = 0; i < fields.Length; i++)
                {
                    fields[i] = clbStructureSelect.CheckedItems[i].ToString();
                }
                using (formBuildStructure fbs = new formBuildStructure(PossableStructures[cbStructure.SelectedIndex], fields))
                {
                    fbs.ShowDialog();
                }
            }
        }

        private void cbStructure_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectStruct(PossableStructures[cbStructure.SelectedIndex]);
        }
    }
}
