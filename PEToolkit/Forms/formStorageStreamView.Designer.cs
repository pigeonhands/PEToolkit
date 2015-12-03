namespace PEToolkit.Forms
{
    partial class formStorageStreamView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lvSections = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmStorageStream = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dumpStorageStreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmStorageStream.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvSections
            // 
            this.lvSections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.lvSections.ContextMenuStrip = this.cmStorageStream;
            this.lvSections.FullRowSelect = true;
            this.lvSections.GridLines = true;
            this.lvSections.Location = new System.Drawing.Point(12, 12);
            this.lvSections.MultiSelect = false;
            this.lvSections.Name = "lvSections";
            this.lvSections.Size = new System.Drawing.Size(428, 156);
            this.lvSections.TabIndex = 1;
            this.lvSections.UseCompatibleStateImageBehavior = false;
            this.lvSections.View = System.Windows.Forms.View.Details;
            this.lvSections.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvSections_MouseDoubleClick);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Name";
            this.columnHeader6.Width = 140;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Offset";
            this.columnHeader7.Width = 146;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Size";
            this.columnHeader8.Width = 102;
            // 
            // cmStorageStream
            // 
            this.cmStorageStream.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dumpStorageStreamToolStripMenuItem});
            this.cmStorageStream.Name = "cmStorageStream";
            this.cmStorageStream.Size = new System.Drawing.Size(173, 26);
            // 
            // dumpStorageStreamToolStripMenuItem
            // 
            this.dumpStorageStreamToolStripMenuItem.Name = "dumpStorageStreamToolStripMenuItem";
            this.dumpStorageStreamToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.dumpStorageStreamToolStripMenuItem.Text = "Dump stream data";
            this.dumpStorageStreamToolStripMenuItem.Click += new System.EventHandler(this.dumpStorageStreamToolStripMenuItem_Click);
            // 
            // formStorageStreamView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 180);
            this.Controls.Add(this.lvSections);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "formStorageStreamView";
            this.Text = "Storage Streams";
            this.Load += new System.EventHandler(this.formStorageStreamView_Load);
            this.cmStorageStream.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvSections;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ContextMenuStrip cmStorageStream;
        private System.Windows.Forms.ToolStripMenuItem dumpStorageStreamToolStripMenuItem;
    }
}