namespace Instant.Forms
{
    partial class CaptureForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaptureForm));
            this.snapshotPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.snapshotPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // snapshotPictureBox
            // 
            this.snapshotPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.snapshotPictureBox.Location = new System.Drawing.Point(0, 0);
            this.snapshotPictureBox.Name = "snapshotPictureBox";
            this.snapshotPictureBox.Size = new System.Drawing.Size(384, 361);
            this.snapshotPictureBox.TabIndex = 0;
            this.snapshotPictureBox.TabStop = false;
            // 
            // CaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.snapshotPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CaptureForm";
            this.Text = "Instant";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.snapshotPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox snapshotPictureBox;
    }
}