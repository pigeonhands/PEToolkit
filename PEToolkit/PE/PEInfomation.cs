using PEViewer.PE.Structures;
using System;
using System.Collections.Generic;
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
        public IntPtr ModuleBaseAddress { get { return SelectedModule.BaseAddress; } }

        private int ProcessID;
        private string FilePath;
        IntPtr Handle = IntPtr.Zero;
        IntPtr ModuleHandle = IntPtr.Zero;
        private ProcessModule SelectedModule;

        public IMAGE_DOS_HEADER DosHeader;
        public IMAGE_FILE_HEADER FileHeader;
        public IMAGE_OPTIONAL_HEADER32 OptionalHeader32;
        public IMAGE_DATA_DIRECTORIES DataDirectories;
        public IMAGE_SECTION_HEADER[] Sections;
        public IMAGE_OVERVIEW Overview;

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

        public PEInfomation(int pId, ProcessModule _module)
        {
            ProcessID = pId;
            SelectedModule = _module;
            IsProcess = true;
        }

        public IntPtr GetHandle()
        {
            if (Handle != IntPtr.Zero)
                return Handle;
            if (IsProcess)
                Handle = OpenProcess(0x1F0FFF, false, ProcessID);
            return Handle;
        }

        public IntPtr LoadModule()
        {
            if (ModuleHandle != IntPtr.Zero)
                return ModuleHandle;

            if(IsProcess)
                ModuleHandle = LoadLibrary(SelectedModule.FileName);
            else
                ModuleHandle = LoadLibrary(FilePath);

            return ModuleHandle;
        }

        public void UnloadModule()
        {
            if (ModuleHandle == IntPtr.Zero)
                return;

            if (FreeLibrary(ModuleHandle))
                ModuleHandle = IntPtr.Zero;
        }

        public void CloseHandle()
        {
            if (Handle == IntPtr.Zero)
                return;

            if (IsProcess)
            {
                if (CloseHandle(Handle))
                    Handle = IntPtr.Zero;
            }
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr handle);
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string path);
        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr handle);
    }
}
