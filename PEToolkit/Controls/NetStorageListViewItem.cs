using PEToolkit.PE.Structures.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEToolkit.Controls
{
    public class NetStorageListViewItem : ListViewItem
    {
        public STORAGE_STREAM_HEADER Header;
        public NetStorageListViewItem(STORAGE_STREAM_HEADER _h) : base(new string(_h.rcName))
        {
            Header = _h;
            SubItems.Add(string.Format("0x{0:x2}", Header.iOffset));
            SubItems.Add(string.Format("0x{0:x2}", Header.iSize));
        }
    }
}
