namespace MuBoxes
{
    partial class MuBoxDefault
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
            this.btn_DefaultOK = new System.Windows.Forms.Button();
            this.lbl_DefaultHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_DefaultOK
            // 
            this.btn_DefaultOK.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_DefaultOK.Location = new System.Drawing.Point(279, 235);
            this.btn_DefaultOK.Name = "btn_DefaultOK";
            this.btn_DefaultOK.Size = new System.Drawing.Size(120, 45);
            this.btn_DefaultOK.TabIndex = 0;
            this.btn_DefaultOK.Text = "OK";
            this.btn_DefaultOK.UseVisualStyleBackColor = false;
            this.btn_DefaultOK.Click += new System.EventHandler(this.btn_DefaultOK_Click);
            // 
            // lbl_DefaultHeader
            // 
            this.lbl_DefaultHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DefaultHeader.Location = new System.Drawing.Point(144, 40);
            this.lbl_DefaultHeader.Name = "lbl_DefaultHeader";
            this.lbl_DefaultHeader.Size = new System.Drawing.Size(400, 150);
            this.lbl_DefaultHeader.TabIndex = 1;
            this.lbl_DefaultHeader.Text = "Message";
            this.lbl_DefaultHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MuBoxDefault
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(678, 344);
            this.Controls.Add(this.lbl_DefaultHeader);
            this.Controls.Add(this.btn_DefaultOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MuBoxDefault";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MuBoxDefault";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_DefaultOK;
        private System.Windows.Forms.Label lbl_DefaultHeader;
    }
}