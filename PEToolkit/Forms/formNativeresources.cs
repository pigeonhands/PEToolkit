using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEViewer.Forms
{

    public partial class formNativeresources : Form
    {
        private delegate bool EnumResourceNameCallback(IntPtr module, string type, string name, IntPtr z);
        private delegate bool EnumResourceTypeCallback(IntPtr module, string type, IntPtr z);

        EnumResourceNameCallback ResourceNameCallback;
        EnumResourceTypeCallback ResourceTypeCallback;

        IntPtr handle = IntPtr.Zero;
        public formNativeresources(IntPtr _handle)
        {
            handle = _handle;
            InitializeComponent();
            ResourceNameCallback = new EnumResourceNameCallback(nameCallback);
            ResourceTypeCallback = new EnumResourceTypeCallback(typeCallback);

            EnumResourceTypes(handle, ResourceTypeCallback, IntPtr.Zero);
           /*
            EnumResourceTypes(handle, "RT_RCDATA", "RT_STRING", "RT_VERSION", 
                "RT_ICON", "RT_GROUP_ICON", "RT_BITMAP", "RT_MESSAGETABLE", 
                "RT_MENU", "RT_MANIFEST", "RT_HTML", "RT_GROUP_CURSOR", 
                "RT_FONTDIR", "RT_FONT", "RT_DLGINCLUDE", "RT_DIALOG", 
                "RT_CURSOR", "RT_BITMAP", "RT_ANIICON", "RT_ANICURSOR", "RT_ACCELERATOR");
                */
        }

        void EnumResourceGroups(IntPtr handle, params string[] types)
        {
            foreach(string s in types)
            {
                EnumResourceNames(handle, s, ResourceNameCallback, IntPtr.Zero);
            }
        }

        bool typeCallback(IntPtr module, string type, IntPtr z)
        {
            EnumResourceNames(module, type, ResourceNameCallback, IntPtr.Zero);
            Debug.WriteLine("Type: " + type);
            return true;
        }

        bool nameCallback(IntPtr module, string type, string name, IntPtr z)
        {
            ListViewItem i = new ListViewItem(name);
            i.SubItems.Add(type);
            lvResources.Items.Add(i);
            return true;
        }

        private void formNativeresources_Load(object sender, EventArgs e)
        {

        }

        [DllImport("kernel32.dll")]
        private static extern bool EnumResourceNames(IntPtr module, string rType, EnumResourceNameCallback cb, IntPtr z);

        [DllImport("kernel32.dll")]
        private static extern bool EnumResourceTypes(IntPtr module, EnumResourceTypeCallback cb, IntPtr z);

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(new Win32Exception(Marshal.GetLastWin32Error()).Message);
        }
    }
}
