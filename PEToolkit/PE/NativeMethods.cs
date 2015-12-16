using PEToolkit.PE.Structures;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PEToolkit.PE
{
    internal delegate bool EnumResourceNameCallback(IntPtr module, string type, string name, IntPtr z);
    internal delegate bool EnumResourceTypeCallback(IntPtr module, string type, IntPtr z);
    internal class NativeMethods
    {
        

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string path);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr handle);

        [DllImport("psapi.dll")]
        public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, StringBuilder lpBaseName, int nSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr process, IntPtr baseAddress, byte[] buffer, int bufferSize, int bytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtectEx(IntPtr process, IntPtr baseAddress, int size, uint newProtection, out uint oldProtection);

        [DllImport("psapi.dll")]
        public static extern bool EnumProcessModulesEx(IntPtr hProc, IntPtr[] lphModule, int size, out int sizeNeeded, uint flags);

        [DllImport("kernel32.dll")]
        public static extern uint VirtualQueryEx(IntPtr handle, uint address, out MEMORY_BASIC_INFORMATION info, int size);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, int lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int WriteProcessMemory(IntPtr handle, IntPtr address, byte[] buffer, uint blength, int readwrite);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int WriteProcessMemory(IntPtr handle, IntPtr address, string buffer, int blength, int readwrite);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string name);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr mHandle, string fname);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateRemoteThread(IntPtr pHandle, int att_0, int stacksize_0, IntPtr callingFunction, IntPtr paramiters, uint createFlags_0, int tID);

        [DllImport("kernel32.dll")]
        public static extern bool GetExitCodeThread(IntPtr handle, ref IntPtr retBuffer);

        [DllImport("kernel32")]
        public static extern int WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        [DllImport("kernel32.dll")]
        public static extern bool EnumResourceNames(IntPtr module, string rType, EnumResourceNameCallback cb, IntPtr z);

        [DllImport("kernel32.dll")]
        public static extern bool EnumResourceTypes(IntPtr module, EnumResourceTypeCallback cb, IntPtr z);
    }
}
