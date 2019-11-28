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

            
            

            timer.Interval = 200;
            timer.Tick += MoveTrains;

            

            for (int i = 0; i < 120; i++)
            {

                PictureBox p = new PictureBox
                {
                    BackColor = System.Drawing.SystemColors.Control,
                    Location = new System.Drawing.Point(708, 3),
                    Name = "pictureBox1",
                    Size = new System.Drawing.Size(9, 25)
                };
                tableLayoutPanel1.Controls.Add(p, i, 0);

            }


        }




        void MoveTrains(object source, EventArgs e)
        {

            trainBehaviour.MoveTrains();

            DrawRails(trainBehaviour.GetRails());


        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
        }

        private void DrawRails(Rail[] rails)
        {
            for (int i = 0; i < 120; i++)
            {
                if (rails[i].GetTrains().Count > 0)
                {
                    PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(i, 0);
                    p.BackColor = System.Drawing.SystemColors.ActiveBorder;
                }
                else
                {
                    PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(i, 0);
                    p.BackColor = System.Drawing.SystemColors.Control;
                }
                if (rails[i].GetStation() != null)
                {
                    PictureBox p = (PictureBox)tableLayoutPanel1.GetControlFromPosition(i, 0);
                    p.BackColor = System.Drawing.SystemColors.Window;
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
