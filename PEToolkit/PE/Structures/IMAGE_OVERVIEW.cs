using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEViewer.PE.Structures
{
    public struct IMAGE_OVERVIEW
    {
        public uint FileHeaderPointer;
        public uint NumberOfSections;
        public uint AddressOfEntrypoint;
        public uint ImageBase;
        public uint SizeOfImage;
        public uint SizeOfHeaders;
    }
}
