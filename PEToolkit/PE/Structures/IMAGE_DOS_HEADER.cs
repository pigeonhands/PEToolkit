using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEViewer.PE.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_DOS_HEADER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public char[] e_magic;
        public short e_cblp;
        public short e_cp;
        public short e_crlc;
        public short e_cparhdr;
        public short e_minalloc;
        public short e_maxalloc;
        public short e_ss;
        public short e_sp;
        public short e_csum;
        public short e_ip;
        public short e_cs;
        public short e_lfarlc;
        public short e_ovno;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public short[] e_res1;
        public short e_oemid;
        public short e_oeminfo;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public short[] e_res2; 
        public uint e_lfanew;
    }
}
