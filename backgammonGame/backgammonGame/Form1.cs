using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
namespace backgammonGame
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static Control.ControlCollection Value;
        public static Button roll = new Button();
        public static Player Player1 = new Player();
        public static Player Player2 = new Player();
        public static string des;
        public static string des1;
        public static string des2;
        public static string des3;

        public static int a;
        public Game game = new Game();
        gameStream gameStr = new gameStream();

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1.Value = this.Controls;

            gameStr.WindowState = FormWindowState.Normal;
            gameStr.StartPosition = FormStartPosition.Manual;
            gameStr.Location = new Point(this.Location.X+this.Size.Width,this.Location.Y);
            gameStr.Show();

            Form1.roll.Text = "Zar At";
            Form1.roll.Size = new Size(90,48);
            Form1.roll.Location = new Point(525,260);
            Form1.roll.FlatStyle = FlatStyle.Flat;
            Form1.roll.FlatAppearance.BorderSize = 0;
            Form1.roll.MouseClick += new MouseEventHandler(roll_Click);
            Form1.Value.Add(Form1.roll);

            Player1.Team = "brownTeam";
            Player2.Team = "purpleTeam";
            Game.team();
            Game.CreateRock();
            for (int i = 0; i < 15; i++)
            {
                Game.brownCreate[i].MouseUp += new MouseEventHandler(WhoNext);
                Game.purpleCreate[i].MouseUp += new MouseEventHandler(WhoNext);
                Game.brownCreate[i].MouseUp += new MouseEventHandler(turnButtonVisible);
                Game.purpleCreate[i].MouseUp += new MouseEventHandler(turnButtonVisible);

            }
            for (int i = 0; i < 3; i++)
            {
                Game.arrayTurnColumn[i] = 0;
                Game.arrayTurnCrashed[i] = false;
                Game.arrayTurnCrashRock[i] = false;
                Game.arrayTurnLocation[i] = new Point(0,0);
                Game.arrayTurnName[i] = "";
                Game.arrayTurnSize[i] = new Size(0, 0);
            }
            Game.NewGame(Form1.Player1);
        }

        private void WhoNext(object sender, MouseEventArgs e)
        {
            if (Form1.Player2.Order && ((Game.sıra == 1 && Game.sıra1 == 1) || Game.sıra == 4))
            {
                Game.Dice1 = 0;
                Game.Dice2 = 0;
                Game.LockedPurple();
                Game.UnlockedBrown();
                Game.sıra = 0;
                Game.sıra1 = 0;
                Form1.Player1.Order = true;
                Form1.Player2.Order = false;
                Form1.roll.Enabled = true;
                Game.changeLockedRockColor();
                gameStream.info_listBox.Items.Add("Sıra Kahverengi oyuncuda, zar atınız...");
            }
            else if (Form1.Player1.Order && ((Game.sıra == 1 && Game.sıra1 == 1) || Game.sıra == 4))
            {
                Game.Dice1 = 0;
                Game.Dice2 = 0;
                Game.LockedBrown();
                Game.UnlockedPurple();
                Game.sıra = 0;
                Game.sıra1 = 0;
                Form1.Player2.Order = true;
                Form1.Player1.Order = false;
                Form1.roll.Enabled = true;
                Game.changeLockedRockColor();
                gameStream.info_listBox.Items.Add("Sıra Mor oyuncuda, zar atınız...");

            }
        }
        public void WhoNext()
        {
            if (Form1.Player2.Order && ((Game.sıra == 1 && Game.sıra1 == 1) || Game.sıra == 4))
            {
                Game.Dice1 = 0;
                Game.Dice2 = 0;
                Game.LockedPurple();
                Game.UnlockedBrown();
                Game.sıra = 0;
                Game.sıra1 = 0;
                Form1.Player1.Order = true;
                Form1.Player2.Order = false;
                Form1.roll.Enabled = true;
                Game.changeLockedRockColor();
                gameStream.info_listBox.Items.Add("Sıra Kahverengi oyuncuda, zar atınız...");

            }
            else if (Form1.Player1.Order && ((Game.sıra == 1 && Game.sıra1 == 1) || Game.sıra == 4))
            {
                Game.Dice1 = 0;
                Game.Dice2 = 0;
                Game.LockedBrown();
                Game.UnlockedPurple();
                Game.sıra = 0;
                Game.sıra1 = 0;
                Form1.Player2.Order = true;
                Form1.Player1.Order = false;
                Form1.roll.Enabled = true;
                Game.changeLockedRockColor();
                gameStream.info_listBox.Items.Add("Sıra Mor oyuncuda, zar atınız...");

            }
        }

        private void turnButtonVisible(object sender, MouseEventArgs e)
            {
                if (Game.sıra >= 1 || (Game.sıra1 == 1 && Game.sıra == 0))
                    turnRock.Visible = true;
                else if (Game.sıra == 0 && Game.sıra1 == 0)
                    turnRock.Visible = false;
            }

        private void turnRock_Click(object sender, EventArgs e)
        {

            Game.TurnRock();
            if (Form1.Player1.Order)
                Game.aboveRock(Form1.Player1);
            if (Form1.Player2.Order)
                Game.aboveRock(Form1.Player2);

            if (Game.sıra >= 1 || (Game.sıra1 == 1 && Game.sıra == 0))
                turnRock.Visible = true;
            else if (Game.sıra == 0 && Game.sıra1 == 0)
                turnRock.Visible = false;
            gameStream.info_listBox.SelectedIndex = gameStream.info_listBox.Items.Count - 1;
            gameStream.info_listBox.SetSelected(gameStream.info_listBox.Items.Count - 1, false);
        }

        private void roll_Click(object sender, MouseEventArgs e)
        {
            //Game.Dice1 = Convert.ToByte(textBox1.Text);
            //Game.Dice2 = Convert.ToByte(textBox2.Text);
            bool control = false;
            Game.Roll();
            Form1.roll.Enabled = false;
            string oyuncu="";
            if (Form1.Player1.Order)
            {
                Game.aboveRock(Form1.Player1);
                Game.blankThrowRoll(Form1.Player1);
                Game.changeLockedRockColor();
            }
            if (Form1.Player2.Order)
            {
                Game.aboveRock(Form1.Player2);
                Game.blankThrowRoll(Form1.Player2);
                Game.changeLockedRockColor();
            }
            if (Form1.Player1.Order)
            {
                if (Form1.Player1.Team == "purpleTeam")
                    oyuncu = "Mor";
                else
                    oyuncu = "Kahverengi";
                if (Form1.Player1.Team == "purpleTeam")
                    foreach (PurpleRock item in Game.purpleCreate)
                    {
                        if (item.LockedRock == false)
                        {
                            control = true;
                            break;
                        }
                    }
                else if (Form1.Player1.Team == "brownTeam")
                    foreach (BrownRock item in Game.brownCreate)
                    {
                        if (item.LockedRock == false)
                        {
                            control = true;
                            break;
                        }
                    }
            }
            else if (Form1.Player2.Order)
            {
                if (Form1.Player2.Team == "purpleTeam")
                    oyuncu = "Mor";
                else
                    oyuncu = "Kahverengi";
                if (Form1.Player2.Team == "purpleTeam")
                    foreach (PurpleRock item in Game.purpleCreate)
                    {
                        if (item.LockedRock == false)
                        {
                            control = true;
                            break;
                        }
                    }
                else if (Form1.Player2.Team == "brownTeam")
                    foreach (BrownRock item in Game.brownCreate)
                    {
                        if (item.LockedRock == false)
                        {
                            control = true;
                            break;
                        }
                    }
            }
            gameStream.info_listBox.SelectedIndex = gameStream.info_listBox.Items.Count - 1;
            gameStream.info_listBox.SetSelected(gameStream.info_listBox.Items.Count - 1, false);

            if (!control)
            {
                Game.sıra = 1;
                Game.sıra1 = 1;
                MessageBox.Show(oyuncu+" oyuncu gele attı.");

                WhoNext();
            }
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            gameStr.Location = new Point(this.Location.X + this.Size.Width, this.Location.Y);

        }

    }
}

