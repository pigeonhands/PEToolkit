using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEViewer.Forms
{
    public partial class formBuildStructure : Form
    {
        public formBuildStructure(Type structure)
        {
            InitializeComponent();
            rtbStruct.Text = "[StructLayout(LayoutKind.Sequential)]" + Environment.NewLine;
            rtbStruct.Text += string.Format("public struct {0}", structure.Name) + Environment.NewLine;
            rtbStruct.Text += "{" + Environment.NewLine;
            foreach(FieldInfo f in structure.GetFields())
            {
                if(f.FieldType.IsArray)
                    rtbStruct.Text += string.Format("   [MarshalAs(UnmanagedType.ByValArray, SizeConst={0})]", f.GetCustomAttribute<MarshalAsAttribute>().SizeConst) + Environment.NewLine;
                rtbStruct.Text += string.Format("   public {0} {1};", f.FieldType.Name, f.Name) + Environment.NewLine;
            }
            rtbStruct.Text += "}" + Environment.NewLine;
        }

        public formBuildStructure(Type structure, string[] list)
        {
            InitializeComponent();
            int offset = 0;

            rtbStruct.Text = "[StructLayout(LayoutKind.Explicit)]" + Environment.NewLine;
            rtbStruct.Text += string.Format("public struct {0}", structure.Name) + Environment.NewLine;
            rtbStruct.Text += "{" + Environment.NewLine;
            foreach (FieldInfo f in structure.GetFields())
            {
                if (list.Contains(f.Name))
                {

                    if (f.FieldType.IsArray)
                        rtbStruct.Text += string.Format("   [MarshalAs(UnmanagedType.ByValArray, SizeConst={0})]", f.GetCustomAttribute<MarshalAsAttribute>().SizeConst) + Environment.NewLine;
                    rtbStruct.Text += string.Format("   [FieldOffset({0})] public {1} {2};", offset, f.FieldType.Name, f.Name) + Environment.NewLine;
                }
                if (f.FieldType.IsArray)
                    offset += (f.GetCustomAttribute<MarshalAsAttribute>().SizeConst * Marshal.SizeOf(f.FieldType.GetElementType()));
                else
                    offset += Marshal.SizeOf(f.FieldType);
            }
            rtbStruct.Text += "}" + Environment.NewLine;
        }

        private void formBuildStructure_Load(object sender, EventArgs e)
        {

        }
    }
}
