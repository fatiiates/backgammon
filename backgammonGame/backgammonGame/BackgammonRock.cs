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
    public class BackGammonRock : Button
    {
        #region // DEĞİŞKENLER
        private bool isDragged = false;
        private Point ptOffset;
        private short rockLocation = 0;
        private short locationI = 0;
        private short column = 0;
        private bool lockedRock;
        private Color mouseDownBack;
        private string player;
        private Size lastSize;
        private bool crashed;
        private bool isSummed = false;
        private static byte sayac = 0;

        public static bool tempLockedRock = false;
        public Game game = new Game();
        public Point firstPlace;
        public int[] newColumns = new int[2];
        private short lastColumn;

        public bool IsSummed
        {
            get { return isSummed; }
            set { isSummed = value; }
        }
        public bool Crashed
        {
            get { return crashed; }
            set { crashed = value; }
        }
        public string Player
        {
            get { return player; }
            set { player = value; }
        }
        public bool LockedRock
        {
            get { return lockedRock; }
            set { lockedRock = value; }
        }
        public short Column
        {
            get { return column; }
            set { column = value; }
        }
        public short LastColumn
        {
            get { return lastColumn; }
            set { lastColumn = value; }
        }

        public int LocationI
        {
            get { return locationI; }
            set { locationI = (short)value; }
        }
        public int RockLocation
        {
            get { return rockLocation; }
            set { rockLocation = (short)value; }
        }
        #endregion

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(new Rectangle(new Point(), this.Size));
            this.Region = new Region(gp);
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            mouseDownBack = this.FlatAppearance.MouseDownBackColor;
            this.FlatAppearance.MouseDownBackColor = this.BackColor;
            this.Text = "";
            this.MouseDown += new MouseEventHandler(clickTo);
            this.MouseUp += new MouseEventHandler(unClickTo);
            this.MouseMove += new MouseEventHandler(moveTo);
            
            columnCalculation();
            rockLocation = Convert.ToInt16(this.Location.X + 23);
            this.ControlAdded += LockedRockChanged;

            this.Text = "";
        }

        private void LockedRockChanged(object sender, ControlEventArgs e)
        {
            if(LockedRock == true)
                
            throw new NotImplementedException();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(new Rectangle(new Point(), this.Size));
            this.Region = new Region(gp);
        }

        private void clickTo(object sender, MouseEventArgs e)
        {
            gameStream.karakter.Items.Clear();
            sayac++;
            if (sayac > 1)
                this.comeBack();
            columnCalculation();
            lastSize = this.Size;
            LastColumn = this.column;
            tempLockedRock = this.LockedRock;
            if (this.player == "brownPlayer" && !this.Crashed)
            {
                newColumns[0] = this.Column - Game.Dice1;
                newColumns[1] = this.Column - Game.Dice2;
            }
            else if (this.player == "purplePlayer" && !this.Crashed)
            {
                newColumns[0] = this.Column + Game.Dice1;
                newColumns[1] = this.Column + Game.Dice2;
            }
            firstPlace = new Point(this.Location.X, this.Location.Y);
            if (LockedRock == false)
            {
                this.Size = new Size(46, 46);
                this.FlatAppearance.MouseDownBackColor = mouseDownBack;
                if (e.Button == MouseButtons.Left)
                {
                    isDragged = true;
                    Point ptStartPosition = this.PointToScreen(new Point(e.X, e.Y));

                    ptOffset = new Point();
                    ptOffset.X = this.Location.X - ptStartPosition.X;
                    ptOffset.Y = this.Location.Y - ptStartPosition.Y;
                }
                else
                {
                    isDragged = false;
                }
            }
            else if (LockedRock == true)
                this.FlatAppearance.MouseDownBackColor = this.BackColor;
        }
        private void unClickTo(object sender, MouseEventArgs e)
        {
            sayac = 0;
            isDragged = false;
            
            //columnCalculation();

            //game.rockGo(this);
        }
        //private void rightClick(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //        comeBack();
        //}
        private void moveTo(object sender, MouseEventArgs e)
        {
            if (isDragged)
            {
                Point newPoint = this.PointToScreen(new Point(e.X, e.Y));
                newPoint.Offset(ptOffset);
                this.Location = newPoint;
            }

        }

        public void columnCalculation()
        {
            locationI = 83;
            column = 1;
            rockLocation = Convert.ToInt16(this.Location.X + 23);
            while (true)
            {
                if (column == 7)
                    locationI += 65;
                if (this.Location.Y >= 280)
                {
                    locationI = 714;
                    column = 13;
                    while (true)
                    {
                        if (column == 19)
                        {
                            locationI -= 65;
                        }
                        if (rockLocation <= locationI && rockLocation >= (locationI - 47))
                        {
                            break;
                        }
                        //if (column > 30)
                        //{
                        //    comeBack();
                        //    break;
                        //}
                        column++;
                        locationI -= 47;
                    }
                    break;
                }
                else if (rockLocation >= locationI && rockLocation <= (locationI + 47))
                {

                    break;
                }

                if (!(this.Location.Y >= 280))
                {
                    column++;
                    locationI += 47;
                }


            }

        }

        public void comeBack()
        {
            this.Location = firstPlace;
            this.Column = lastColumn;
            this.Size = lastSize;
        }

    }
}
