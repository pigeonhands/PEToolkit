using PEViewer.PE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEToolkit.Controls
{
    public class ModuleListViewItem : ListViewItem
    {
        public IntPtr ProcessHandle { get; private set; }
        public IntPtr Handle { get; private set; }
        public string ModulePath { get; set; }
        public PEInfomation ModuleInfomation { get; private set; }

        public ModuleListViewItem(int procID, IntPtr _handle)
        {
            ModuleInfomation = PELoader.Load(procID, _handle);
            Handle = _handle;
            ProcessHandle = ModuleInfomation.GetProcessHandle();

            StringBuilder sb = new StringBuilder(255);

            GetModuleFileNameEx(ProcessHandle, Handle, sb, 255);
            ModulePath = sb.ToString();

            Text = Path.GetFileName(ModulePath);
            SubItems.Add(string.Format("0x{0:x2}", IntPtr.Size == 4 ? _handle.ToInt32() : _handle.ToInt64()));
           SubItems.Add(ModuleInfomation.Overview.SizeOfImage.ToString());
            SubItems.Add(ModulePath);
        }

        [DllImport("psapi.dll")]
        static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpBaseName, int nSize);
    }
}
