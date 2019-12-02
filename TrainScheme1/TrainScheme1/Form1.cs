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

            /*
            Random r = new Random();
            string lol = "FxFF 9 ";

            for (int i = 0; i < 120; i++)
            {
                int rr = r.Next(0, 5);
                switch (rr)
                {
                    case 0:
                        lol += "fffff";
                        break;
                    case 1:
                        lol += "f036a6";
                        break;
                    case 2:
                        lol += "123456";
                        break;
                    case 3:
                        lol += "abcdef";
                        break;
                    case 4:
                        lol += "000000";
                        break;
                }
            }

            lol += " FxF0";

            Debug.WriteLine(lol);
            */



            for (int i = 0; i < 120; i++)
            {

                PictureBox p = new PictureBox
                {
                    BackColor = System.Drawing.SystemColors.ActiveBorder,
                    Location = new System.Drawing.Point(708, 3),
                    Name = "pictureBox1",
                    Size = new System.Drawing.Size(9, 25)
                };
                PictureBox p1 = new PictureBox
                {
                    BackColor = System.Drawing.SystemColors.ActiveBorder,
                    Location = new System.Drawing.Point(708, 3),
                    Name = "pictureBox1",
                    Size = new System.Drawing.Size(9, 25)
                };
                tableLayoutPanel1.Controls.Add(p, i, 0);
                tableLayoutPanel1.Controls.Add(p1, i, 1);
            }

            timer.Interval = 100;
            timer.Tick += CycleTick;
            timer.Start();


        }




        void CycleTick(object source, EventArgs e)
        {

            trainBehaviour.MoveTrains();

            DrawRails(trainBehaviour.GetRails());
            //Debug.WriteLine(GenerateHexString(trainBehaviour.GetRails()));

        }

        private void button1_Click(object sender, EventArgs e)
        {

            timer.Enabled = !timer.Enabled;
            
        }

        private string GenerateHexString(Rail[] rails)
        {
            string hexArr = "FxFF 9 ";
            string[] arr = new string[240];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = "000000";
            }


            for (int r = 0; r < rails.Length; r++)
            {
                if (rails[r].GetStation() != null)
                {
                    List<Train> trains = rails[r].GetStation().GetTrains();

                    if (trains.Count > 0)
                    {
                        int ledRightIndex = r + 3;
                        int ledLeftIndex = r;
                        for (int t = 0; t < trains.Count; t++)
                        {
                            if (trains[t].NeedsToGoRight())
                            {
                                if (ledRightIndex >= r)
                                {
                                    arr[ledRightIndex] = trains[t].GetHEX();
                                    ledRightIndex--;
                                }
                            }
                            else
                            {
                                if (ledLeftIndex <= r + 3)
                                {
                                    arr[ledLeftIndex] = trains[t].GetHEX();
                                    ledLeftIndex++;
                                }
                            }
                        }
                    }
                }
                else
                {

                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                hexArr += arr[i];
            }

            hexArr += " FxF0";
            return hexArr;
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
                        p.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + rails[ri, r].GetTrains()[0].GetHEX());
                    }
                    else if (rails[ri, r].GetStation() != null)
                    {
                        PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(r, ri);
                        if (rails[ri, r].GetStation().CentralStation())
                        {
                            p.BackColor = System.Drawing.ColorTranslator.FromHtml("#000000");
                        }
                        else
                        {
                            p.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
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


        private void DrawTrainsDebug(Rail[] rails)
        {
            for (int r = 0; r < rails.Length; r++)
            {
                Debug.Write("|| ");
            }
            Debug.WriteLine("");
            for (int r = 0; r < rails.Length; r++)
            {
                if (rails[r].GetStation() != null)
                {
                    for (int i = 0; i < rails.Length; i++)
                    {
                        if (r != i)
                        {
                            Debug.Write("-- ");
                        }
                        else
                        {
                            Debug.Write(rails[r].GetStation().name + " ");
                        }
                    }
                    Debug.WriteLine("");
                }

                for (int t = 0; t < rails[r].GetTrains().Count; t++)
                {
                    for (int i = 0; i < rails.Length; i++)
                    {
                        if (r != i)
                        {
                            Debug.Write("-- ");
                        }
                        else
                        {
                            Debug.Write(rails[r].GetTrains()[t].name + " ");
                        }
                    }
                    Debug.WriteLine("");

                }
            }
        }


    }
}
