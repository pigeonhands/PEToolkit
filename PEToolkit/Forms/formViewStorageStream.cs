using PEToolkit.PE.Structures.MetaData;
using PEViewer.PE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEToolkit.Forms
{
    public partial class formViewStorageStream : Form
    {
        public formViewStorageStream(PEInfomation pe, STORAGE_STREAM_HEADER targetStream)
        {
            InitializeComponent();
            this.Text += string.Format(" ({0})", new string(targetStream.rcName).Replace("\0", ""));
            try
            {
                rtbStorageData.Text = Encoding.UTF8.GetString(pe.ReadStorageStream(targetStream)).Replace("\0", "");//temp
            }
            catch
            {
                rtbStorageData.Text = "Failed.";
            }
        }

        private void formViewStorageStream_Load(object sender, EventArgs e)
        {

        }
    }
}
