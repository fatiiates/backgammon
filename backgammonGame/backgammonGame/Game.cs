using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
namespace backgammonGame
{
    public class Game
    {
        #region//DEĞİŞKENLER
        private static Point coLoc = new Point(0, 0);
        public static byte enemyBrownRock = 0;
        public static byte enemyPurpleRock = 0;
        public static byte friendBrownRock = 0;
        public static byte friendPurpleRock = 0;
        private bool rockPlayer = false;
        
        private static byte dice1;
        private static byte dice2;
        private static byte crashedBrownRock = 0;
        private static byte crashedPurpleRock = 0;
        private static byte sumBrownRock = 0;
        private static byte sumPurpleRock = 0;
        public static byte sıra = 0;
        public static byte sıra1 = 0;
        public static bool enableButton = false;
        public static short tempColumn = 0;
        public static Point tempLocation;
        public static bool tempCrash = false;
        public static string tempName = "";
        public static Size tempSize;
        public static byte tempCrashedBrownRock = 0;
        public static byte tempCrashedPurpleRock = 0;
        public static short tempLocationY = 0;
        public static bool tempCrashRock;
        public static byte tempParam = 0;
        public static byte i = 0;
        public static byte j = 0;
        public static byte z = 0;
        public static short k = 15;


        public static string[] arrayTurnName = new string[3];
        public static Size[] arrayTurnSize = new Size[3];
        public static short[] arrayTurnColumn = new short[3];
        public static bool[] arrayTurnCrashed = new bool[3];
        public static bool[] arrayTurnCrashRock = new bool[3];
        public static Point[] arrayTurnLocation = new Point[3];
        public static bool[] arrayTurnLockedRock = new bool[3];

        public static BrownRock[] brownCreate = new BrownRock[15];
        public static PurpleRock[] purpleCreate = new PurpleRock[15];

        public static short[] control = new short[15];
        public static byte ii=0;

        public static byte CrashedBrownRock
        {
            get { return crashedBrownRock; }
            set { crashedBrownRock = value; }
        }
        public static byte CrashedPurpleRock
        {
            get { return crashedPurpleRock; }
            set { crashedPurpleRock = value; }
        }
        public static byte SumBrownRock
        {
            get { return sumBrownRock; }
            set { sumBrownRock = value; }
        }
        public static byte SumPurpleRock
        {
            get { return sumPurpleRock; }
            set { sumPurpleRock = value; }
        }
        public static byte Dice1
        {
            get { return dice1; }
            set { dice1 = (byte)value; }
        }
        public static byte Dice2
        {
            get { return dice2; }
            set { dice2 = (byte)value; }
        }
        #endregion


        public static void recursiveTurnRock(byte param, bool control)
        {
            if (control)
            {

                if (param == 0)
                {
                    arrayTurnName[param] = tempName;
                    arrayTurnSize[param] = tempSize;
                    arrayTurnColumn[param] = tempColumn;
                    arrayTurnCrashed[param] = tempCrash;
                    arrayTurnCrashRock[param] = tempCrashRock;
                    arrayTurnLocation[param] = tempLocation;
                    arrayTurnLockedRock[param] = BackGammonRock.tempLockedRock;
                }
                else
                {
                    arrayTurnName[param] = arrayTurnName[param - 1];
                    arrayTurnSize[param] = arrayTurnSize[param - 1];
                    arrayTurnColumn[param] = arrayTurnColumn[param - 1];
                    arrayTurnCrashed[param] = arrayTurnCrashed[param - 1];
                    arrayTurnCrashRock[param] = arrayTurnCrashRock[param - 1];
                    arrayTurnLocation[param] = arrayTurnLocation[param - 1];
                    arrayTurnLockedRock[param] = arrayTurnLockedRock[param - 1];
                    recursiveTurnRock((byte)(param - 1), true);
                }
            }
            else if (!control)
            {
                arrayTurnName[param] = arrayTurnName[param + 1];
                arrayTurnSize[param] = arrayTurnSize[param + 1];
                arrayTurnColumn[param] = arrayTurnColumn[param + 1];
                arrayTurnCrashed[param] = arrayTurnCrashed[param + 1];
                arrayTurnCrashRock[param] = arrayTurnCrashRock[param + 1];
                arrayTurnLocation[param] = arrayTurnLocation[param + 1];
                arrayTurnLockedRock[param] = arrayTurnLockedRock[param + 1];
                if (param != 1)
                    recursiveTurnRock((byte)(param + 1), false);
            }
        }
        public static void TurnRock()
        {
            short tempCol = 0;
            bool tempSum= false;
            tempLocationY = 0;
            if (arrayTurnName[0].Substring(0, 10) == "purpleRock")
            
                foreach (PurpleRock item in purpleCreate)
                {
                    if (item.Name == arrayTurnName[0])
                    {
                        tempCol = item.Column;
                        tempSum = item.IsSummed;
                        if (arrayTurnCrashRock[0])
                        {
                            foreach (BrownRock crashItem in brownCreate)
                            {
                                if (crashItem.Crashed)
                                    if (tempLocationY <= crashItem.Location.Y)
                                    {
                                        tempLocationY = (short)crashItem.Location.Y;
                                        crashItem.Location = item.Location;
                                        crashItem.Column = item.Column;
                                        crashItem.Size = new Size(46, 46);
                                        crashItem.Crashed = false;
                                        break;
                                    }
                            }
                            CrashedBrownRock--;
                        }
                        if (item.IsSummed)
                        {
                            SumPurpleRock--;
                            item.IsSummed = false;
                        }
                        if (arrayTurnCrashed[0])
                            CrashedPurpleRock++;
                        item.Location = arrayTurnLocation[0];
                        item.Crashed = arrayTurnCrashed[0];
                        item.Size = arrayTurnSize[0];
                        item.Column = arrayTurnColumn[0];
                        item.LockedRock = arrayTurnLockedRock[0];
                        if (tempCol - arrayTurnColumn[0] == Game.Dice1)
                            sıra--;
                        else if (tempCol - arrayTurnColumn[0] == Game.Dice2)
                            sıra1--;
                        else if (arrayTurnColumn[0] == 25 - Game.Dice1 && tempSum)
                            sıra--;
                        else if (arrayTurnColumn[0] == 25 - Game.Dice2 && tempSum)
                            sıra1--;
                        else if (Game.Dice1 >= Game.Dice2 && sıra != 0)
                            sıra--;
                        else if (Game.Dice2 > Game.Dice1 && sıra1 != 0)
                            sıra1--;
                        else if (Game.Dice1 < Game.Dice2 && sıra1 == 0 && sıra != 0)
                            sıra--;
                        else if (Game.Dice2 < Game.Dice1 && sıra == 0 && sıra1 != 0)
                            sıra1--;
                        columnLocation(item);
                        friendRockPurple(item.Column);
                        friendPurpleRock--;
                        purplePress(item, item.Column);
                        friendRockPurple(tempCol);
                        purplePress(item, tempCol);
                        recursiveTurnRock((byte)(0), false);
                        if(Form1.Player1.Team == "purpleTeam")
                            Game.aboveRock(Form1.Player1);
                        else
                            Game.aboveRock(Form1.Player2);
                        changeLockedRockColor();
                        item.BackColor = Color.Purple;
                        gameStream.info_listBox.Items.Add("Mor oyuncu taşını geri aldı.");
                        break;
                    }

                }

            else if (arrayTurnName[0].Substring(0, 9) == "brownRock")
                foreach (BrownRock item in brownCreate)
                {
                    if (item.Name == arrayTurnName[0])
                    {
                        tempCol = item.Column;
                        tempSum = item.IsSummed;

                        if (arrayTurnCrashRock[0])
                        {
                            foreach (PurpleRock crashItem in purpleCreate)
                            {
                                if (crashItem.Crashed)
                                    if (tempLocationY <= crashItem.Location.Y)
                                    {
                                        tempLocationY = (short)crashItem.Location.Y;
                                        crashItem.Location = item.Location;
                                        crashItem.Column = item.Column;
                                        crashItem.Size = new Size(46, 46);
                                        crashItem.Crashed = false;
                                        break;
                                    }
                            }
                            CrashedPurpleRock--;
                        }
                        if (item.IsSummed)
                        {
                            SumBrownRock--;
                            item.IsSummed = false;
                        }
                        if (arrayTurnCrashed[0])
                            CrashedBrownRock++;
                        item.Location = arrayTurnLocation[0];
                        item.Crashed = arrayTurnCrashed[0];
                        item.Size = arrayTurnSize[0];
                        item.Column = arrayTurnColumn[0];
                        item.LockedRock = arrayTurnLockedRock[0];
                        //Form1.a += temp;
                        if (arrayTurnColumn[0] - tempCol == Game.Dice1)
                            sıra--;
                        else if (arrayTurnColumn[0] - tempCol == Game.Dice2)
                            sıra1--;
                        else if (arrayTurnColumn[0] == Game.Dice1 && tempSum)
                            sıra--;
                        else if (arrayTurnColumn[0] == Game.Dice2 && tempSum)
                            sıra1--;
                        else if (Game.Dice1 >= Game.Dice2 && sıra != 0)
                            sıra--;
                        else if (Game.Dice2 > Game.Dice1 && sıra1 != 0)
                            sıra1--;
                        else if (Game.Dice1 < Game.Dice2 && sıra1 == 0 && sıra != 0)
                            sıra--;
                        else if (Game.Dice2 < Game.Dice1 && sıra == 0 && sıra1 != 0)
                            sıra1--;
                        columnLocation(item);
                        friendRockBrown(item.Column);
                        friendBrownRock--;
                        brownPress(item, item.Column);
                        friendRockBrown(tempCol);
                        brownPress(item, tempCol);
                        recursiveTurnRock((byte)(0), false);
                        if (Form1.Player1.Team == "brownTeam")
                            Game.aboveRock(Form1.Player1);
                        else
                            Game.aboveRock(Form1.Player2);
                        changeLockedRockColor();
                        item.BackColor= Color.FromArgb(169, 116, 79);
                        gameStream.info_listBox.Items.Add("Kahverengi oyuncu taşını geri aldı.");             
                        break;
                    }

                }
            

        }


