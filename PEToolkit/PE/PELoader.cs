using PEViewer.PE.Structures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEViewer.PE
{
    /// <summary>
    /// Made by BahNahNah
    /// uid=2388291
    /// </summary>
    public class PELoader
    {
        public static PEInfomation Load(string file)
        {
            if (!File.Exists(file)) throw new ArgumentException("File does not exist", "file");
            return Load(File.ReadAllBytes(file), file);
        }

        public static PEInfomation Load(byte[] data, string path)
        {
            if (data == null) throw new ArgumentNullException("data");

            PEInfomation info = new PEInfomation(path);

            info.DosHeader = StructFromBytes<IMAGE_DOS_HEADER>(data, 0);
            info.FileHeader = StructFromBytes<IMAGE_FILE_HEADER>(data, Convert.ToInt32(info.DosHeader.e_lfanew));
            info.OptionalHeader32 = StructFromBytes<IMAGE_OPTIONAL_HEADER32>(data, Convert.ToInt32(info.DosHeader.e_lfanew) + Marshal.SizeOf(info.FileHeader));
            info.DataDirectories = StructFromBytes<IMAGE_DATA_DIRECTORIES>(data, Convert.ToInt32(info.DosHeader.e_lfanew) + Marshal.SizeOf(info.FileHeader) + Marshal.SizeOf(info.OptionalHeader32));

            info.Sections = new IMAGE_SECTION_HEADER[info.FileHeader.NumberOfSections];
            int sectionsBase = Convert.ToInt32(info.DosHeader.e_lfanew) + Marshal.SizeOf(info.FileHeader) + Marshal.SizeOf(info.OptionalHeader32) + Marshal.SizeOf(info.DataDirectories);
            int sizeOfSection = Marshal.SizeOf(typeof(IMAGE_SECTION_HEADER));
            for (int i = 0; i < info.Sections.Length; i++)
            {
                int sectionLocation = sectionsBase + (sizeOfSection * i);
                info.Sections[i] = StructFromBytes<IMAGE_SECTION_HEADER>(data, sectionLocation);
            }

            info.WriteOverview();
            return info;

        }

        public static PEInfomation Load(int ProcessID, ProcessModule module)
        {
            PEInfomation info = new PEInfomation(ProcessID, module);
            IntPtr handle = info.GetHandle();
            if (handle == IntPtr.Zero)
                throw new ArgumentException("Invalid process", "ProcessID");

            IntPtr baseAddress = module.BaseAddress;

            info.DosHeader = StructFromMemory<IMAGE_DOS_HEADER>(handle, baseAddress);
            IntPtr imageBase = new IntPtr(info.DosHeader.e_lfanew + (uint)baseAddress);

            info.FileHeader = StructFromMemory<IMAGE_FILE_HEADER>(handle, imageBase);
            info.OptionalHeader32 = StructFromMemory<IMAGE_OPTIONAL_HEADER32>(handle, imageBase + Marshal.SizeOf(info.FileHeader));
            info.DataDirectories = StructFromMemory<IMAGE_DATA_DIRECTORIES>(handle, imageBase + Marshal.SizeOf(info.FileHeader) + Marshal.SizeOf(info.OptionalHeader32));

            info.Sections = new IMAGE_SECTION_HEADER[info.FileHeader.NumberOfSections];
            IntPtr sectionsBase = imageBase + Marshal.SizeOf(info.FileHeader) + Marshal.SizeOf(info.OptionalHeader32) + Marshal.SizeOf(info.DataDirectories);
            int sizeOfSection = Marshal.SizeOf(typeof(IMAGE_SECTION_HEADER));
            for (int i = 0; i < info.Sections.Length; i++)
            {
                IntPtr sectionLocation = sectionsBase + (sizeOfSection * i);
                info.Sections[i] = StructFromMemory<IMAGE_SECTION_HEADER>(handle, sectionLocation);
            }

            info.CloseHandle();

            info.WriteOverview();
            return info;
        }

        public static PEInfomation DisectSelf()
        {
            Process p = Process.GetCurrentProcess();
            return Load(p.Id, p.Modules[0]);
        }

        private static T StructFromMemory<T>(IntPtr handle, IntPtr address)
        {
            int structSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[structSize];
            ReadProcessMemory(handle, address, buffer, buffer.Length, 0);
            return StructFromBytes<T>(buffer, 0);
        }

        private static T StructFromBytes<T>(byte[] data, int offset)
        {
            int structSize = Marshal.SizeOf(typeof(T));
            IntPtr gAlloc = Marshal.AllocHGlobal(structSize);
            Marshal.Copy(data, offset, gAlloc, structSize);
            T retStruct = (T)Marshal.PtrToStructure(gAlloc, typeof(T));
            Marshal.FreeHGlobal(gAlloc);
            return retStruct;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(uint access, bool inherit, int id);
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr process, IntPtr baseAddress, byte[] buffer, int bufferSize, int bytesRead);
    }
}
