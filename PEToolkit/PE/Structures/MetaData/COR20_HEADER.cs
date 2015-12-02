using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEToolkit.PE.Structures.MetaData
{
    [StructLayout(LayoutKind.Sequential)]
    public struct COR20_HEADER
    {
        public uint cb;
        public ushort MajorRuntimeVersion;
        public ushort MinorRuntimeVersion;
        public uint MetaDataRva;
        public uint MetaDataSize;
        public uint Flags;
        public uint EntryPointToken;
        public uint ResourcesRva;
        public uint ResourcesSize;
        public uint StrongNameSignatureRva;
        public uint StrongNameSignatureSize;
        public uint CodeManagerTableRva;
        public uint CodeManagerTableSize;
        public uint VTableFixupsRva;
        public uint VTableFixupsSize;
        public uint ExportAddressTableJumpsRva;
        public uint ExportAddressTableJumpsSize;
        public uint ManagedNativeHeaderRva;
        public uint ManagedNativeHeaderSize;
    }
}
