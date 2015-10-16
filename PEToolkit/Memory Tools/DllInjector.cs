﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace PEViewer.Memory_Tools
{
    /// <summary>
    /// Dll Injector
    /// Made by BahNahNah
    /// uid=2388291
    /// </summary>
    public class DllInjector
    {
        /// <summary>
        /// Gets the last error
        /// </summary>
        /// <returns></returns>
        public static string GetLastError()
        {
            return new Win32Exception(Marshal.GetLastWin32Error()).Message;
        }

        /// <summary>
        /// Injects a dll into the target process
        /// </summary>
        /// <param name="TargetProcess">Process to inject into</param>
        /// <param name="dll">Path to dll</param>
        /// <returns>Success of injection</returns>
        public static bool Inject(Process TargetProcess, string dll)
        {
            if (TargetProcess == null) throw new ArgumentNullException("TargetProcess");
            return Inject(TargetProcess.Id, dll);
        }

        /// <summary>
        /// Injects a dll into the target process
        /// </summary>
        /// <param name="pID">Process ID of target process</param>
        /// <param name="DllPath">Path to dll</param>
        /// <returns>Success of injection</returns>
        public static bool Inject(int pID, string DllPath)
        {
            //Opens the target process with access to modify memory and create threads
            IntPtr Handle = OpenProcess(0x8 | 0x2 | 0x400 | 0x10 | 0x20, false, pID);

            if (Handle == IntPtr.Zero) throw new ArgumentException("Invalid process id or no permission", "pID");
            bool success;
            Inject(Handle, DllPath, out success, false);
            return success;
        }

        /// <summary>
        /// Injects a dll into the target process, waits for remote thread to exit and returns dll handle
        /// </summary>
        /// <param name="pId">Process ID of target process</param>
        /// <param name="DllPath">Path to dll</param>
        /// <param name="success">Success of dll injection</param>
        /// <returns>Handle of injected dll</returns>
        public static IntPtr Inject(int pID, string DllPath, out bool success)
        {
            IntPtr Handle = OpenProcess(0x8 | 0x2 | 0x400 | 0x10 | 0x20, false, pID);

            if (Handle == IntPtr.Zero) throw new ArgumentException("Invalid process id or no permission", "pID");
            return Inject(Handle, DllPath, out success, true);
        }

        /// <summary>
        /// Injects a dll into the target process
        /// </summary>
        /// <param name="Handle">Handle of target process</param>
        /// <param name="DllPath">Path to dll</param>
        /// <param name="success">Success of dll injection</param>
        /// <param name="waitForDllHandle">If true, waits for remote thread to exit then returns DllHandle</param>
        /// <returns>if waitForDllHandle is true, Handle of DLL in remote process is returned, else IntPtr.Zero is returned</returns>
        public static IntPtr Inject(IntPtr Handle, string DllPath, out bool success, bool waitForDllHandle)
        {
            if (Handle == IntPtr.Zero) throw new ArgumentNullException("Handle");
            if (!File.Exists(DllPath)) throw new ArgumentException("Must point to a valid file", "DllPath");

            //We need the FULL path of the dll when loading it
            string FullDllPath = Path.GetFullPath(DllPath);

            //Allocate ehough memory in the target process for the full dll path plus a "null Terminator" byte
            IntPtr vAlloc = VirtualAllocEx(Handle, 0, FullDllPath.Length + 1, 0x1000, 0x40);
            if (vAlloc == IntPtr.Zero)
            {
                //If the memory was not allocated, close the process handle and exit
                CloseHandle(Handle);
                success = false;
                return IntPtr.Zero;
            }

            //Write the path of the dll into the memory that was allocated
            //This is the same thing as setting a variable, except it is setting the value in the target process
            if (WriteProcessMemory(Handle, vAlloc, FullDllPath, FullDllPath.Length, 0) == 0)
            {
                //If the path was not written to the target process, close the process handle and exit
                CloseHandle(Handle);
                success = false;
                return IntPtr.Zero;
            }

            //Get the address of the kernel32 library
            IntPtr hKernel32 = GetModuleHandle("kernel32.dll");

            //Get the address of LoadLibraryA from inside the kernel32 library
            //https://msdn.microsoft.com/en-us/library/windows/desktop/ms684175(v=vs.85).aspx
            //LoadLibraryA - ANSI string as paramiter
            //LoadLibraryW - Unicode string as paramiter
            //LoadLibrary - Use default (Unicode), but not avalible through GetProcAddress
            //
            IntPtr hLoadLibrary = GetProcAddress(hKernel32, "LoadLibraryA");

            if (hLoadLibrary == IntPtr.Zero)
            {
                //If We could not find the address of LoadLibraryA, close the process handle and exit
                CloseHandle(Handle);
                success = false;
                return IntPtr.Zero;
            }

            //Call "LoadLibraryA" with the full path of the dll as the paramiter in the target process in a new thread
            IntPtr hThread = CreateRemoteThread(Handle, 0, 0, hLoadLibrary, vAlloc, 0, 0);

            //If thread was started successfully, injection was a success
            success = hThread != IntPtr.Zero;

            IntPtr dllHandle = IntPtr.Zero;

            if (waitForDllHandle && success)
            {
                //If injection was a success, get thread exit code
                //In this case, it will be the return value of LoadLibrary or 259 if its still running
                GetExitCodeThread(hThread, ref dllHandle);
            }

            //Close the process handle
            CloseHandle(Handle);

            //Return the handle of the created thread
            return dllHandle;
        }
        #region " WinApi "

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, int lpAddress, int dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int WriteProcessMemory(IntPtr handle, IntPtr address, string buffer, int blength, int readwrite);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr mHandle, string fname);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateRemoteThread(IntPtr pHandle, int att_0, int stacksize_0, IntPtr callingFunction, IntPtr paramiters, uint createFlags_0, int tID);

        [DllImport("kernel32.dll")]
        private static extern bool GetExitCodeThread(IntPtr handle, ref IntPtr retBuffer);

        #endregion
    }
}