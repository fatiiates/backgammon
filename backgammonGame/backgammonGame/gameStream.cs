using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace backgammonGame
{
    public partial class gameStream : Form
    {
        public gameStream()
        {
            InitializeComponent();
        }
        public static Control.ControlCollection Val;
        public static ListBox info_listBox = new ListBox();
        public static ListBox karakter = new ListBox();
        public static Label brownInfo = new Label();
        public static Label purpleInfo = new Label();
        public static byte brownScore = 0;
        public static byte purpleScore = 0;

        private void gameStream_Load(object sender, EventArgs e)
        {
            gameStream.Val = this.Controls;

            info_listBox.Size = new Size(278, 238);
            info_listBox.Location = new Point(12,12);
            info_listBox.Font = new Font("Arial",10,FontStyle.Regular);
            info_listBox.BackColor = Color.White;
            info_listBox.ForeColor = Color.Black;
            gameStream.Val.Add(info_listBox);

            brownInfo.Font = new Font("Arial",12,FontStyle.Regular);
            brownInfo.Location = new Point(125, 260);
            brownInfo.Text = brownScore.ToString();
            gameStream.Val.Add(brownInfo);

            purpleInfo.Font = new Font("Arial", 12, FontStyle.Regular);
            purpleInfo.Location = new Point(125, 285);
            purpleInfo.Text = purpleScore.ToString();
            gameStream.Val.Add(purpleInfo);

        }

        private void gameStream_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
