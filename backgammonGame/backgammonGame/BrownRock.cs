using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace backgammonGame
{
    public class BrownRock : BackGammonRock
    {
        private bool controlBrownSum = true;
        private bool controlDice = true;

        public bool ControlDice
        {
            get { return controlDice; }
            set { controlDice = value; }
        }
        public bool ControlBrownSum
        {
            get { return controlBrownSum; }
            set { controlBrownSum = value; }
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.BackColor = Color.FromArgb(169, 116, 79);
            this.Player = "brownPlayer";
            this.MouseDown += new MouseEventHandler(clickTo);
            this.MouseUp += new MouseEventHandler(unClickTo);
            this.LockedRock = true;
        }


        private void clickTo(object sender, MouseEventArgs e)
        {

            ControlBrownSum = true;
            ControlDice = false;
            foreach (BrownRock item in Game.brownCreate)
            {

                if (((item.Column <= 24 && item.Column >= 7) && ((item.Location.X > 75 && item.Location.X < 375) || (item.Location.X > 420 && item.Location.X < 720)))|| ((BrownRock)item).Crashed)
                    {
                        ControlBrownSum = false;
                        break;
                    }
                    if (!ControlDice && (this.Column < ((BrownRock)item).Column && !item.IsSummed) && (this.Column != Game.Dice1 || this.Column != Game.Dice2))
                     ControlDice = true; 

            }
        }
        private void unClickTo(object sender, MouseEventArgs e)
        {
                if (!this.LockedRock)
                {
                    this.columnCalculation();
                    if (ControlBrownSum)
                        game.brownSumRock(this);
                    else if (!ControlBrownSum)
                        game.rockGo(this);
                }


                gameStream.info_listBox.SelectedIndex = gameStream.info_listBox.Items.Count - 1;
                gameStream.info_listBox.SetSelected(gameStream.info_listBox.Items.Count - 1, false);

                this.Text = this.Column.ToString();

        }


    }
}
