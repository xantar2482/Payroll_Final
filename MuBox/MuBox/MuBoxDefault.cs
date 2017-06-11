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
    public partial class MuBoxDefault : Form
    {
        public static MuBoxDefault mubox;
        public static string result;

        public MuBoxDefault()
        {
            InitializeComponent();
        }

        public static string showMuBoxDefault(string titleBar, string message)
        {
            mubox = new MuBoxDefault();
            mubox.Text = titleBar;
            mubox.lbl_DefaultHeader.Text = message;
            mubox.ShowDialog();

            return result;
        }
        public static string showMuBoxDefault(string titleBar, string message, int messFontSize)
        {
            mubox = new MuBoxDefault();
            mubox.lbl_DefaultHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", messFontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            mubox.Text = titleBar;
            mubox.lbl_DefaultHeader.Text = message;
            mubox.ShowDialog();

            return result;
        }


        private void btn_DefaultOK_Click(object sender, EventArgs e)
        {
            result = "OK";
            Dispose();
        }
    }
}
