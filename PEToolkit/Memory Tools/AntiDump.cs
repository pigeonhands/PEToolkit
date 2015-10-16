using PEViewer.PE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEToolkit.Memory_Tools
{
    public class AntiDump
    {
        public static void Apply()
        {
            PEInfomation info = PELoader.DisectSelf();
            uint oldProt = 0;

            VirtualProtect(info.ModuleBaseAddress, PEInfomation.SizeOfDosHeader, 0x4, out oldProt);
            ZeroMemory(info.ModuleBaseAddress, PEInfomation.SizeOfDosHeader);
            
            IntPtr p = new IntPtr((uint)info.ModuleBaseAddress + info.Overview.FileHeaderPointer + PEInfomation.SizeOfFileHeader);
            VirtualProtect(p, PEInfomation.SizeOfOptionalHeader, 0x4, out oldProt);
            ZeroMemory(p, PEInfomation.SizeOfOptionalHeader);
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int VirtualProtect(IntPtr address, int size, uint flNewProtect, out uint old);
        [DllImport("Kernel32.dll")]
        private static extern void ZeroMemory(IntPtr dest, int size);

    }
}