        public static void Roll()
        {
            Random rand = new Random();
            dice1 = Convert.ToByte(rand.Next(1, 7));
            dice2 = Convert.ToByte(rand.Next(1, 7));
            /* dice1 = 6;
             dice2 = 5;*/
            gameStream.info_listBox.Items.Add("Zarlar "+Game.Dice1 + "," + Game.Dice2);

        }
        public void rockGo(BackGammonRock sender)
        {
            tempSize = new Size(46, 46);
            tempCrash = false;
            if (sender.Player == "purplePlayer")
                rockPlayer = true;
            else if (sender.Player == "brownPlayer")
                rockPlayer = false;

            if (fiftyFirstZone(sender))
                sender.comeBack();
            else if (sender.Crashed)
            {
                if (rockPlayer)
                {

                    enemyRockBrown(sender.Column);
                    friendRockPurple(sender.Column);
                    if (friendPurpleRock != 0)
                        friendPurpleRock -= 1;
                    if (enemyBrownRock >= 2)
                        sender.comeBack();
                    else if (sender.Column == Game.Dice1 && !(sıra == 1 && Game.Dice1 != Game.Dice2))
                    {
                        purpleGo((PurpleRock)sender);
                        tempCrash = true;
                        tempSize = new Size(40, 40);
                        tempRAR(sender);

                    }
                    else if (sender.Column == Game.Dice2 && sıra1 == 0)
                    {
                        purpleGo((PurpleRock)sender);
                        tempCrash = true;
                        tempSize = new Size(40, 40);
                        tempRAR(sender);
                    }
                    else
                        sender.comeBack();
                }
                else if (!rockPlayer)
                {
                    enemyRockPurple(sender.Column);
                    friendRockBrown(sender.Column);
                    if (friendBrownRock != 0)
                        friendBrownRock -= 1;
                    if (enemyPurpleRock >= 2)
                        sender.comeBack();
                    else if (sender.Column == 25 - Game.Dice1)
                    {
                        brownGo((BrownRock)sender);
                        tempCrash = true;
                        tempSize = new Size(40, 40);
                        tempRAR(sender);
                        //MessageBox.Show("1");
                    }
                    else if (sender.Column == 25 - Game.Dice2 && sıra1 == 0)
                    {
                        brownGo((BrownRock)sender);
                        tempCrash = true;
                        tempSize = new Size(40, 40);
                        tempRAR(sender);
                        //MessageBox.Show("2");
                    }
                    else
                        sender.comeBack();

                }
            }
            else if (sender.newColumns[0] == sender.Column && !(sıra == 1 && Game.Dice1 != Game.Dice2))
            {
                if (rockPlayer)
                {
                    //MessageBox.Show("4");

                    enemyRockBrown(sender.Column);
                    friendRockPurple(sender.Column);
                    if (friendPurpleRock != 0)
                        friendPurpleRock -= 1;
                    if (enemyBrownRock >= 2 || (CrashedPurpleRock != 0 && !(sender.Crashed)))
                        sender.comeBack();
                    else if (sender.Column <= 24 && sender.Column >= 1)
                    {
                        purpleGo((PurpleRock)sender);
                        tempRAR(sender);
                        //MessageBox.Show("41");

                    }
                    else
                        sender.comeBack();
                }
                else if (!rockPlayer)
                {
                    //MessageBox.Show("5");
                    enemyRockPurple(sender.Column);
                    friendRockBrown(sender.Column);
                    if (friendBrownRock != 0)
                        friendBrownRock -= 1;
                    if (enemyPurpleRock >= 2 || (CrashedBrownRock != 0 && !(sender.Crashed)))
                        sender.comeBack();
                    else if (sender.Column <= 24 && sender.Column >= 1)
                    {
                        brownGo((BrownRock)sender);
                        tempRAR(sender);
                        //MessageBox.Show("51");
                    }
                    else
                        sender.comeBack();
                }
            }
            else if (sender.newColumns[1] == sender.Column && sıra1 == 0)
            {
                if (rockPlayer)
                {
                    //MessageBox.Show("6");

                    enemyRockBrown(sender.Column);
                    friendRockPurple(sender.Column);
                    if (friendPurpleRock != 0)
                        friendPurpleRock -= 1;
                    if (enemyBrownRock >= 2 || (CrashedPurpleRock != 0 && !(sender.Crashed)))
                        sender.comeBack();
                    else if (sender.Column <= 24 && sender.Column >= 1)
                    {
                        purpleGo((PurpleRock)sender);
                        tempRAR(sender);
                        //MessageBox.Show("61");

                    }
                    else
                        sender.comeBack();
                }
                else if (!rockPlayer)
                {
                    //MessageBox.Show("7");

                    enemyRockPurple(sender.Column);
                    friendRockBrown(sender.Column);
                    if (friendBrownRock != 0)
                        friendBrownRock -= 1;
                    if (enemyPurpleRock >= 2 || (CrashedBrownRock != 0 && !(sender.Crashed)))
                        sender.comeBack();
                    else if (sender.Column <= 24 && sender.Column >= 1)
                    {
                        brownGo((BrownRock)sender);
                        tempRAR(sender);
                        //MessageBox.Show("71");

                    }
                    else
                        sender.comeBack();
                }
            }
            else
                sender.comeBack();


        }

        public void purpleGo(PurpleRock sender)
        {
            tempCrashedBrownRock = CrashedBrownRock;
            if (enemyBrownRock == 1)
                crash(sender);
            columnLocation(sender);
            purplePress(sender,sender.Column);
            friendRockPurple(sender.LastColumn);
            purplePress(sender,sender.LastColumn);
            if (sender.newColumns[0] == sender.Column || (sender.Column == Game.Dice1 && sender.Crashed))
            {
                sıra += 1;
                tempColumn = Convert.ToInt16(sender.Column - Game.Dice1);
                if(sender.Crashed)
                {
                    sender.Crashed = false;
                    CrashedPurpleRock--;
                }
                if (Form1.Player1.Order)
                {
                    Game.aboveRock(Form1.Player1);
                    blankThrowDice1(Form1.Player1);
                    lockControl(Form1.Player1);
                }
                else if (Form1.Player2.Order)
                {
                    Game.aboveRock(Form1.Player2);
                    blankThrowDice1(Form1.Player2);
                    lockControl(Form1.Player2);
                }

                gameStream.info_listBox.Items.Add("Mor oyuncu "+ Game.Dice1 + " zarını oynadı.");

            }
            else if (sender.newColumns[1] == sender.Column || (sender.Column == Game.Dice2 && sender.Crashed))
            {
                sıra1 += 1;
                tempColumn = Convert.ToInt16(sender.Column - Game.Dice2);
                if (sender.Crashed)
                {
                    sender.Crashed = false;
                    CrashedPurpleRock--;
                }
                if (Form1.Player1.Order)
                {
                    Game.aboveRock(Form1.Player1);
                    blankThrowDice2(Form1.Player1);
                    lockControl(Form1.Player1);
                }
                else if (Form1.Player2.Order)
                {
                    Game.aboveRock(Form1.Player2);
                    blankThrowDice2(Form1.Player2);
                    lockControl(Form1.Player2);
                }
                gameStream.info_listBox.Items.Add("Mor oyuncu " + Game.Dice2 + " zarını oynadı.");

            }
            else
                sender.comeBack();

            if (CrashedBrownRock == tempCrashedBrownRock + 1)
                tempCrashRock = true;
            else
                tempCrashRock = false;

        }

