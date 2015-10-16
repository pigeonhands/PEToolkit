namespace PEViewer.Forms
{
    partial class formBuildStructure
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
            this.rtbStruct = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbStruct
            // 
            this.rtbStruct.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbStruct.Location = new System.Drawing.Point(12, 12);
            this.rtbStruct.Name = "rtbStruct";
            this.rtbStruct.ReadOnly = true;
            this.rtbStruct.Size = new System.Drawing.Size(371, 346);
            this.rtbStruct.TabIndex = 0;
            this.rtbStruct.Text = "";
            // 
            // formBuildStructure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 370);
            this.Controls.Add(this.rtbStruct);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "formBuildStructure";
            this.Text = "Structure";
            this.Load += new System.EventHandler(this.formBuildStructure_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbStruct;
    }
}