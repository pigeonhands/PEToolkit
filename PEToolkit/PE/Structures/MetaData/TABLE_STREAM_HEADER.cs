using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEToolkit.PE.Structures.MetaData
{
    public struct TABLE_STREAM_HEADER
    {
        public uint mulReserved;
        public byte m_major;
        public byte m_minor;
        public byte m_heaps;
        public byte m_rid;
        public ulong m_maskvalid;
        public ulong m_sorted;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=15)]
        public uint[] StreamLengths;
    }
}
