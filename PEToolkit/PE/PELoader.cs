using PEToolkit.PE.Structures.MetaData;
using PEViewer.PE.Structures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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

            if (info.FileHeader.NumberOfSections > 0 && info.DataDirectories.CLRRuntimeHeaderRva != 0x0 && info.DataDirectories.SizeOfCLRRumtimeHeader != 0x0)
            {
                //is .net
                info.NetStructures.NetOffsets.COR20RawAddress = Convert.ToInt32(info.DataDirectories.CLRRuntimeHeaderRva - info.Sections[0].VirtualAddress + info.Sections[0].PointerToRawData);
                info.NetStructures.COR20Header = StructFromBytes<COR20_HEADER>(data, info.NetStructures.NetOffsets.COR20RawAddress);

                info.NetStructures.NetOffsets.MetaDataRawAddress = Convert.ToInt32(info.NetStructures.COR20Header.MetaDataRva - info.Sections[0].VirtualAddress + info.Sections[0].PointerToRawData);

                info.NetStructures.MetaDataHeader.Signature = BitConverter.ToUInt32(data, info.NetStructures.NetOffsets.MetaDataRawAddress);
                info.NetStructures.MetaDataHeader.MajorVersion = BitConverter.ToUInt16(data, info.NetStructures.NetOffsets.MetaDataRawAddress + 4);
                info.NetStructures.MetaDataHeader.MinorVersion = BitConverter.ToUInt16(data, info.NetStructures.NetOffsets.MetaDataRawAddress + 6);
                info.NetStructures.MetaDataHeader.Reserved = BitConverter.ToUInt32(data, info.NetStructures.NetOffsets.MetaDataRawAddress + 8);
                info.NetStructures.MetaDataHeader.VersionLength = BitConverter.ToUInt32(data, info.NetStructures.NetOffsets.MetaDataRawAddress + 12);

                info.NetStructures.MetaDataHeader.VersionString = new char[info.NetStructures.MetaDataHeader.VersionLength];
                info.NetStructures.MetaDataHeader.VersionString = Encoding.ASCII.GetString(data, info.NetStructures.NetOffsets.MetaDataRawAddress + 16, info.NetStructures.MetaDataHeader.VersionString.Length).ToCharArray();

                info.NetStructures.MetaDataHeader.Flags = BitConverter.ToUInt16(data, info.NetStructures.NetOffsets.MetaDataRawAddress + 16 + Convert.ToInt32(info.NetStructures.MetaDataHeader.VersionLength));
                info.NetStructures.MetaDataHeader.NumberOfStreams = BitConverter.ToUInt16(data, info.NetStructures.NetOffsets.MetaDataRawAddress + 18 + Convert.ToInt32(info.NetStructures.MetaDataHeader.VersionLength));

                int metaDataHeaderSize = 20 + Convert.ToInt32(info.NetStructures.MetaDataHeader.VersionLength);
                int offset = 0;

                info.NetStructures.StorageStreamHeaders = new STORAGE_STREAM_HEADER[5];

                int[] nameLengths = new int[] { 4, 12, 4, 8, 8};

                for(int i = 0; i < nameLengths.Length; i++)
                {
                    info.NetStructures.StorageStreamHeaders[i].iOffset = BitConverter.ToUInt32(data, info.NetStructures.NetOffsets.MetaDataRawAddress + metaDataHeaderSize + offset);
                    info.NetStructures.StorageStreamHeaders[i].iSize = BitConverter.ToUInt32(data, info.NetStructures.NetOffsets.MetaDataRawAddress + metaDataHeaderSize + 4 + offset);

                    info.NetStructures.StorageStreamHeaders[i].rcName = new char[nameLengths[i]];
                    info.NetStructures.StorageStreamHeaders[i].rcName = Encoding.ASCII.GetString(data, info.NetStructures.NetOffsets.MetaDataRawAddress + metaDataHeaderSize + 8 + offset, info.NetStructures.StorageStreamHeaders[i].rcName.Length).ToCharArray();
                    offset += 8 + nameLengths[i];
                }
                info.NetStructures.SizeOfSotrageStreamHeaders = offset;

                Debug.WriteLine((info.NetStructures.NetOffsets.MetaDataRawAddress + metaDataHeaderSize + offset + 0x6C).ToString("x2"));
            }

            info.WriteOverview();
            return info;

        }

        public static PEInfomation Load(int ProcessID, ProcessModule module)
        {
            return Load(ProcessID, module.BaseAddress);
        }


        public static PEInfomation Load(int ProcessID, IntPtr moduleAddress)
        {
            PEInfomation info = new PEInfomation(ProcessID, moduleAddress);
            return vLoad(info, moduleAddress);
        }

        public static PEInfomation Load(IntPtr procHandle, IntPtr moduleAddress)
        {
            PEInfomation info = new PEInfomation(procHandle, moduleAddress);
            return vLoad(info, moduleAddress);
        }

        private static PEInfomation vLoad(PEInfomation info, IntPtr baseAddress)
        {
            IntPtr handle = info.GetProcessHandle();
            if (handle == IntPtr.Zero)
                throw new ArgumentException("Invalid process", "ProcessID");

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

            info.CloseProcessHandle();

            info.WriteOverview();
            return info;
        }

        
        public static PEInfomation DisectSelf()
        {
            Process p = Process.GetCurrentProcess();
            return Load(p.Id, p.Modules[0]);
        }

        public static IntPtr OpenProcessHandle(int pid)
        {
            return OpenProcess(0x1F0FFF, false, pid);
        }

        public static void CloseProcessHandle(IntPtr handle)
        {
            if (handle != IntPtr.Zero)
                CloseHandle(handle);
        }

        public static T StructFromMemory<T>(IntPtr handle, IntPtr address)
        {
            int structSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[structSize];
            ReadProcessMemory(handle, address, buffer, buffer.Length, 0);
            return StructFromBytes<T>(buffer, 0);
        }

        public static T StructFromBytes<T>(byte[] data, int offset)
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
        private static extern bool CloseHandle(IntPtr handle);
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr process, IntPtr baseAddress, byte[] buffer, int bufferSize, int bytesRead);
    }
}
