namespace PEViewer.Forms
{
    partial class mainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.cmOpen = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.overviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dOSHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionalPEHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataDirectoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.structuresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
            this.checkForRunPEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.injectDllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.generateStructuresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCurrentSection = new System.Windows.Forms.Label();
            this.lvInfo = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.unloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmOpen.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmOpen
            // 
            this.cmOpen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.processToolStripMenuItem});
            this.cmOpen.Name = "cmOpen";
            this.cmOpen.Size = new System.Drawing.Size(115, 48);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.toolStripDropDownButton2,
            this.toolStripSeparator4,
            this.toolStripDropDownButton4,
            this.toolStripSeparator3,
            this.toolStripDropDownButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(550, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.processToolStripMenuItem1});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(49, 22);
            this.toolStripDropDownButton1.Text = "Open";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
            this.fileToolStripMenuItem1.Text = "File";
            this.fileToolStripMenuItem1.Click += new System.EventHandler(this.fileToolStripMenuItem1_Click);
            // 
            // processToolStripMenuItem1
            // 
            this.processToolStripMenuItem1.Name = "processToolStripMenuItem1";
            this.processToolStripMenuItem1.Size = new System.Drawing.Size(114, 22);
            this.processToolStripMenuItem1.Text = "Process";
            this.processToolStripMenuItem1.Click += new System.EventHandler(this.processToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.overviewToolStripMenuItem,
            this.dOSHeaderToolStripMenuItem,
            this.imageHeaderToolStripMenuItem,
            this.optionalPEHeaderToolStripMenuItem,
            this.dataDirectoriesToolStripMenuItem,
            this.structuresToolStripMenuItem,
            this.resourcesToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(157, 22);
            this.toolStripDropDownButton2.Text = "Loaded Image Infomation";
            // 
            // overviewToolStripMenuItem
            // 
            this.overviewToolStripMenuItem.Name = "overviewToolStripMenuItem";
            this.overviewToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.overviewToolStripMenuItem.Text = "Overview";
            this.overviewToolStripMenuItem.Click += new System.EventHandler(this.overviewToolStripMenuItem_Click);
            // 
            // dOSHeaderToolStripMenuItem
            // 
            this.dOSHeaderToolStripMenuItem.Name = "dOSHeaderToolStripMenuItem";
            this.dOSHeaderToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.dOSHeaderToolStripMenuItem.Text = "DOS Header";
            this.dOSHeaderToolStripMenuItem.Click += new System.EventHandler(this.dOSHeaderToolStripMenuItem_Click);
            // 
            // imageHeaderToolStripMenuItem
            // 
            this.imageHeaderToolStripMenuItem.Name = "imageHeaderToolStripMenuItem";
            this.imageHeaderToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.imageHeaderToolStripMenuItem.Text = "File Header";
            this.imageHeaderToolStripMenuItem.Click += new System.EventHandler(this.imageHeaderToolStripMenuItem_Click);
            // 
            // optionalPEHeaderToolStripMenuItem
            // 
            this.optionalPEHeaderToolStripMenuItem.Name = "optionalPEHeaderToolStripMenuItem";
            this.optionalPEHeaderToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.optionalPEHeaderToolStripMenuItem.Text = "Optional PE Header";
            this.optionalPEHeaderToolStripMenuItem.Click += new System.EventHandler(this.optionalPEHeaderToolStripMenuItem_Click);
            // 
            // dataDirectoriesToolStripMenuItem
            // 
            this.dataDirectoriesToolStripMenuItem.Name = "dataDirectoriesToolStripMenuItem";
            this.dataDirectoriesToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.dataDirectoriesToolStripMenuItem.Text = "Data Directories";
            this.dataDirectoriesToolStripMenuItem.Click += new System.EventHandler(this.dataDirectoriesToolStripMenuItem_Click);
            // 
            // structuresToolStripMenuItem
            // 
            this.structuresToolStripMenuItem.Name = "structuresToolStripMenuItem";
            this.structuresToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.structuresToolStripMenuItem.Text = "Structures";
            this.structuresToolStripMenuItem.Click += new System.EventHandler(this.structuresToolStripMenuItem_Click);
            // 
            // resourcesToolStripMenuItem
            // 
            this.resourcesToolStripMenuItem.Name = "resourcesToolStripMenuItem";
            this.resourcesToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.resourcesToolStripMenuItem.Text = "Resources";
            this.resourcesToolStripMenuItem.Click += new System.EventHandler(this.resourcesToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton4
            // 
            this.toolStripDropDownButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForRunPEToolStripMenuItem,
            this.injectDllToolStripMenuItem,
            this.unloadToolStripMenuItem});
            this.toolStripDropDownButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton4.Image")));
            this.toolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
            this.toolStripDropDownButton4.Size = new System.Drawing.Size(60, 22);
            this.toolStripDropDownButton4.Text = "Process";
            // 
            // checkForRunPEToolStripMenuItem
            // 
            this.checkForRunPEToolStripMenuItem.Name = "checkForRunPEToolStripMenuItem";
            this.checkForRunPEToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.checkForRunPEToolStripMenuItem.Text = "Check for RunPE";
            this.checkForRunPEToolStripMenuItem.Click += new System.EventHandler(this.checkForRunPEToolStripMenuItem_Click);
            // 
            // injectDllToolStripMenuItem
            // 
            this.injectDllToolStripMenuItem.Name = "injectDllToolStripMenuItem";
            this.injectDllToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.injectDllToolStripMenuItem.Text = "Inject Dll";
            this.injectDllToolStripMenuItem.Click += new System.EventHandler(this.injectDllToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateStructuresToolStripMenuItem});
            this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(78, 22);
            this.toolStripDropDownButton3.Text = "Developers";
            this.toolStripDropDownButton3.Click += new System.EventHandler(this.toolStripDropDownButton3_Click);
            // 
            // generateStructuresToolStripMenuItem
            // 
            this.generateStructuresToolStripMenuItem.Name = "generateStructuresToolStripMenuItem";
            this.generateStructuresToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.generateStructuresToolStripMenuItem.Text = "Generate structures";
            this.generateStructuresToolStripMenuItem.Click += new System.EventHandler(this.generateStructuresToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Current Section:";
            // 
            // lbCurrentSection
            // 
            this.lbCurrentSection.AutoSize = true;
            this.lbCurrentSection.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrentSection.Location = new System.Drawing.Point(103, 25);
            this.lbCurrentSection.Name = "lbCurrentSection";
            this.lbCurrentSection.Size = new System.Drawing.Size(58, 13);
            this.lbCurrentSection.TabIndex = 4;
            this.lbCurrentSection.Text = "Unloaded";
            // 
            // lvInfo
            // 
            this.lvInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvInfo.FullRowSelect = true;
            this.lvInfo.GridLines = true;
            this.lvInfo.Location = new System.Drawing.Point(10, 53);
            this.lvInfo.MultiSelect = false;
            this.lvInfo.Name = "lvInfo";
            this.lvInfo.Size = new System.Drawing.Size(528, 415);
            this.lvInfo.TabIndex = 5;
            this.lvInfo.UseCompatibleStateImageBehavior = false;
            this.lvInfo.View = System.Windows.Forms.View.Details;
            this.lvInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvInfo_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 128;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 83;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Address";
            this.columnHeader3.Width = 108;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Size (bytes)";
            this.columnHeader4.Width = 90;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Type";
            this.columnHeader5.Width = 75;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // unloadToolStripMenuItem
            // 
            this.unloadToolStripMenuItem.Name = "unloadToolStripMenuItem";
            this.unloadToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.unloadToolStripMenuItem.Text = "View Modules";
            this.unloadToolStripMenuItem.Click += new System.EventHandler(this.unloadToolStripMenuItem_Click);
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 480);
            this.Controls.Add(this.lvInfo);
            this.Controls.Add(this.lbCurrentSection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(250, 300);
            this.Name = "mainWindow";
            this.Text = "PEToolkit - BahNahNah";
            this.Load += new System.EventHandler(this.mainWindow_Load);
            this.cmOpen.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmOpen;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem overviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dOSHeaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageHeaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionalPEHeaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataDirectoriesToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCurrentSection;
        private System.Windows.Forms.ListView lvInfo;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem structuresToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem generateStructuresToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton4;
        private System.Windows.Forms.ToolStripMenuItem checkForRunPEToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem injectDllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resourcesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unloadToolStripMenuItem;
    }
}