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
            PEInfomation selfPE = PELoader.DisectSelf();
            uint oldProt = 0;
            uint buff = 0;

            MEMORY_BASIC_INFORMATION meb = new MEMORY_BASIC_INFORMATION();
            int sizeofMeb = Marshal.SizeOf(meb);
            uint address = 0;

            while(VirtualQuery(address, out meb, sizeofMeb) != 0)
            {
                if(meb.Type == 0x1000000 && meb.State == 0x1000 && meb.AllocationProtect != 0x01)
                {
                    if (meb.AllocationBase == selfPE.OptionalHeader32.ImageBase)
                    {
                        address += 0x1000000;
                        continue;
                    }
                        
                    IntPtr mBase = new IntPtr(meb.AllocationBase);

                    VirtualProtect(selfPE.ModuleBaseAddress, PEInfomation.SizeOfDosHeader, 0x4, out oldProt);
                    //ZeroMemory(selfPE.ModuleBaseAddress + 0x3c, 4);
                    //memcpy(selfPE.ModuleBaseAddress + 0x3c, new IntPtr(meb.AllocationBase + 0x3c), 4);
                   // WriteMemory(selfPE.ModuleBaseAddress + 0x3c, )
                    VirtualProtect(selfPE.ModuleBaseAddress, PEInfomation.SizeOfDosHeader, oldProt, out buff);
                    break;
                }
                else
                {
                    address += meb.RegionSize;
                }
            }

            
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int VirtualProtect(IntPtr address, int size, uint flNewProtect, out uint old);
        [DllImport("Kernel32.dll")]
        private static extern void ZeroMemory(IntPtr dest, int size);
        [DllImport("Kernel32.dll")]
        private static extern int VirtualQuery(uint addr, out MEMORY_BASIC_INFORMATION b, int len);
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr process, IntPtr baseAddress, IntPtr buf, int bufferSize, int bytesRead);
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WriteMemory(IntPtr dest, ref uint buf, int size, int count);
        [StructLayout(LayoutKind.Sequential)]
        struct MEMORY_BASIC_INFORMATION
        {
            public uint BaseAddress;
            public uint AllocationBase;
            public uint AllocationProtect;
            public uint RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct IMAGE_DOS_HEADER
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            [FieldOffset(0)]
            public Char[] e_magic;
        }


    }
}
