using PEToolkit.PE;
using PEToolkit.PE.Structures;
using PEToolkit.PE.Structures.MetaData;
using PEViewer.PE.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEViewer.PE
{
    public class PEInfomation
    {

        public string PESource { get; set; }
        public bool IsProcess { get; private set;}
        public IntPtr ModuleBaseAddress { get; private set; }
        public byte[] DataBytes { get; private set; }

        private int ProcessID;
        private string FilePath;
        IntPtr Handle = IntPtr.Zero;
        IntPtr loadedModuleHandle = IntPtr.Zero;

        public IMAGE_DOS_HEADER DosHeader;
        public IMAGE_FILE_HEADER FileHeader;
        public IMAGE_OPTIONAL_HEADER32 OptionalHeader32;
        public IMAGE_DATA_DIRECTORIES DataDirectories;
        public IMAGE_SECTION_HEADER[] Sections;
        public IMAGE_OVERVIEW Overview;
        //public MODULE_INFO ModuleInfo;

        public NET_STRUCTURES NetStructures;

        public bool IsNet { get { return (FileHeader.NumberOfSections > 0 && DataDirectories.CLRRuntimeHeaderRva != 0x0 && DataDirectories.SizeOfCLRRumtimeHeader != 0x0); } }

        public const int SizeOfDosHeader = 0x40;
        public const int SizeOfFileHeader = 0x18;
        public const int SizeOfOptionalHeader = 0x60;
        public const int SizeOfDataDirectories = 0x80;
        public const int SizeOfSectionHeader = 0x28;

        public void WriteOverview()
        {
            Overview.AddressOfEntrypoint = OptionalHeader32.AddressOfEntrypoint;
            Overview.FileHeaderPointer = DosHeader.e_lfanew;
            Overview.ImageBase = OptionalHeader32.ImageBase;
            Overview.NumberOfSections = FileHeader.NumberOfSections;
            Overview.SizeOfHeaders = OptionalHeader32.SizeOfHeaders;
            Overview.SizeOfImage = OptionalHeader32.SizeOfImage;
        }

        public PEInfomation(byte[] b, string path)
        {
            DataBytes = b;
            FilePath = path;
            IsProcess = false;
        }

        public PEInfomation(int pId, IntPtr _module)
        {
            ProcessID = pId;
            ModuleBaseAddress = _module;
            LoadModuleInfo();
        }

        public PEInfomation(IntPtr procHandle, IntPtr _module)
        {
            Handle = procHandle;
            ProcessID = 0;
            ModuleBaseAddress = _module;
            LoadModuleInfo();
        }

        void LoadModuleInfo()
        {
            StringBuilder sb = new StringBuilder(255);
            NativeMethods.GetModuleFileNameEx(GetProcessHandle(), ModuleBaseAddress, sb, 255);
            FilePath = sb.ToString();

            /*
            ModuleInfo = new MODULE_INFO();
            GetModuleInformation(GetProcessHandle(), ModuleBaseAddress, out ModuleInfo, Marshal.SizeOf(typeof(MODULE_INFO)));
            Debug.WriteLine(new Win32Exception(Marshal.GetLastWin32Error()).Message);
            */

            CloseProcessHandle();

            IsProcess = true;
        }

        public IntPtr GetProcessHandle()
        {
            if (Handle != IntPtr.Zero)
                return Handle;
            if (IsProcess)
                Handle = NativeMethods.OpenProcess(0x1F0FFF, false, ProcessID);
            return Handle;
        }

        public void CloseProcessHandle()
        {
            if (Handle == IntPtr.Zero)
                return;

            if (IsProcess)
            {
                if (NativeMethods.CloseHandle(Handle))
                    Handle = IntPtr.Zero;
            }
        }

        public IntPtr LoadModule()
        {
            if (loadedModuleHandle != IntPtr.Zero)
                return loadedModuleHandle;

            loadedModuleHandle = NativeMethods.LoadLibrary(FilePath);


            return loadedModuleHandle;
        }

        public void UnloadModule()
        {
            if (loadedModuleHandle == IntPtr.Zero)
                return;

            if (NativeMethods.FreeLibrary(loadedModuleHandle))
                loadedModuleHandle = IntPtr.Zero;
        }

        public byte[] ReadStorageStream(STORAGE_STREAM_HEADER storageStream)
        {
            if (!IsNet)
                return null;
            try
            {
                byte[] stream = new byte[storageStream.iSize];

                if (IsProcess)
                {
                    uint protection = 0;
                    
                    IntPtr handle = GetProcessHandle();
                    IntPtr address = new IntPtr(ModuleBaseAddress.ToInt32() + NetStructures.COR20Header.MetaDataRva + storageStream.iOffset);

                    NativeMethods.VirtualProtectEx(handle, address, stream.Length, 0x10, out protection);
                    bool success = NativeMethods.ReadProcessMemory(handle, address, stream, stream.Length, 0);
                    NativeMethods.VirtualProtectEx(handle, address, stream.Length, protection, out protection);

                    CloseProcessHandle();
                    if (!success)
                        throw new Exception("Failed to read.");
                }
                else
                {
                    Buffer.BlockCopy(DataBytes, NetStructures.NetOffsets.MetaDataRawAddress + (int)storageStream.iOffset, stream, 0, stream.Length);
                }
                return stream;
            }
            catch
            {
                return null;
            }
        }



        
    }
}
