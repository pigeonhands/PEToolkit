using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEToolkit.PE.Structures.MetaData
{
    [StructLayout(LayoutKind.Sequential)]
    public struct STORAGE_STREAM_HEADER
    {
        public uint iOffset;
        public uint iSize;
        [MarshalAs(UnmanagedType.ByValArray)]
        public char[] rcName;
    }
}
