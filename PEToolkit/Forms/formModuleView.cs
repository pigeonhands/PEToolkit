using PEToolkit.Controls;
using PEToolkit.PE;
using PEToolkit.PE.Structures;
using PEViewer.Memory_Tools;
using PEViewer.PE;
using PEViewer.PE.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEToolkit.Forms
{
    public partial class formModuleView : Form
    {
        int ProcessID;
        public PEInfomation LoadInfomation { get; private set; }

        public object DllInject { get; private set; }
        List<IntPtr> FoundModules = new List<IntPtr>();

        public formModuleView(int pID, string name)
        {
            InitializeComponent();
            ProcessID = pID;
            this.Text += string.Format(" ({0})", name);
        }

        private void formModuleView_Load(object sender, EventArgs e)
        {
            PopulateList();
        }

        void PopulateList()
        {
            
            IntPtr pHandle = PELoader.OpenProcessHandle(ProcessID);
            if (pHandle == IntPtr.Zero)
            {
                MessageBox.Show("Failed to load process");
                this.DialogResult = DialogResult.OK;
                return;
            }

            int size = 0;
            if (!NativeMethods.EnumProcessModulesEx(pHandle, null, 0, out size, 0x01))
            {
                MessageBox.Show("Failed to get module count");
                this.DialogResult = DialogResult.OK;
                return;
            }

            lvModules.Items.Clear();
            FoundModules.Clear();

            int ModuleCount = size / Marshal.SizeOf(typeof(IntPtr));
            IntPtr[] modules = new IntPtr[ModuleCount];

            if (!NativeMethods.EnumProcessModulesEx(pHandle, modules, size, out size, 0x01))
            {
                MessageBox.Show("Failed to get modules");
                this.DialogResult = DialogResult.OK;
                return;
            }

            FoundModules.AddRange(modules);

            foreach (IntPtr m in modules)
            {
                lvModules.Items.Add(new ModuleListViewItem(ProcessID, m));
            }

            PELoader.CloseProcessHandle(pHandle);
        }

        

        private void dumpSelectedModulesToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (lvModules.SelectedItems.Count < 1)
                return;

            if (lvModules.SelectedItems.Count == 1)
            {
                ModuleListViewItem item = (ModuleListViewItem)lvModules.SelectedItems[0];
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = string.Format("Module dump|*{0}", Path.GetExtension(item.ModulePath));
                    if(sfd.ShowDialog() == DialogResult.OK)
                    {
                        DumpModule(item.ModuleInfomation, sfd.FileName);
                        MessageBox.Show("Done.");
                    }
                }
            }
            else
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    if(fbd.ShowDialog() == DialogResult.OK)
                    {
                        foreach(ListViewItem i in lvModules.SelectedItems)
                        {
                            ModuleListViewItem item = (ModuleListViewItem)i;
                            DumpModule(item.ModuleInfomation, Path.Combine(fbd.SelectedPath, "Dump_" + Path.GetFileName(item.ModulePath)));
                        }
                        MessageBox.Show("Done.");
                    }
                }
            }
        }

        void DumpModule(PEInfomation procPE, string path)
        {

            byte[] buffer = new byte[procPE.Overview.SizeOfImage];

            IntPtr procHandle = procPE.GetProcessHandle();
            NativeMethods.ReadProcessMemory(procHandle, procPE.ModuleBaseAddress, buffer, Convert.ToInt32(procPE.Overview.SizeOfHeaders), 0);

            foreach (IMAGE_SECTION_HEADER section in procPE.Sections)
            {
                if (section.SizeOfRawData == 0)
                    continue;

                byte[] sData = new byte[section.SizeOfRawData];
                NativeMethods.ReadProcessMemory(procHandle, new IntPtr(procPE.Overview.ImageBase + section.VirtualAddress), sData, sData.Length, 0);

                Buffer.BlockCopy(sData, 0, buffer, Convert.ToInt32(section.PointerToRawData), sData.Length);
            }

            File.WriteAllBytes(path, buffer);
            procPE.CloseProcessHandle();
        }

        private void unloadSelectedModulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem i in lvModules.SelectedItems)
            {
                ModuleListViewItem item = (ModuleListViewItem)i;
                IntPtr pHandle = PELoader.OpenProcessHandle(ProcessID);
                DllInjector.UnloadDll(pHandle, item.ModuleInfomation.ModuleBaseAddress);
                PELoader.CloseProcessHandle(pHandle);
                PopulateList();
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopulateList();
        }

        private void findUnlistedImageSectorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MEMORY_BASIC_INFORMATION memInfo = new MEMORY_BASIC_INFORMATION();
            int mem_size = Marshal.SizeOf(memInfo);
            uint currentAddress = 0;

            IntPtr hProc = PELoader.OpenProcessHandle(ProcessID);
            while(NativeMethods.VirtualQueryEx(hProc, currentAddress, out memInfo, mem_size) != 0)
            {
                if (FoundModules.Contains(memInfo.AllocationBase))
                {
                    currentAddress += memInfo.RegionSize;
                    continue;
                }

                if (memInfo.Protect == 0x1)//memInfo.Type != 0x1000000
                {
                    currentAddress += memInfo.RegionSize;
                    continue;
                }

                IMAGE_DOS_HEADER header = PELoader.StructFromMemory<IMAGE_DOS_HEADER>(hProc, memInfo.AllocationBase);

                if (!FoundModules.Contains(memInfo.BaseAddress))
                {
                    byte[] buffer = new byte[memInfo.RegionSize];
                    NativeMethods.ReadProcessMemory(hProc, memInfo.BaseAddress, buffer, buffer.Length, 0);
                    for (int i = 0; i < buffer.Length - 1; i++)
                    {
                        if (buffer[i] == 'M' && buffer[i + 1] == 'Z')
                            lvModules.Items.Add(new ModuleListViewItem(ProcessID, memInfo.BaseAddress + i));
                    }
                    FoundModules.Add(memInfo.BaseAddress);
                }
                /*
                if(header.e_magic[0] == 'M' && header.e_magic[1] == 'Z')
                    lvModules.Items.Add(new ModuleListViewItem(ProcessID, memInfo.AllocationBase));
                FoundModules.Add(memInfo.AllocationBase);
                */
                currentAddress += memInfo.RegionSize;//0x1000000
            }

            PELoader.CloseProcessHandle(hProc);
        }

        private void loadModuleIntoPEViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvModules.SelectedItems.Count > 0)
            {
                ModuleListViewItem i = (ModuleListViewItem)lvModules.SelectedItems[0];
                LoadInfomation = i.ModuleInfomation;
                this.DialogResult = DialogResult.Yes;
            }
        }

       

        
    }
}
