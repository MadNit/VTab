namespace VTab
{
    partial class TabPageUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabPageRTBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabPageRTBox
            // 
            this.TabPageRTBox.BackColor = System.Drawing.SystemColors.Desktop;
            this.TabPageRTBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TabPageRTBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabPageRTBox.ForeColor = System.Drawing.Color.White;
            this.TabPageRTBox.Location = new System.Drawing.Point(-3, -3);
            this.TabPageRTBox.Name = "TabPageRTBox";
            this.TabPageRTBox.Size = new System.Drawing.Size(579, 270);
            this.TabPageRTBox.TabIndex = 0;
            this.TabPageRTBox.Text = "";
            this.TabPageRTBox.Enter += new System.EventHandler(this.TabPageRTBox_Enter);
            this.TabPageRTBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.richTextBox1_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TabPageRTBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(623, 274);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // TabPageUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "TabPageUserControl";
            this.Size = new System.Drawing.Size(582, 275);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox TabPageRTBox;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
