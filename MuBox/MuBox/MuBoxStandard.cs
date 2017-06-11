using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuBoxes
{
    public partial class MuBoxStandard : Form
    {
        private static MuBoxStandard muBox;
        public static string MuResult { get; set; }

        public MuBoxStandard()
        {
            InitializeComponent();
        }

        public static string showMuBoxStandard(string titleBar, string message)
        {
            muBox = new MuBoxStandard();
            muBox.Text = titleBar;
            muBox.lbl_Message.Text = message;
            muBox.ShowDialog();
            
            return MuResult;
        }

        public static string showMuBoxStandard(string titleBar, string message, int messFontSize)
        {
            muBox = new MuBoxStandard();
            muBox.lbl_Message.Font = new System.Drawing.Font("Microsoft Sans Serif", messFontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            muBox.Text = titleBar;
            muBox.lbl_Message.Text = message;
            muBox.ShowDialog();

            return MuResult;
        }

        private void btn_StandardOK_Click(object sender, EventArgs e)
        {
            MuResult = "OK";
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MuResult = "Cancel";
            Dispose();
        }
    }
}
