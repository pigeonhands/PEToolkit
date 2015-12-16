using PEToolkit.PE;
using PEViewer.PE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEToolkit.Memory_Tools
{
    public class ManualMap
    {
        public static void LoadDll(int pID, byte[] dllBytes)
        {
            PEInfomation dllPE = PELoader.Load(dllBytes, string.Empty);
            IntPtr pHandle = PELoader.OpenProcessHandle(pID);

            if (pHandle == IntPtr.Zero)
                throw new Exception("Invalid PID");

            IntPtr vAlloc = NativeMethods.VirtualAllocEx(pHandle, 0, dllPE.Overview.SizeOfImage, 0x1000, 0x40);

            if (vAlloc == IntPtr.Zero)
                throw new Exception("Alloc failed");

            NativeMethods.WriteProcessMemory(pHandle, vAlloc, dllBytes, dllPE.Overview.SizeOfHeaders, 0);

            foreach(var section in dllPE.Sections)
            {
                byte[] sData = new byte[section.VirtualSize];
                Buffer.BlockCopy(dllBytes, (int)section.PointerToRawData, sData, 0, sData.Length);

                NativeMethods.WriteProcessMemory(pHandle,  new IntPtr(vAlloc.ToInt32() + section.VirtualAddress), sData, (uint)sData.Length, 0);
            }

            
        }
    }
}
