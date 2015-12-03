namespace PEToolkit.Forms
{
    partial class formViewStorageStream
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
            this.rtbStorageData = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbStorageData
            // 
            this.rtbStorageData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbStorageData.Location = new System.Drawing.Point(12, 12);
            this.rtbStorageData.Name = "rtbStorageData";
            this.rtbStorageData.ReadOnly = true;
            this.rtbStorageData.Size = new System.Drawing.Size(398, 116);
            this.rtbStorageData.TabIndex = 0;
            this.rtbStorageData.Text = "";
            // 
            // formViewStorageStream
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 140);
            this.Controls.Add(this.rtbStorageData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "formViewStorageStream";
            this.Text = "Storage Stream";
            this.Load += new System.EventHandler(this.formViewStorageStream_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbStorageData;
    }
}