        private static void locY(BackGammonRock sender, short param,byte friend, bool col)
        {
            if(col)
            switch (param)
            {
                case 0: sender.Location = new Point(sender.Location.X, 495 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 1:
                    sender.Location = new Point(sender.Location.X, 449 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 2:
                    sender.Location = new Point(sender.Location.X, 403 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 3:
                    sender.Location = new Point(sender.Location.X, 357 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 4:
                    sender.Location = new Point(sender.Location.X, 311 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 5:
                    sender.Location = new Point(sender.Location.X, 265 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 6:
                    sender.Location = new Point(sender.Location.X, 219 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 7:
                    sender.Location = new Point(sender.Location.X, 173 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 8:
                    sender.Location = new Point(sender.Location.X, 127 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 9:
                    sender.Location = new Point(sender.Location.X, 81 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 10:
                    sender.Location = new Point(sender.Location.X, 35 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 11:
                    sender.Location = new Point(sender.Location.X, -11 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 12:
                    sender.Location = new Point(sender.Location.X,-57 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 13:
                    sender.Location = new Point(sender.Location.X, -103 + (param * (46 - 230 / (friend + 1))));
                    break;
                case 14:
                    sender.Location = new Point(sender.Location.X, -149 + (param * (46 - 230 / (friend + 1))));
                    break;
                default:
                    break;
            }
            else if(!col)
            switch (param)
            {
                    case 1:
                        sender.Location = new Point(sender.Location.X, 68 - (param * (46 - 230 / (friend + 1))));
                        break;
                    case 2:
                        sender.Location = new Point(sender.Location.X, 114 - (param * (46 - 230 / (friend + 1))));
                        break;
                    case 3:
                        sender.Location = new Point(sender.Location.X, 160 - (param * (46 - 230 / (friend + 1))));
                        break;
                    case 4:
                        sender.Location = new Point(sender.Location.X, 206 - (param * (46 - 230 / (friend + 1))));
                        break;
                    case 5:
                        sender.Location = new Point(sender.Location.X, 252 - (param * (46 - 230 / (friend + 1))));
                        break;
                    case 6:
                        sender.Location = new Point(sender.Location.X, 298 - (param * (46 - 230 / (friend + 1))));
                        break;
                    case 7:
                        sender.Location = new Point(sender.Location.X, 344 - (param * (46 - 230 / (friend + 1))));
                        break;
                    case 8:
                        sender.Location = new Point(sender.Location.X, 390 - (param * (46 - 230 / (friend + 1))));
                        break;
                    case 9:
                        sender.Location = new Point(sender.Location.X, 436 - (param * (46 - 230 / (friend + 1))));
                        break;
                    case 10:
                        sender.Location = new Point(sender.Location.X, 482 - (param * (46 - 230 / (friend + 1))));
                        break;
                    case 11:
                        sender.Location = new Point(sender.Location.X, 528 - (param * (46 - 230 / (friend + 1))));
                        break;
                    case 12:
                        sender.Location = new Point(sender.Location.X, 574 - (param * (46 - 230 / (friend + 1))));
                        break;
                    case 13:
                        sender.Location = new Point(sender.Location.X, 620 - (param * (46 - 230 / (friend + 1))));
                        break;
                    case 14:
                        sender.Location = new Point(sender.Location.X, 666 - (param * (46 - 230 / (friend + 1))));
                        break;
                    default:
                        break;
            }

        }

        public static void purplePress(PurpleRock sender,short col)
        {
            if (col <= 24 && col >= 13)
            {
                if (friendPurpleRock >= 5)
                {

                    short[] arrayLocationY = new short[friendPurpleRock];
                    if (col != sender.Column)
                        friendPurpleRock--;
                    foreach (PurpleRock item in purpleCreate)
                        if (item.Column == col && item.Name != sender.Name)
                            if (arrayLocationY[0] == 0)
                                arrayLocationY[0] = Convert.ToInt16(item.Location.Y);
                            else
                                for (int i = 0; i < arrayLocationY.Count(); i++)
                                    if (item.Location.Y > arrayLocationY[i])
                                    {
                                        for (int j = arrayLocationY.Count() - 1; j > i; j--)
                                            arrayLocationY[j] = arrayLocationY[j - 1];
                                        arrayLocationY[i] = Convert.ToInt16(item.Location.Y);
                                        break;
                                    }
                                    else if (i == arrayLocationY.Count() - 1 && item.Location.Y < arrayLocationY[i])
                                        for (int j = 0; j < arrayLocationY.Count(); j++)
                                            if (arrayLocationY[j] == 0)
                                                arrayLocationY[j] = Convert.ToInt16(item.Location.Y);
                    foreach (PurpleRock item in purpleCreate)
                        if (item.Column == col && item.Name != sender.Name)
                            for (int i = 1; i < arrayLocationY.Count(); i++)
                                if (item.Location.Y == arrayLocationY[i])
                                {
                                    locY(item, (short)i, friendPurpleRock, true);
                                    break;
                                }
                    if (col == sender.Column)
                        sender.Location = new Point(coLoc.X, coLoc.Y - (friendPurpleRock * (230 / (friendPurpleRock + 1))));
                }
                else if (col == sender.Column)
                    sender.Location = new Point(coLoc.X, coLoc.Y - (friendPurpleRock * 46));
                Form1.a = friendPurpleRock;
            }
            else if (col <= 12 && col >= 1)
                    if (friendPurpleRock >= 5)
                    {
                    short[] arrayLocationY = new short[friendPurpleRock];
                    if (col != sender.Column)
                        friendPurpleRock--;
                    foreach (PurpleRock item in purpleCreate)
                            if (item.Column == col && item.Name != sender.Name)
                                if (arrayLocationY[0] == 0)
                                    arrayLocationY[0] = Convert.ToInt16(item.Location.Y);
                                else
                                    for (int i = 0; i < arrayLocationY.Count(); i++)
                                        if (item.Location.Y < arrayLocationY[i])
                                        {
                                            for (int j = arrayLocationY.Count() - 1; j > i; j--)
                                                arrayLocationY[j] = arrayLocationY[j - 1];
                                            arrayLocationY[i] = Convert.ToInt16(item.Location.Y);
                                            break;
                                        }
                                        else if (i == arrayLocationY.Count() - 1 && item.Location.Y > arrayLocationY[i])
                                            for (int j = 0; j < arrayLocationY.Count(); j++)
                                                if (arrayLocationY[j] == 0)
                                                {
                                                    arrayLocationY[j] = Convert.ToInt16(item.Location.Y);
                                                    break;
                                                }
                        foreach (PurpleRock item in purpleCreate)
                            if (item.Column == col && item.Name != sender.Name)
                                for (int i = 1; i < arrayLocationY.Count(); i++)
                                    if (item.Location.Y == arrayLocationY[i])
                                    {
                                        locY(item, (short)i, friendPurpleRock, false);
                                        break;
                                    }
                        if (col == sender.Column)
                            sender.Location = new Point(coLoc.X, coLoc.Y + (friendPurpleRock * (230 / (friendPurpleRock + 1))));
                    }
                    else if (col == sender.Column)
                        sender.Location = new Point(coLoc.X, coLoc.Y + (friendPurpleRock * 46));

        }
        public static void brownPress(BrownRock sender, short col)
        {
            if (col <= 24 && col >= 13)
            {
                if (friendBrownRock >= 5)
                {
                    short[] arrayLocationY = new short[friendBrownRock];
                    if (col != sender.Column)
                        friendBrownRock--;
                    foreach (BrownRock item in brownCreate)
                        if (item.Column == col && item.Name != sender.Name)
                            if (arrayLocationY[0] == 0)
                                arrayLocationY[0] = Convert.ToInt16(item.Location.Y);
                            else
                                for (int i = 0; i < arrayLocationY.Count(); i++)
                                    if (item.Location.Y > arrayLocationY[i])
                                    {
                                        for (int j = arrayLocationY.Count() - 1; j > i; j--)
                                            arrayLocationY[j] = arrayLocationY[j - 1];
                                        arrayLocationY[i] = Convert.ToInt16(item.Location.Y);
                                        break;
                                    }
                                    else if (i == arrayLocationY.Count() - 1 && item.Location.Y < arrayLocationY[i])
                                        for (int j = 0; j < arrayLocationY.Count(); j++)
                                            if (arrayLocationY[j] == 0)
                                                arrayLocationY[j] = Convert.ToInt16(item.Location.Y);
                    foreach (BrownRock item in brownCreate)
                        if (item.Column == col && item.Name != sender.Name)
                            for (int i = 1; i < arrayLocationY.Count(); i++)
                                if (item.Location.Y == arrayLocationY[i])
                                {
                                    locY(item, (short)i, friendBrownRock, true);
                                    break;
                                }
                    if (col == sender.Column)
                        sender.Location = new Point(coLoc.X, coLoc.Y - (friendBrownRock * (230 / (friendBrownRock + 1))));
                }
                else if (col == sender.Column)
                    sender.Location = new Point(coLoc.X, coLoc.Y - (friendBrownRock * 46));
            }
            else if (col <= 12 && col >= 1)
                if (friendBrownRock >= 5)
                {
                    short[] arrayLocationY = new short[friendBrownRock];
                    if (col != sender.Column)
                        friendBrownRock--;
                    foreach (BrownRock item in brownCreate)
                        if (item.Column == col && item.Name != sender.Name)
                            if (arrayLocationY[0] == 0)
                                arrayLocationY[0] = Convert.ToInt16(item.Location.Y);
                            else
                                for (int i = 0; i < arrayLocationY.Count(); i++)
                                    if (item.Location.Y < arrayLocationY[i])
                                    {
                                        for (int j = arrayLocationY.Count() - 1; j > i; j--)
                                            arrayLocationY[j] = arrayLocationY[j - 1];
                                        arrayLocationY[i] = Convert.ToInt16(item.Location.Y);
                                        break;
                                    }
                                    else if (i == arrayLocationY.Count() - 1 && item.Location.Y > arrayLocationY[i])
                                        for (int j = 0; j < arrayLocationY.Count(); j++)
                                            if (arrayLocationY[j] == 0)
                                            {
                                                arrayLocationY[j] = Convert.ToInt16(item.Location.Y);
                                                break;
                                            }
                    foreach (BrownRock item in brownCreate)
                        if (item.Column == col && item.Name != sender.Name)
                            for (int i = 1; i < arrayLocationY.Count(); i++)
                                if (item.Location.Y == arrayLocationY[i])
                                {
                                    locY(item, (short)i, friendBrownRock, false);
                                    break;
                                }
                    if (col == sender.Column)
                        sender.Location = new Point(coLoc.X, coLoc.Y + (friendBrownRock * (230 / (friendBrownRock + 1))));
                }
                else if (col == sender.Column)
                    sender.Location = new Point(coLoc.X, coLoc.Y + (friendBrownRock * 46));

        }

        public void brownGo(BrownRock sender)
        {
            tempCrashedPurpleRock = CrashedPurpleRock;
            if (enemyPurpleRock == 1)
                crash(sender);
            columnLocation(sender);
            brownPress(sender, sender.Column);
            friendRockBrown(sender.LastColumn);
            brownPress(sender, sender.LastColumn);

            if (sender.newColumns[0] == sender.Column || (sender.Column == 25 - Game.Dice1 && sender.Crashed))
            {
                sıra += 1;
                tempColumn = Convert.ToInt16(sender.Column + Game.Dice1);
                if(sender.Crashed)
                {
                    sender.Crashed = false;
                    CrashedBrownRock--;
                }
                if (Form1.Player1.Order)
                {
                    Game.aboveRock(Form1.Player1);
                    blankThrowDice1(Form1.Player1);
                    lockControl(Form1.Player1);
                }
                else if (Form1.Player2.Order)
                {
                    Game.aboveRock(Form1.Player2);
                    blankThrowDice1(Form1.Player2);
                    lockControl(Form1.Player2);
                }
                gameStream.info_listBox.Items.Add("Kahverengi oyuncu " + Game.Dice1 + " zarını oynadı." );

            }
            else if (sender.newColumns[1] == sender.Column || (sender.Column == 25 - Game.Dice2 && sender.Crashed))
            {
                sıra1 += 1;
                tempColumn = Convert.ToInt16(sender.Column + Game.Dice2);
                if (sender.Crashed)
                {
                    sender.Crashed = false;
                    CrashedBrownRock--;
                }
                if (Form1.Player1.Order)
                {
                    Game.aboveRock(Form1.Player1);
                    blankThrowDice2(Form1.Player1);
                    lockControl(Form1.Player1);
                }
                else if (Form1.Player2.Order)
                {
                    Game.aboveRock(Form1.Player2);
                    blankThrowDice2(Form1.Player2);
                    lockControl(Form1.Player2);
                }
                gameStream.info_listBox.Items.Add("Kahverengi oyuncu " + Game.Dice2 + " zarını oynadı.");

            }
            else
                sender.comeBack();

            if (CrashedPurpleRock == tempCrashedPurpleRock + 1)
                tempCrashRock = true;
            else
                tempCrashRock = false;

        }

        public void tempRAR(BackGammonRock sender)
        {
            tempLocation = sender.firstPlace;
            tempName = sender.Name;
            if (sıra != 4 && !(sıra == 1 && sıra1 == 1))
                recursiveTurnRock(2, true);

        }

        public void purpleSumRock(PurpleRock sender)
        {
            tempCrash = false;
            if (CrashedPurpleRock != 0 || (fiftyFirstZone(sender) && !((sender.Location.X + 23 >= 12 && sender.Location.X + 23 <= 75) && (sender.Location.Y + 23 >= 308 && sender.Location.Y + 23 <= 542))))
                sender.comeBack(); 
            else if ((sender.newColumns[0] == sender.Column || sender.LastColumn == 25 - Game.Dice1) && sender.LastColumn <= 24 && !(sıra == 1 && Game.Dice1 != Game.Dice2))
            {
                if (sender.ControlPurpleSum && sender.newColumns[0] == sender.Column && sender.newColumns[0] <= 24)
                    rockGo(sender);
                else if (sender.ControlPurpleSum && sender.LastColumn == 25 - Game.Dice1 && ((sender.Location.X + 23 >= 12 && sender.Location.X + 23 <= 75) && (sender.Location.Y + 23 >= 308 && sender.Location.Y + 23 <= 542)))
                {
                    sıra += 1;
                    tempColumn = sender.LastColumn;
                    tempLocation = sender.firstPlace;
                    tempName = sender.Name;
                    tempCrash = false;
                    tempCrashRock = false;
                    sumPurple(sender);
                    if (sıra != 4 && !(sıra == 1 && sıra1 == 1))
                        recursiveTurnRock(2, true);
                    gameStream.info_listBox.Items.Add("Mor oyuncu " + Game.Dice1 + " zarıyla topladı.");
                }
                else
                    sender.comeBack();
                gameStream.karakter.Items.Add(sender.ControlPurpleSum + " , PS1");

            }
            else if ((sender.newColumns[1] == sender.Column || sender.LastColumn == 25 - Game.Dice2) && sender.LastColumn <= 24 && sıra1 == 0)
            {
                if (sender.ControlPurpleSum && sender.newColumns[1] == sender.Column && sender.newColumns[1] <= 24)
                    rockGo(sender);
                else if (sender.ControlPurpleSum && sender.LastColumn == 25 - Game.Dice2 &&  ((sender.Location.X + 23 >= 12 && sender.Location.X + 23 <= 75) && (sender.Location.Y + 23 >= 308 && sender.Location.Y + 23 <= 542)))
                {
                    sıra1 += 1;
                    tempColumn = sender.LastColumn;
                    tempLocation = sender.firstPlace;
                    tempName = sender.Name;
                    tempCrash = false;
                    tempCrashRock = false;
                    sumPurple(sender);
                    if (sıra != 4 && !(sıra == 1 && sıra1 == 1))
                        recursiveTurnRock(2, true);
                    gameStream.info_listBox.Items.Add("Mor oyuncu " + Game.Dice2 + " zarıyla topladı.");

                }
                else
                    sender.comeBack();
                gameStream.karakter.Items.Add(sender.ControlPurpleSum + " , PS2");

            }
            else if ((sender.LastColumn >= 25 - Game.Dice1) && !(sıra == 1 && Game.Dice1 != Game.Dice2) && !sender.ControlDice && ((sender.Location.X + 23 >= 12 && sender.Location.X + 23 <= 75) && (sender.Location.Y + 23 >= 308 && sender.Location.Y + 23 <= 542)))
            {
                sıra += 1;
                tempColumn = sender.LastColumn;
                tempLocation = sender.firstPlace;
                tempName = sender.Name;
                tempCrash = false;
                tempCrashRock = false;
                sumPurple(sender);
                if (sıra != 4 && !(sıra == 1 && sıra1 == 1))
                    recursiveTurnRock(2, true);
                gameStream.info_listBox.Items.Add("Mor oyuncu " + Game.Dice1 + " zarıyla topladı.");

                gameStream.karakter.Items.Add(sender.ControlPurpleSum + " , PS3");


            }
            else if ((sender.LastColumn >= 25 - Game.Dice2) && !sender.ControlDice && sıra1 == 0 && ((sender.Location.X + 23 >= 12 && sender.Location.X + 23 <= 75) && (sender.Location.Y + 23 >= 308 && sender.Location.Y + 23 <= 542)))
            {
                sıra1 += 1;
                tempColumn = sender.LastColumn;
                tempLocation = sender.firstPlace;
                tempName = sender.Name;
                tempCrash = false;
                tempCrashRock = false;
                sumPurple(sender);
                if (sıra != 4 && !(sıra == 1 && sıra1 == 1))
                    recursiveTurnRock(2, true);
                gameStream.info_listBox.Items.Add("Mor oyuncu " + Game.Dice2 + " zarıyla topladı.");

                gameStream.karakter.Items.Add(sender.ControlPurpleSum + " , PS4");


            }
            else
             sender.comeBack();
        }
        public void sumPurple(PurpleRock sender)
        {
            SumPurpleRock++;

            sender.IsSummed = true;
            friendRockPurple(sender.Column);
            purplePress(sender, sender.Column);
            friendRockPurple(sender.LastColumn);
            purplePress(sender, sender.LastColumn);

            if (SumPurpleRock == 15)
            {
                
                
                DialogResult selected;
                selected = MessageBox.Show("Tebrikler! Oyunu mor takım kazandı.\nYeni oyuna başlamak ister misiniz ?", " Oyun bitti", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (selected == DialogResult.Yes)
                {
                    Game.endAndNewGame();
                    Game.NewGame(Form1.Player2);
                    gameStream.purpleScore++;
                    
                }
                else
                    Application.Exit();
            }
            else if (SumPurpleRock % 2 == 1)
            {
                sender.Size = new Size(25, 25);
                sender.Location = new Point(16, ((SumPurpleRock / 2) * 31 + 320));
                sender.LockedRock = true;
            }
            else if (SumPurpleRock % 2 == 0)
            {
                sender.Size = new Size(25, 25);
                sender.Location = new Point(44, ((SumPurpleRock / 2 - 1) * 31 + 320));
                sender.LockedRock = true;
            }
            tempSize = new Size(46, 46);
            if (sıra > 0)
            {
                if (Form1.Player1.Order)
                {
                    Game.aboveRock(Form1.Player1);
                    blankThrowDice1(Form1.Player1);
                    lockControl(Form1.Player1);
                }
                else if (Form1.Player2.Order)
                {
                    Game.aboveRock(Form1.Player2);
                    blankThrowDice1(Form1.Player2);
                    lockControl(Form1.Player2);
                }
            }
            else if(sıra1 > 0)
            {
                if (Form1.Player1.Order)
                {
                    Game.aboveRock(Form1.Player1);
                    blankThrowDice2(Form1.Player1);
                    lockControl(Form1.Player1);
                }
                else if (Form1.Player2.Order)
                {
                    Game.aboveRock(Form1.Player2);
                    blankThrowDice2(Form1.Player2);
                    lockControl(Form1.Player2);
                }
            }
            
        }

        public void brownSumRock(BrownRock sender)
        {
            tempCrash = false;
            if (CrashedBrownRock != 0 || (fiftyFirstZone(sender) && !((sender.Location.X + 23 >= 12 && sender.Location.X + 23 <= 75) && (sender.Location.Y + 23 >= 22 && sender.Location.Y + 23 <= 255))))
                sender.comeBack();
            else if ((sender.newColumns[0] == sender.Column || sender.LastColumn == Game.Dice1) && !(sıra == 1 && Game.Dice1 != Game.Dice2) && sender.LastColumn >= 1)
            {
                if (sender.ControlBrownSum && sender.newColumns[0] == sender.Column && sender.newColumns[0] >= 1)
                    rockGo(sender);
                else if (sender.ControlBrownSum && sender.LastColumn == Game.Dice1 && ((sender.Location.X + 23 >= 12 && sender.Location.X + 23 <= 75) && (sender.Location.Y + 23 >= 22 && sender.Location.Y + 23 <= 255)))
                {
                    sıra += 1;
                    tempColumn = sender.LastColumn;
                    tempLocation = sender.firstPlace;
                    tempName = sender.Name;
                    tempCrash = false;
                    tempCrashRock = false;
                    sumBrown(sender);
                    if (sıra != 4 && !(sıra == 1 && sıra1 == 1))
                        recursiveTurnRock(2, true);
                    gameStream.info_listBox.Items.Add("Kahverengi oyuncu " + Game.Dice1 + " zarıyla topladı.");


                }
                else
                    sender.comeBack();
                gameStream.karakter.Items.Add(sender.ControlBrownSum + " , bS1");

            }
            else if ((sender.newColumns[1] == sender.Column || sender.LastColumn == Game.Dice2) && sıra1 == 0 && sender.LastColumn >= 1)
            {
                if (sender.ControlBrownSum && sender.newColumns[1] == sender.Column && sender.newColumns[1] >= 1)
                    rockGo(sender);
                else if (sender.ControlBrownSum && sender.LastColumn == Game.Dice2 && ((sender.Location.X + 23 >= 12 && sender.Location.X + 23 <= 75) && (sender.Location.Y + 23 >= 22 && sender.Location.Y + 23 <= 255)))
                {
                    sıra1 += 1;
                    tempColumn = sender.LastColumn;
                    tempLocation = sender.firstPlace;
                    tempName = sender.Name;
                    tempCrash = false;
                    tempCrashRock = false;
                    sumBrown(sender);
                    if (sıra != 4 && !(sıra == 1 && sıra1 == 1))
                        recursiveTurnRock(2, true);
                    gameStream.info_listBox.Items.Add("Kahverengi oyuncu " + Game.Dice2 + " zarıyla topladı.");

                }
                else
                    sender.comeBack();
                gameStream.karakter.Items.Add(sender.ControlBrownSum + " , bS2");

            }
            else if ((sender.LastColumn <= Game.Dice1) && !sender.ControlDice && !(sıra == 1 && Game.Dice1 != Game.Dice2) && ((sender.Location.X + 23 >= 12 && sender.Location.X + 23 <= 75) && (sender.Location.Y + 23 >= 22 && sender.Location.Y + 23 <= 255)))
            {
                sıra += 1;
                tempColumn = sender.LastColumn;
                tempLocation = sender.firstPlace;
                tempName = sender.Name;
                tempCrash = false;
                tempCrashRock = false;
                sumBrown(sender);
                if (sıra != 4 && !(sıra == 1 && sıra1 == 1))
                    recursiveTurnRock(2, true);
                gameStream.info_listBox.Items.Add("Kahverengi oyuncu " + Game.Dice1 + " zarıyla topladı.");
                gameStream.karakter.Items.Add(sender.ControlBrownSum + " , bS3");


            }
            else if ((sender.LastColumn <= Game.Dice2) && sıra1 == 0 && !sender.ControlDice && ((sender.Location.X + 23 >= 12 && sender.Location.X + 23 <= 75) && (sender.Location.Y + 23 >= 22 && sender.Location.Y + 23 <= 255)))
            {
                sıra1 += 1;
                tempColumn = sender.LastColumn;
                tempLocation = sender.firstPlace;
                tempName = sender.Name;
                tempCrash = false;
                tempCrashRock = false;
                sumBrown(sender);
                if (sıra != 4 && !(sıra == 1 && sıra1 == 1))
                    recursiveTurnRock(2, true);
                gameStream.info_listBox.Items.Add("Kahverengi oyuncu " + Game.Dice2 + " zarıyla topladı.");
                gameStream.karakter.Items.Add(sender.ControlBrownSum + " , bS4");

            }
            else
                sender.comeBack(); ;
        }
        public void sumBrown(BrownRock sender)
        {
            SumBrownRock++;
            sender.IsSummed = true;
            friendRockBrown(sender.Column);
            brownPress(sender,sender.Column);

            friendRockBrown(sender.LastColumn);
            brownPress(sender, sender.LastColumn);

            if (SumBrownRock == 15)
            {
                DialogResult selected;
                selected = MessageBox.Show("Tebrikler! Oyunu kahverengi takım kazandı.\nYeni oyuna başlamak ister misiniz ?", " Oyun bitti", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (selected == DialogResult.Yes)
                {
                    Game.endAndNewGame();
                    Game.NewGame(Form1.Player1);
                    gameStream.brownScore++;
                }
                else
                    Application.Exit();
            }
            else if (SumBrownRock % 2 == 1)
            {
                sender.Size = new Size(25, 25);
                sender.Location = new Point(16, ((SumBrownRock / 2) * 31 + 32));
                sender.LockedRock = true;
            }
            else if (SumBrownRock % 2 == 0)
            {
                sender.Size = new Size(25, 25);
                sender.Location = new Point(44, ((SumBrownRock / 2 - 1) * 31 + 32));
                sender.LockedRock = true;
            }
            tempSize = new Size(46, 46);
            if (sıra > 0)
            {
                if (Form1.Player1.Order)
                {
                    Game.aboveRock(Form1.Player1);
                    blankThrowDice1(Form1.Player1);
                    lockControl(Form1.Player1);
                }
                else if (Form1.Player2.Order)
                {
                    Game.aboveRock(Form1.Player2);
                    blankThrowDice1(Form1.Player2);
                    lockControl(Form1.Player2);
                }
            }
            else if (sıra1 > 0)
            {
                if (Form1.Player1.Order)
                {
                    Game.aboveRock(Form1.Player1);
                    blankThrowDice2(Form1.Player1);
                    lockControl(Form1.Player1);
                }
                else if (Form1.Player2.Order)
                {
                    Game.aboveRock(Form1.Player2);
                    blankThrowDice2(Form1.Player2);
                    lockControl(Form1.Player2);
                }
            }
        }

        public bool fiftyFirstZone(BackGammonRock sender)
        {
            if ((sender.Location.X + 23 >= 371 && sender.Location.X + 23 <= 425) || sender.Location.Y + 23 >= 541 || sender.Location.Y <= 20 || sender.Location.X < 80 || sender.Location.X + 46 > 738)
                return true;
            if (sender.Column == sender.LastColumn)
                return true;
            return false;
        }

        public static void friendRockPurple(short col)
        {
            friendPurpleRock = 0;
            foreach (PurpleRock item in purpleCreate)
            {
                    if (item.Column == col)
                     friendPurpleRock++; 
            }
        }
        public static void friendRockBrown(short col)
        {
            friendBrownRock = 0;
            foreach (BrownRock item in brownCreate)
            {
                    if (item.Column == col)
                     friendBrownRock++; 
            }
        }

        public static void enemyRockBrown(short col)
        {
            enemyBrownRock = 0;
            foreach (BrownRock item in brownCreate)
            {
                    if (item.Column == col)
                        enemyBrownRock++;
                
            }
        }
        public static void enemyRockPurple(short col)
        {
            enemyPurpleRock = 0;
            foreach (PurpleRock item in purpleCreate)
            {
                    if (item.Column == col)
                        enemyPurpleRock++;
            }
        }

        public static void blankThrowRoll(Player player)
        {
            bool blank = false;
            byte counter = 0;
            bool control = false;
            bool columnControl = false;
            if(player.Team=="purpleTeam")
            {
                foreach (PurpleRock itemCol in purpleCreate)
                {
                    if (itemCol.Column < 19 && !itemCol.Crashed && !itemCol.IsSummed)
                        columnControl = true;
                    
                }
                foreach (PurpleRock item in purpleCreate)
                {
                    if (!item.LockedRock)
                    {

                        if (item.Crashed && !control)
                        {
                            control = true;
                            counter = 0;
                            enemyRockBrown(Game.Dice1);
                            if (enemyBrownRock >= 2)
                                counter++;
                            enemyRockBrown(Game.Dice2);
                            if (enemyBrownRock >= 2)
                                counter++;
                            if (counter == 2)
                            {
                                blank = true;
                                break;
                            }
                        }
                        else
                        {   
                            counter = 0;
                            enemyRockBrown(Convert.ToInt16(item.Column + Game.Dice1));
                            if (enemyBrownRock < 2 && ((columnControl && item.Column + Game.Dice1 < 25) || !columnControl))
                                blank = false;
                            else
                                counter++;
                            enemyRockBrown(Convert.ToInt16(item.Column + Game.Dice2));
                            if (enemyBrownRock < 2 && ((columnControl && item.Column + Game.Dice2 < 25) || !columnControl))
                                blank = false;
                            else
                                counter++;
                            if (counter == 2)
                                item.LockedRock = true;
                        }
                        if (!item.Crashed && !item.LockedRock)
                            ////MessageBox.Show("goto1");
                            if (columnControl)
                            {
                                if ((item.Column + Game.Dice1) > 24 && (item.Column + Game.Dice2) > 24)
                                        item.LockedRock = true;
                                //MessageBox.Show("goto2 " +item.LockedRock+ (item.Column + Game.Dice1) + "," + (item.Column + Game.Dice2));

                            }
                            

                    }
                }
                if (blank)
                {
                    LockedPurple();
                    //MessageBox.Show("Mor oyuncu gele attı.");

                }
            }
            else if (player.Team == "brownTeam")
            {
                    foreach (BrownRock itemCol in brownCreate)
                    {
                        if (itemCol.Column > 6 && !itemCol.Crashed && !itemCol.IsSummed)
                            columnControl = true;
                    }
                    foreach (BrownRock item in brownCreate)
                    {
                        if (!item.LockedRock)
                        {
                            if (item.Crashed && !control)
                            {
                                control = true;
                                counter = 0;
                                enemyRockPurple(Convert.ToInt16(25 - Game.Dice1));
                                if (enemyPurpleRock >= 2)
                                    counter++;
                                enemyRockPurple(Convert.ToInt16(25 - Game.Dice2));
                                if (enemyPurpleRock >= 2)
                                    counter++;
                                if (counter == 2)
                                {
                                    blank = true;
                                    break;
                                }
                            }
                            else
                            {
                                counter = 0;
                                enemyRockPurple(Convert.ToInt16(item.Column - Game.Dice1));
                                if (enemyPurpleRock < 2 && ((columnControl && item.Column + Game.Dice1 > 0) || !columnControl))
                                    blank = false;
                                else
                                    counter++;
                                enemyRockPurple(Convert.ToInt16(item.Column - Game.Dice2));
                                if (enemyPurpleRock < 2 && ((columnControl && item.Column + Game.Dice2 > 0) || !columnControl))
                                    blank = false;
                                else
                                    counter++;
                                if (counter == 2)
                                    item.LockedRock = true;
                            }
                        if (!item.Crashed && !item.LockedRock)
                           // //MessageBox.Show("goto5");
                            if (columnControl)
                            {
                                if ((item.Column - Game.Dice1) < 1 && (item.Column - Game.Dice2) < 1)
                                    item.LockedRock = true;
                                //MessageBox.Show("goto6 " + item.LockedRock + (item.Column - Game.Dice1) + "," + (item.Column - Game.Dice2));

                            }
                    }
                }
                if (blank)
                {
                    LockedBrown();
                    //MessageBox.Show("Kahverengi oyuncu gele attı.");
                }
                    
            }
            Game.changeLockedRockColor();

        }
        public static void blankThrowDice1(Player player)
        {
            // 1. ZAR OYNANDIKTAN SONRA 2. ZARIN GELE OLUP OLMADIĞINI KONTROL EDEN METOT
            bool blank = false;
            bool control = false;
            bool columnControl = false;
            if (player.Team == "purpleTeam")
            {
                foreach (PurpleRock itemCol in purpleCreate)
                {
                    if (itemCol.Column < 19 && !itemCol.Crashed && !itemCol.IsSummed)
                        columnControl = true;
                }
                foreach (PurpleRock item in purpleCreate)
                {
                    if (!item.LockedRock)
                    {
                        if(item.Crashed && !control)
                        {
                            control = true;
                            enemyRockBrown(Game.Dice2);
                            if (enemyBrownRock >= 2)
                            {
                                blank = true;
                                break;
                            }
                        }
                        else
                        {
                            enemyRockBrown(Convert.ToInt16(item.Column + Game.Dice2));
                            if (enemyBrownRock >= 2)
                                item.LockedRock = true;
                        }
                        if (!item.Crashed && !item.LockedRock)
                            ////MessageBox.Show("goto1");
                            if (columnControl)
                            {
                                if (item.Column + Game.Dice2 > 24)
                                    item.LockedRock = true;
                                //MessageBox.Show("goto2 " + item.LockedRock + (item.Column + Game.Dice1) + "," + (item.Column + Game.Dice2));

                            }

                    }

                }
                if (blank)
                {
                    foreach (PurpleRock item in purpleCreate)
                        item.LockedRock = true;
                    MessageBox.Show("Mor oyuncu gele attı.");
                }

                    
            }
            else if (player.Team == "brownTeam")
            {
                foreach (BrownRock itemCol in brownCreate)
                {
                    if (itemCol.Column > 6 && !itemCol.Crashed && !itemCol.IsSummed)
                        columnControl = true;
                }
                foreach (BrownRock item in brownCreate)
                {
                    if (!item.LockedRock)
                    {
                        if (item.Crashed && !control)
                        {
                            control = true;
                            enemyRockPurple(Convert.ToInt16(25 - Game.Dice2));
                            if (enemyPurpleRock >= 2)
                            {
                                blank = true;
                                break;
                            }
                        }
                        else
                        {
                            enemyRockPurple(Convert.ToInt16(item.Column - Game.Dice2));
                            if (enemyPurpleRock >= 2)
                                item.LockedRock = true;
                            //MessageBox.Show("134134");
                        }
                        if (!item.Crashed && !item.LockedRock)
                            // //MessageBox.Show("goto5");
                            if (columnControl)
                            {
                                if (item.Column - Game.Dice2 < 1)
                                    item.LockedRock = true;
                                //MessageBox.Show("goto6 " + item.LockedRock + (item.Column - Game.Dice1) + "," + (item.Column - Game.Dice2));

                            }
                    }
                }
                if (blank)
                { 
                    foreach (BrownRock item in brownCreate)
                        item.LockedRock = true;
                    MessageBox.Show("Kahverengi oyuncu gele attı.");
                }
            }
            Game.changeLockedRockColor();

        }
        public static void blankThrowDice2(Player player)
        {
            // 2. ZAR OYNANDIKTAN SONRA 1. ZARIN GELE OLUP OLMADIĞINI KONTROL EDEN METOT
            bool blank = false;
            bool control = false;
            bool columnControl = false;
            if (player.Team == "purpleTeam")
            {
                foreach (PurpleRock itemCol in purpleCreate)
                {
                    if (itemCol.Column < 19 && !itemCol.Crashed && !itemCol.IsSummed)
                        columnControl = true;
                }
                foreach (PurpleRock item in purpleCreate)
                {
                    if (!item.LockedRock)
                    {
                        if (item.Crashed && !control)
                        {
                            control = true;
                            enemyRockBrown(Game.Dice1);
                            if (enemyBrownRock >= 2)
                            {
                                blank = true;
                                break;
                            }
                        }
                        else
                        {
                            enemyRockBrown(Convert.ToInt16(item.Column + Game.Dice1));
                            if (enemyBrownRock >= 2)
                                item.LockedRock = true;
                        }
                        if (!item.Crashed && !item.LockedRock)
                            ////MessageBox.Show("goto1");
                            if (columnControl)
                            {
                                if (item.Column + Game.Dice1 > 24)
                                    item.LockedRock = true;
                                //MessageBox.Show("goto2 " + item.LockedRock + (item.Column + Game.Dice1) + "," + (item.Column + Game.Dice2));

                            }
                    }
                }
                if (blank)
                {
                    foreach (PurpleRock item in purpleCreate)
                        item.LockedRock = true;
                    MessageBox.Show("Mor oyuncu gele attı.");
                }
            }
            else if (player.Team == "brownTeam")
            {
                foreach (BrownRock itemCol in brownCreate)
                {
                    if (itemCol.Column > 6 && !itemCol.Crashed && !itemCol.IsSummed)
                        columnControl = true;
                }
                foreach (BrownRock item in brownCreate)
                {
                    if (!item.LockedRock)
                    {
                        if (item.Crashed && !control)
                        {
                            control = true;
                            enemyRockPurple(Convert.ToInt16(25 - Game.Dice1));
                            if (enemyPurpleRock >= 2)
                            {
                                blank = true;
                                break;
                            }
                        }
                        else
                        {
                            enemyRockPurple(Convert.ToInt16(item.Column - Game.Dice1));
                            if (enemyPurpleRock >= 2)
                                item.LockedRock = true;
                        }
                        if (!item.Crashed && !item.LockedRock)
                            // //MessageBox.Show("goto5");
                            if (columnControl)
                            {
                                if (item.Column - Game.Dice1 < 1)
                                    item.LockedRock = true;
                                //MessageBox.Show("goto6 " + item.LockedRock + (item.Column - Game.Dice1) + "," + (item.Column - Game.Dice2));

                            }

                    }
                }
                if (blank)
                {
                    foreach (BrownRock item in brownCreate)
                        item.LockedRock = true;
                    MessageBox.Show("Kahverengi oyuncu gele attı.");
                }
            }
            Game.changeLockedRockColor();

        }
        public static void lockControl(Player player)
        {
            bool control = false;
            if (player.Team == "purpleTeam")
               foreach (PurpleRock item in Game.purpleCreate)
               {
                    if (item.LockedRock == false)
                    {
                        control = true;
                        break;
                    }
               }
             else if (player.Team == "brownTeam")
               foreach (BrownRock item in Game.brownCreate)
               {
                    if (item.LockedRock == false)
                    {
                        control = true;
                        break;
                    }
               }
             if (!control)
             {
                Game.sıra = 1;
                Game.sıra1 = 1;
                MessageBox.Show("Diğer zarınız gele atışa denktir.");
             }

        }

        public void crash(BackGammonRock sender)
        {
            if (rockPlayer)
            {
                crashedBrownRock += 1;
                foreach (BrownRock item in brownCreate)
                {
                        if (item.Column == sender.Column)
                        {
                            item.Width = 40;
                            item.Height = 40;
                            item.Location = new Point(378, 213 - ((crashedBrownRock - 1) * 25));
                            item.Column = 0;
                            item.Crashed = true;
                            break;
                        }
                }
                gameStream.info_listBox.Items.Add("Mor oyuncu taş kırdı.");

            }
            else if (!rockPlayer)
            {
                crashedPurpleRock += 1;
                foreach (PurpleRock item in purpleCreate)
                {
                        if (item.Column == sender.Column)
                        {
                            item.Width = 40;
                            item.Height = 40;
                            item.Location = new Point(378, 307 + ((crashedPurpleRock - 1) * 25));
                            item.Column = 0;
                            item.Crashed = true;
                            break;
                        }
                }
                gameStream.info_listBox.Items.Add("Kahverengi oyuncu taş kırdı.");

            }
        }

        public static void LockedPurple()
        {
            foreach (PurpleRock item in purpleCreate)
            {
                    item.LockedRock = true;
            }
        }
        public static void UnlockedPurple()
        {
            foreach (PurpleRock item in purpleCreate)
            {
                if (item.Size != new Size(25, 25))
                    item.LockedRock = false;
            }
        }

        public static void LockedBrown()
        {
            foreach (BrownRock item in brownCreate)
            {
                    item.LockedRock = true;
            }
        }
        public static void UnlockedBrown()
        {
            foreach (BrownRock item in brownCreate)
            {
                if(item.Size != new Size(25,25))
                   item.LockedRock = false;
            }
        }

        public static void NewGame(Player player)
        {
            gameStream.info_listBox.Items.Add("Oyun başlıyor...");
            
            if (player.Team == "purpleTeam")
            {
                Game.UnlockedPurple();
                player.Order = true;
                Game.rockOrder();
                Game.LockedBrown();
                gameStream.info_listBox.Items.Add("Sıra Mor oyuncuda, zar atınız...");

            }
            else if (player.Team == "brownTeam")
            {
                Game.UnlockedBrown();
                player.Order = true;
                Game.rockOrder();
                Game.LockedPurple();
                gameStream.info_listBox.Items.Add("Sıra Kahverengi oyuncuda, zar atınız...");

            }

        }
        public static void endAndNewGame()
        {
            // GAME classındaki gerekli değişkenlerin resetlenmesi
            foreach (var item in typeof(Game).GetFields())
            {
                switch (item.IsStatic)
                {
                    case true:
                        switch (item.FieldType.ToString())
                        {
                            case "System.Drawing.Point":
                                item.SetValue(null, new Point(0, 0));
                                break;
                            case "System.Byte":
                                item.SetValue(null, Convert.ToByte(0));
                                break;
                            case "System.Boolean":
                                item.SetValue(null, false);
                                break;
                            case "System.Int16":
                                item.SetValue(null, Convert.ToInt16(0));
                                break;
                            case "System.String":
                                item.SetValue(null, null);
                                break;
                            case "System.Drawing.Size":
                                item.SetValue(null, new Size(0, 0));
                                break;

                        }
                        break;
                }
            }
            foreach (var item in typeof(Game).GetProperties())
                item.SetValue(null , Convert.ToByte(0));
            
            // Bağzı gerekli değişkenleri resetlenmesi
            for (int i = 0; i < 15; i++)
            {
                Game.purpleCreate[i].Crashed = false;
                Game.purpleCreate[i].IsSummed = false;
                Game.purpleCreate[i].LockedRock = false;
                Game.purpleCreate[i].Size = new Size(46, 46);
                Game.purpleCreate[i].ControlDice = true;
                Game.purpleCreate[i].ControlPurpleSum = true;

                Game.brownCreate[i].Crashed = false;
                Game.brownCreate[i].IsSummed = false;
                Game.brownCreate[i].LockedRock = false;
                Game.brownCreate[i].Size = new Size(46, 46);
                Game.brownCreate[i].ControlDice = true;
                Game.brownCreate[i].ControlBrownSum = true;

            }
            // Zar at butonu aktifleştirilmesi
            Form1.roll.Enabled = true;


        }
        public static void rockOrder()
        {
            byte Border = 1;
            byte Porder = 1;
            //foreach (PurpleRock item in purpleCreate)
            //{
            //    if (Porder <= 2)
            //    {
            //        item.Location = new Point(83, 495 - (Porder - 1) * 46);
            //        item.Column = 1;
            //    }
            //    else if (Porder > 2 && Porder <= 7)
            //    {
            //        item.Location = new Point(669, 495 - (Porder - 3) * 46);
            //        item.Column = 12;
            //    }
            //    else
            //        break;
            //    Porder++;
            //    item.columnCalculation();
            //}
            //foreach (BrownRock item in brownCreate)
            //{
            //    if (Border <= 2)
            //    {
            //        item.Location = new Point(83, 22 + (Border - 1) * 46);
            //        item.Column = 24;
            //    }
            //    else if (Border > 2 && Border <= 7)
            //    {
            //        item.Location = new Point(669, 22 + (Border - 3) * 46);
            //        item.Column = 13;
            //    }
            //    else
            //        break;
            //    Border++;
            //    item.columnCalculation();
            //}
            foreach (PurpleRock item in purpleCreate)
            {
                if (Porder <= 2)
                {
                    item.Location = new Point(83, 22 + (Porder - 1) * 46);
                    item.Column = 1;
                }
                else if (Porder > 2 && Porder <= 7)
                {
                    item.Location = new Point(669, 22 + (Porder - 3) * 46);
                    item.Column = 12;
                }
                else if (Porder > 7 && Porder <= 10)
                {
                    item.Location = new Point(476, 495 - (Porder - 8) * 46);
                    item.Column = 17;
                }
                else if (Porder > 10 && Porder <= 15)
                {
                    item.Location = new Point(322, 495 - (Porder - 11) * 46);
                    item.Column = 19;
                }
                else
                    break;
                Porder++;
                item.columnCalculation();
            }
            foreach (BrownRock item in brownCreate)
            {
                if (Border <= 2)
                {
                    item.Location = new Point(83, 495 - (Border - 1) * 46);
                    item.Column = 24;
                }
                else if (Border > 2 && Border <= 7)
                {
                    item.Location = new Point(669, 495 - (Border - 3) * 46);
                    item.Column = 13;
                }
                else if (Border > 7 && Border <= 10)
                {
                    item.Location = new Point(476, 22 + (Border - 8) * 46);
                    item.Column = 8;
                }
                else if (Border > 10 && Border <= 15)
                {
                    item.Location = new Point(322, 22 + (Border - 11) * 46);
                    item.Column = 6;
                }
                else
                    break;
                Border++;
                item.columnCalculation();

            }
        }

        public static void aboveRock(Player player)
        {
            short crashedTempLocationY = 0;
            short columnTempLocationY = 0;

            ArrayList tempList = new ArrayList();
            bool tempCol ;
            tempList.Clear();
            if (player.Team == "purpleTeam")
            {
                foreach (PurpleRock item in purpleCreate)
                {
                    tempCol = false;
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (Convert.ToInt16(tempList[i]) == item.Column)
                        {
                            tempCol = true;
                            break;
                        }
                    }
                    if (item.Crashed)
                    {
                        if (item.Location.Y > crashedTempLocationY)
                            crashedTempLocationY = (short)item.Location.Y;
                        else if (crashedTempLocationY == 0)
                            crashedTempLocationY = (short)item.Location.Y;
                    }
                    else if (item.Size == new Size(25, 25))
                        continue;
                    else if (!tempCol)
                    {
                        tempList.Add(item.Column);
                        columnTempLocationY = (short)item.Location.Y;
                        foreach (PurpleRock ColumnItem in purpleCreate)
                        {
                            if (item.Column == ColumnItem.Column && (item.Column >= 1 && item.Column <= 12) && ColumnItem.Location.Y >= columnTempLocationY)
                                columnTempLocationY = (short)ColumnItem.Location.Y;
                            else if (item.Column == ColumnItem.Column && (item.Column >= 13 && item.Column <= 24) && ColumnItem.Location.Y <= columnTempLocationY)
                                columnTempLocationY = (short)ColumnItem.Location.Y;
                        }
                        foreach (PurpleRock LockedItem in purpleCreate)
                        {
                            if (LockedItem.Location.Y != columnTempLocationY && Convert.ToInt16(tempList[tempList.Count - 1]) == LockedItem.Column)
                                LockedItem.LockedRock = true;
                            else if (Convert.ToInt16(tempList[tempList.Count - 1]) == LockedItem.Column)
                                LockedItem.LockedRock = false;
                        }
                    }
                }
            }
            else if (player.Team == "brownTeam")
                foreach (BrownRock item in brownCreate)
                {
                    tempCol = false;
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (Convert.ToInt16(tempList[i]) == item.Column)
                        {
                            tempCol = true;
                            break;
                        }
                    }
                    if (item.Crashed)
                    {
                        if (item.Location.Y > crashedTempLocationY)
                            crashedTempLocationY = (short)item.Location.Y;
                        else if (crashedTempLocationY == 0)
                            crashedTempLocationY = (short)item.Location.Y;
                    }
                    else if (item.Size == new Size(25, 25))
                        continue;
                    else if (!tempCol)
                    {
                        tempList.Add(item.Column);
                        columnTempLocationY = (short)item.Location.Y;
                        foreach (BrownRock ColumnItem in brownCreate)
                        {
                            if (item.Column == ColumnItem.Column && (item.Column >= 1 && item.Column <= 12) && ColumnItem.Location.Y >= columnTempLocationY)
                                columnTempLocationY = (short)ColumnItem.Location.Y;
                            else if (item.Column == ColumnItem.Column && (item.Column >= 13 && item.Column <= 24) && ColumnItem.Location.Y <= columnTempLocationY)
                                columnTempLocationY = (short)ColumnItem.Location.Y;
                        }
                        foreach (BrownRock LockedItem in brownCreate)
                        {

                            if (LockedItem.Location.Y != columnTempLocationY && Convert.ToInt16(tempList[tempList.Count - 1]) == LockedItem.Column)
                                LockedItem.LockedRock = true;
                            else if (Convert.ToInt16(tempList[tempList.Count - 1]) == LockedItem.Column)
                                LockedItem.LockedRock = false;

                        }
                    }

                }
        }
            /*else if (Form1.Player2.Order)
                if (Form1.Player2.Team == "purpleTeam")
                    foreach (PurpleRock item in purpleCreate)
                    {
                        tempCol = false;
                        for (int i = 0; i<tempList.Count; i++)
                        {
                            if (Convert.ToInt16(tempList[i]) == item.Column)
                            {
                                tempCol = true;
                                break;
                            }
}
                        if (item.Crashed)
                        {
                            if (item.Location.Y > crashedTempLocationY)
                                crashedTempLocationY = (short) item.Location.Y;
                            else if (crashedTempLocationY == 0)
                                crashedTempLocationY = (short) item.Location.Y;
                        }
                        else if (item.Size == new Size(25, 25))
                            continue;
                        else if (!tempCol)
                        {
                            tempList.Add(item.Column);
                            columnTempLocationY = (short) item.Location.Y;
                            foreach (PurpleRock ColumnItem in purpleCreate)
                            {                                
                                 if (item.Column == ColumnItem.Column && (item.Column >= 1 && item.Column <= 12) && ColumnItem.Location.Y >= columnTempLocationY)
                                    columnTempLocationY = (short) ColumnItem.Location.Y;
                                else if (item.Column == ColumnItem.Column && (item.Column >= 13 && item.Column <= 24) && ColumnItem.Location.Y <= columnTempLocationY)
                                    columnTempLocationY = (short) ColumnItem.Location.Y;
                            }
                            foreach (PurpleRock LockedItem in purpleCreate)
                            {
                                if (LockedItem.Location.Y != columnTempLocationY && Convert.ToInt16(tempList[tempList.Count - 1]) == LockedItem.Column)
                                    LockedItem.LockedRock = true;
                                else if (Convert.ToInt16(tempList[tempList.Count - 1]) == LockedItem.Column)
                                    LockedItem.LockedRock = false;

                            }

                        }

                    }
                if (Form1.Player2.Team == "brownTeam")
                    foreach (BrownRock item in brownCreate)
                    {
                        tempCol = false;
                        for (int i = 0; i<tempList.Count; i++)
                        {
                            if (Convert.ToInt16(tempList[i]) == item.Column)
                            {
                                tempCol = true;
                                break;
                            }
                        }
                        if (item.Crashed)
                        {
                            if (item.Location.Y > crashedTempLocationY)
                                crashedTempLocationY = (short) item.Location.Y;
                            else if (crashedTempLocationY == 0)
                                crashedTempLocationY = (short) item.Location.Y;
                        }
                        else if (item.Size == new Size(25, 25))
                            continue;
                        else if (!tempCol)
                        {
                            tempList.Add(item.Column);
                            columnTempLocationY = (short) item.Location.Y;
                            foreach (BrownRock ColumnItem in brownCreate)
                            {
                                if (item.Column == ColumnItem.Column && (item.Column >= 1 && item.Column <= 12) && ColumnItem.Location.Y >= columnTempLocationY)
                                    columnTempLocationY = (short) ColumnItem.Location.Y;
                                else if (item.Column == ColumnItem.Column && (item.Column >= 13 && item.Column <= 24) && ColumnItem.Location.Y <= columnTempLocationY)
                                    columnTempLocationY = (short) ColumnItem.Location.Y;
                            }
                            foreach (BrownRock LockedItem in brownCreate)
                            {
                                if (LockedItem.Location.Y != columnTempLocationY && Convert.ToInt16(tempList[tempList.Count - 1]) == LockedItem.Column)
                                    LockedItem.LockedRock = true;
                                else if (Convert.ToInt16(tempList[tempList.Count - 1]) == LockedItem.Column)
                                    LockedItem.LockedRock = false;

                        }

                    }

                }*/

        public static void changeLockedRockColor() {

            foreach (BrownRock item in brownCreate)
                if (item.LockedRock)
                    item.BackColor = Color.FromArgb(222, 184, 135);
                else
                    item.BackColor = Color.FromArgb(169, 116, 79);
            foreach (PurpleRock item in purpleCreate)
                if (item.LockedRock)
                    item.BackColor = Color.FromArgb(216, 150, 216);
                else
                    item.BackColor = Color.Purple;
        }

        public static void team()
        {
            if (Form1.Player1.Team == "brownTeam")
            {
                Form1.Player1.Order = true;
                Form1.Player2.Order = false;
            }
            else if (Form1.Player1.Team == "purpleTeam")
            {
                Form1.Player1.Order = false;
                Form1.Player2.Order = true;
            }

        }
        public static void CreateRock()
        {
            for (int i = 0; i < 15; i++)
            {
                //BROWN
                brownCreate[i] = new BrownRock();
                brownCreate[i].Text = "";
                brownCreate[i].Size = new Size(46, 46);
                brownCreate[i].Location = new Point(0, 0);
                brownCreate[i].Name = "brownRock" + (i + 1).ToString();
                //brownCreate[i].FlatStyle = FlatStyle.Flat;
                Form1.Value.Add(brownCreate[i]);
                //PURPLE
                purpleCreate[i] = new PurpleRock();
                purpleCreate[i].Text = "";
                purpleCreate[i].Size = new Size(46, 46);
                purpleCreate[i].Location = new Point(0, 0);
                purpleCreate[i].Name = "purpleRock" + (i + 1).ToString();
                Form1.Value.Add(purpleCreate[i]);
                //PurpleRock br = new PurpleRock();
                //br.Size = new Size(50, 50);
                //br.Location = new Point(83, 22);
                //this.Controls.Add(br);

            }

        }
        public static void columnLocation(BackGammonRock sender)
        {
            switch (sender.Column)
            {
                case 1: coLoc = new Point(83, 22); break;
                case 2: coLoc = new Point(132, 22); break;
                case 3: coLoc = new Point(179, 22); break;
                case 4: coLoc = new Point(227, 22); break;
                case 5: coLoc = new Point(274, 22); break;
                case 6: coLoc = new Point(322, 22); break;
                case 7: coLoc = new Point(428, 22); break;
                case 8: coLoc = new Point(476, 22); break;
                case 9: coLoc = new Point(523, 22); break;
                case 10: coLoc = new Point(572, 22); break;
                case 11: coLoc = new Point(620, 22); break;
                case 12: coLoc = new Point(669, 22); break;
                case 13: coLoc = new Point(669, 495); break;
                case 14: coLoc = new Point(620, 495); break;
                case 15: coLoc = new Point(572, 495); break;
                case 16: coLoc = new Point(523, 495); break;
                case 17: coLoc = new Point(476, 495); break;
                case 18: coLoc = new Point(428, 495); break;
                case 19: coLoc = new Point(322, 495); break;
                case 20: coLoc = new Point(274, 495); break;
                case 21: coLoc = new Point(227, 495); break;
                case 22: coLoc = new Point(179, 495); break;
                case 23: coLoc = new Point(132, 495); break;
                case 24: coLoc = new Point(83, 495); break;
                default:
                    break;
            }

        }
    }

}