using PEToolkit.PE.Structures;
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

        public PEInfomation(string path)
        {
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
            GetModuleFileNameEx(GetProcessHandle(), ModuleBaseAddress, sb, 255);
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
                Handle = OpenProcess(0x1F0FFF, false, ProcessID);
            return Handle;
        }

        public void CloseProcessHandle()
        {
            if (Handle == IntPtr.Zero)
                return;

            if (IsProcess)
            {
                if (CloseHandle(Handle))
                    Handle = IntPtr.Zero;
            }
        }

        public IntPtr LoadModule()
        {
            if (loadedModuleHandle != IntPtr.Zero)
                return loadedModuleHandle;

            loadedModuleHandle = LoadLibrary(FilePath);


            return loadedModuleHandle;
        }

        public void UnloadModule()
        {
            if (loadedModuleHandle == IntPtr.Zero)
                return;

            if (FreeLibrary(loadedModuleHandle))
                loadedModuleHandle = IntPtr.Zero;
        }



        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr handle);
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string path);
        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr handle);
        [DllImport("psapi.dll")]
        private static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpBaseName, int nSize);
        [DllImport("psapi.dll", SetLastError =true)]
        private static extern bool GetModuleInformation(IntPtr handle, IntPtr module, out MODULE_INFO inf, int cb);
        [DllImport("kernel32.dll")]
        private static extern bool GetModuleHandleEx(uint flags, StringBuilder address, out IntPtr handle);
    }
}
