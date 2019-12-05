using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace TrainScheme1
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        TrainBehaviour trainBehaviour = new TrainBehaviour();
        string[] prevHex = new string[240];

        public Form1()
        {
            InitializeComponent();


            //Random r = new Random();
            //string lol = "FxFF 9 ";

            //for (int i = 0; i < 240; i++)
            //{
            //    int rr = r.Next(0, 5);
            //    switch (i)
            //    {
            //        case 0:
            //            lol += "ffffff";
            //            break;
            //        case 69:
            //            lol += "f036a6";
            //            break;
            //        case 112:
            //            lol += "123456";
            //            break;
            //        case 85:
            //            lol += "abcdef";
            //            break;
            //         default:
            //            lol += "000000";
            //            break;

            //    }


            //}

            //lol += " FxF0";

            //Debug.WriteLine(lol);




            for (int i = 0; i < 240; i++)
            {

                PictureBox p = new PictureBox
                {
                    BackColor = System.Drawing.SystemColors.ActiveBorder,
                    Location = new System.Drawing.Point(708, 3),
                    Name = "pictureBox1",
                    Size = new System.Drawing.Size(9, 25)
                };

                tableLayoutPanel1.Controls.Add(p);
            }

            timer.Interval = 50;
            timer.Tick += CycleTick;
            timer.Start();

        }




        void CycleTick(object source, EventArgs e)
        {
            trainBehaviour.MoveTrains();

            DrawRails(trainBehaviour.GetRails());
            Debug.WriteLine(GenerateHexString(trainBehaviour.GetRails()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
        }

        private string GenerateHexString(Rail[,] rails)
        {
            Rail[] allRails = new Rail[240];
            for (int ri = 0; ri < rails.GetLength(0); ri++)
            {
                for (int r = 0; r < rails.GetLength(1); r++)
                {
                    allRails[(ri * rails.GetLength(1)) + r] = rails[ri, r];
                }
            }

            string longString = "FxFF 9 ";
            string[] hex = new string[240];
            for (int i = 0; i < hex.Length; i++)
            {
                hex[i] = "000000";
            }


            for (int i = 0; i < allRails.Length; i++)
            {
                if (allRails[i].GetTrains().Count > 0)
                {
                    hex[i] = allRails[i].GetTrains()[0].GetHEX();
                }
            }

            for (int i = 0; i < hex.Length; i++)
            {
                if (prevHex[i] != hex[i])
                {
                    longString += i.ToString("000")+hex[i];
                }
                else
                {
                    //longString += "zzzzzz";
                }
            }

            longString += "~";
            prevHex = hex;
            return longString;

        }

        private void DrawRails(Rail[,] rails)
        {
            for (int ri = 0; ri < rails.GetLength(0); ri++)
            {
                for (int r = 0; r < rails.GetLength(1); r++)
                {

                    if (rails[ri, r].GetTrains().Count > 0)
                    {
                        PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(r, ri);
                        Train train = rails[ri, r].GetTrains()[0];
                        p.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + train.GetHEX());
                    }
                    
                    else if (rails[ri, r].GetWagon() != null)
                    {
                        PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(r, ri);
                        Wagon wagon = rails[ri, r].GetWagon();

                        //if (!wagon.Gettrain().InStation())
                        {

                            Color color = new Color();
                            switch (wagon.GetCroudLevel())
                            {
                                case CroudLevel.L:
                                    color = System.Drawing.ColorTranslator.FromHtml("#00ff00");
                                    break;
                                case CroudLevel.M:
                                    color = System.Drawing.ColorTranslator.FromHtml("#fbff00");
                                    break;
                                case CroudLevel.H:
                                    color = System.Drawing.ColorTranslator.FromHtml("#ff0000");
                                    break;
                            }
                            p.BackColor = color;
                        }
                    }
                    else if (rails[ri, r].GetStation() != null)
                    {
                        PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(r, ri);
                        if (rails[ri, r].GetStation().CentralStation())
                        {
                            p.BackColor = System.Drawing.ColorTranslator.FromHtml("#fc03ca");
                        }
                        else
                        {
                            p.BackColor = System.Drawing.ColorTranslator.FromHtml("#6e0058");
                        }

                    }
                    else
                    {
                        PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(r, ri);
                        p.BackColor = System.Drawing.SystemColors.Control;
                    }

                }
            }




        }

    }
}
