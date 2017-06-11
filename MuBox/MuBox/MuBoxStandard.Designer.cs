namespace MuBoxes
{
    partial class MuBoxStandard
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
            this.btn_StandardOK = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lbl_Message = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_StandardOK
            // 
            this.btn_StandardOK.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_StandardOK.Location = new System.Drawing.Point(204, 235);
            this.btn_StandardOK.Name = "btn_StandardOK";
            this.btn_StandardOK.Size = new System.Drawing.Size(120, 45);
            this.btn_StandardOK.TabIndex = 0;
            this.btn_StandardOK.Text = "OK";
            this.btn_StandardOK.UseVisualStyleBackColor = false;
            this.btn_StandardOK.Click += new System.EventHandler(this.btn_StandardOK_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button2.Location = new System.Drawing.Point(354, 235);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 45);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbl_Message
            // 
            this.lbl_Message.Location = new System.Drawing.Point(163, 40);
            this.lbl_Message.Name = "lbl_Message";
            this.lbl_Message.Size = new System.Drawing.Size(352, 152);
            this.lbl_Message.TabIndex = 2;
            this.lbl_Message.Text = "Message";
            this.lbl_Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MuBoxStandard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(678, 344);
            this.Controls.Add(this.lbl_Message);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_StandardOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MuBoxStandard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MuBoxStandard";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_StandardOK;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lbl_Message;
    }
}