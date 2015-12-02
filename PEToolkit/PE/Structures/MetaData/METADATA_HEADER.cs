using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEToolkit.PE.Structures.MetaData
{
    [StructLayout(LayoutKind.Sequential)]
    public struct METADATA_HEADER
    {
        public uint Signature;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public uint Reserved;
        public uint VersionLength;
        public char[] VersionString;
        public ushort Flags;
        public ushort NumberOfStreams;
    }
}
