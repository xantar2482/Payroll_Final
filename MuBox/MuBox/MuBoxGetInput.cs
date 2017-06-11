using System;
using System.Windows.Forms;

namespace MuBoxes
{
    public partial class MuBoxGetInput : Form
    {
        public static MuBoxGetInput muBox;
        public static string textInput;
      
        public MuBoxGetInput()
        {
            InitializeComponent();           
        }

        public static string showMuBox(string titleBar, string message, int messageSize, string inputMessage)
        {
            muBox = new MuBoxGetInput();
            
            muBox.lbl_Main.Text = message;
            muBox.lbl_Main.Font = new System.Drawing.Font("Microsoft Sans Serif", messageSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            muBox.lbl_textBox.Text = inputMessage;
            muBox.Text = titleBar;
                        
            muBox.ShowDialog();                  

            return textInput;
        }
 
        private void btn_MuTextInputOK_Click(object sender, EventArgs e)
        {
            textInput = tb_GetInfo.Text.ToString();
            Dispose();
        }
    }
}
