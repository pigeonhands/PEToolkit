using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEToolkit.PE.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MODULE_INFO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
        public IntPtr lpBaseOfDll;
        public uint SizeOfImage;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
        public IntPtr EntryPoint;
    }
}
