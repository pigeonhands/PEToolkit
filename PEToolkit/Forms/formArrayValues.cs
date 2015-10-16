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
    public partial class formArrayValues : Form
    {
        public formArrayValues(string name, Array val)
        {
            InitializeComponent();
            this.Text = name;
            foreach(object o in val)
            {
                lvValues.Items.Add(new ListViewItem(o.ToString()));
            }
        }

        private void formArrayValues_Load(object sender, EventArgs e)
        {
            
        }
    }
}
