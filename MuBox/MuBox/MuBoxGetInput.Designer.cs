namespace MuBoxes
{
    partial class MuBoxGetInput
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
            this.btn_MuTextInputOK = new System.Windows.Forms.Button();
            this.tb_GetInfo = new System.Windows.Forms.TextBox();
            this.lbl_textBox = new System.Windows.Forms.Label();
            this.lbl_Main = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_MuTextInputOK
            // 
            this.btn_MuTextInputOK.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_MuTextInputOK.Location = new System.Drawing.Point(279, 235);
            this.btn_MuTextInputOK.Name = "btn_MuTextInputOK";
            this.btn_MuTextInputOK.Size = new System.Drawing.Size(120, 45);
            this.btn_MuTextInputOK.TabIndex = 0;
            this.btn_MuTextInputOK.Text = "OK";
            this.btn_MuTextInputOK.UseVisualStyleBackColor = false;
            this.btn_MuTextInputOK.Click += new System.EventHandler(this.btn_MuTextInputOK_Click);
            // 
            // tb_GetInfo
            // 
            this.tb_GetInfo.Location = new System.Drawing.Point(240, 168);
            this.tb_GetInfo.Name = "tb_GetInfo";
            this.tb_GetInfo.Size = new System.Drawing.Size(193, 26);
            this.tb_GetInfo.TabIndex = 1;
            // 
            // lbl_textBox
            // 
            this.lbl_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_textBox.Location = new System.Drawing.Point(29, 168);
            this.lbl_textBox.Name = "lbl_textBox";
            this.lbl_textBox.Size = new System.Drawing.Size(200, 24);
            this.lbl_textBox.TabIndex = 2;
            this.lbl_textBox.Text = "message";
            this.lbl_textBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Main
            // 
            this.lbl_Main.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Main.Location = new System.Drawing.Point(181, 32);
            this.lbl_Main.Name = "lbl_Main";
            this.lbl_Main.Size = new System.Drawing.Size(316, 104);
            this.lbl_Main.TabIndex = 3;
            this.lbl_Main.Text = "MainLabel";
            this.lbl_Main.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MuBoxGetInput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(678, 344);
            this.Controls.Add(this.lbl_Main);
            this.Controls.Add(this.lbl_textBox);
            this.Controls.Add(this.tb_GetInfo);
            this.Controls.Add(this.btn_MuTextInputOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MuBoxGetInput";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MuBoxGetInput";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_MuTextInputOK;
        private System.Windows.Forms.TextBox tb_GetInfo;
        private System.Windows.Forms.Label lbl_textBox;
        private System.Windows.Forms.Label lbl_Main;
    }
}