using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEToolkit.PE.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_BASIC_INFORMATION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
        public IntPtr BaseAddress;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible")]
        public IntPtr AllocationBase;
        public uint AllocationProtect;
        public uint RegionSize;
        public uint State;
        public uint Protect;
        public uint Type;
    }
}
