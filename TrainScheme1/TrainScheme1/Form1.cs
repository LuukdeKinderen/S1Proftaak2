﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Ports;

namespace TrainScheme1
{
    public partial class Form1 : Form
    {
        SerialPort SerialPort1 = new SerialPort("COM12", 9600);
        Timer timer = new Timer();
        TrainBehaviour trainBehaviour = new TrainBehaviour();
        string[] prevHex = new string[240];

        public Form1()
        {
            SerialPort1.Open();
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



            timer.Interval = 150;
            timer.Tick += CycleTick;
            timer.Start();

        }




        void CycleTick(object source, EventArgs e)
        {
            trainBehaviour.MoveTrains();
            DrawRails(trainBehaviour.GetRails());

            SerialPort1.Write(GenerateHexString(trainBehaviour.GetRails()));
            Debug.WriteLine(GenerateHexString(trainBehaviour.GetRails()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
        }

        private string GenerateDifferenceHexString(Rail[,] rails)
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
                hex[i] = "&";
            }


            for (int i = 0; i < allRails.Length; i++)
            {
                if (allRails[i].GetTrains().Count > 0)
                {
                    hex[i] = allRails[i].GetTrains()[0].GetHEX();
                }
                else if (allRails[i].GetWagon() != null)
                {
                    hex[i] = allRails[i].GetWagon().GetHEX();
                }
            }

            for (int i = 0; i < hex.Length; i++)
            {
                if (prevHex[i] != hex[i])
                {
                    longString += i.ToString("000") + hex[i];
                }
            }

            longString += "~";
            prevHex = hex;
            return longString;

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



            for (int i = 0; i < allRails.Length; i++)
            {
                if (allRails[i].GetTrains().Count > 0)
                {
                    longString += i.ToString("000") + allRails[i].GetTrains()[0].GetHEX();
                }
                else if (allRails[i].GetWagon() != null)
                {
                    longString += i.ToString("000") + allRails[i].GetWagon().GetHEX();
                }
            }


            longString += "~";
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
                        p.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + wagon.GetHEX());
                    }
                    else if (rails[ri, r].GetStation() != null)
                    {
                        PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(r, ri);
                        if (rails[ri, r].GetStation().CentralStation())
                        {
                            p.BackColor = System.Drawing.ColorTranslator.FromHtml("#4a4a4a");
                        }
                        else
                        {
                            p.BackColor = System.Drawing.ColorTranslator.FromHtml("#878787");
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
