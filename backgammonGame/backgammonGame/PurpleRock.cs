using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace backgammonGame
{
    public class PurpleRock : BackGammonRock
    {
        private bool controlPurpleSum = true;
        private bool controlDice = true;

        public bool ControlDice
        {
            get { return controlDice; }
            set { controlDice = value; }
        }
        public bool ControlPurpleSum
        {
            get { return controlPurpleSum; }
            set { controlPurpleSum = value; }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.BackColor = Color.Purple;
            this.Player = "purplePlayer";
            this.MouseDown += new MouseEventHandler(clickTo);
            this.MouseUp += new MouseEventHandler(unClickTo);
            this.LockedRock = true;
        }

        private void clickTo(object sender, MouseEventArgs e)
        {
            ControlPurpleSum = true;
            ControlDice = false;
            foreach (PurpleRock item in Game.purpleCreate)
            {

                    if (((item.Column <= 18 && item.Column >=1) && ((item.Location.X > 75 && item.Location.X < 375) || (item.Location.X > 420 && item.Location.X < 720))) || ((PurpleRock)item).Crashed)
                    {
                        ControlPurpleSum = false;
                        break;
                    }
                    if (!ControlDice && (this.Column > ((PurpleRock)item).Column && !item.IsSummed) && (this.Column != Convert.ToInt16(25 - Game.Dice1) || this.Column != Convert.ToInt16(25 - Game.Dice2)))
                        ControlDice = true;
            }
        }
        private void unClickTo(object sender, MouseEventArgs e)
        {
            if (!this.LockedRock)
            {
                this.columnCalculation();
                if (ControlPurpleSum)
                    game.purpleSumRock(this);
                else if (!ControlPurpleSum)
                    game.rockGo(this);
            }
            gameStream.info_listBox.SelectedIndex = gameStream.info_listBox.Items.Count - 1;
            gameStream.info_listBox.SetSelected(gameStream.info_listBox.Items.Count - 1, false);
            this.Text = this.Column.ToString();
        }
    }
}
