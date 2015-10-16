using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEViewer.Controls
{
    public class ArrayListViewItem : ListViewItem
    {
        public ArrayListViewItem(string name, Array a):base(name)
        {
            ArrayValue = a;
        }
        public ArrayListViewItem(Array a) : base()
        {
            ArrayValue = a;
        }
        public Array ArrayValue { get; set; }
    }
}
