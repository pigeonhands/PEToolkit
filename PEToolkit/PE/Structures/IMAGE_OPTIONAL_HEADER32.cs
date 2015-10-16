using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEViewer.PE.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_OPTIONAL_HEADER32
    {
        //Standard Headers
        public ushort Magic;
        public byte MajorLinkedVersion;
        public byte MinorLinkedVersion;
        public uint SizeOfCode;
        public uint SizeOfInitilizedData;
        public uint SizeOfUninitilizedData;
        public uint AddressOfEntrypoint;
        public uint BaseOfCode;
        public uint BaseOfData;

        //Windows NT Headers

        public uint ImageBase;
        public uint SectionAllignment;
        public uint FileAlignment;
        public ushort MajorOperatingSystemVersion;
        public ushort MinorOperatingSystemVersion;
        public ushort MajorImageVersion;
        public ushort MinorImageVersion;
        public ushort MajorSubSystemVersion;
        public ushort MinorSubSystemVersion;
        public uint Win32VersionValue;
        public uint SizeOfImage;
        public uint SizeOfHeaders;
        public uint Checksum;
        public ushort Subsystem;
        public ushort DllCharacteristics;
        public uint SizeOfStacReserve;
        public uint SizeOfStackCommit;
        public uint SizeOfHeapReserve;
        public uint SizeOfHeapCommit;
        public uint LoaderFlags;
        public uint NumberOfRvaAndSizes;
    }


}
