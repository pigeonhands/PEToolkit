using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEViewer.Controls
{
    public class ProcessListViewItem : ListViewItem
    {
        public ProcessListViewItem(Process p) : base(p.ProcessName)
        {
            SelectedProcess = p;
            SubItems.Add(p.Id.ToString());
            SubItems.Add(p.MainWindowTitle);
        }

        public Process SelectedProcess { get; set; }
    }
}
