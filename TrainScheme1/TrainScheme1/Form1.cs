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

        public Form1()
        {
            InitializeComponent();

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



            timer.Interval = 300;
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



            for (int i = 0; i < allRails.Length; i++)
            {
                if (allRails[i].GetTrains().Count > 0)
                {
                    Color c = allRails[i].GetTrains()[0].GetColor();
                    longString += i.ToString("000") + c.R.ToString("000") + c.G.ToString("000") + c.B.ToString("000");
                }
                else if (allRails[i].GetWagon() != null)
                {
                    Color c = allRails[i].GetWagon().GetColor();
                    longString += i.ToString("000") + c.R.ToString("000") + c.G.ToString("000") + c.B.ToString("000");
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
                        p.BackColor = train.GetColor();
                    }

                    else if (rails[ri, r].GetWagon() != null)
                    {
                        PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(r, ri);
                        Wagon wagon = rails[ri, r].GetWagon();
                        p.BackColor = wagon.GetColor();
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






    }
}
