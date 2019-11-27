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
        Rail[] rails = new Rail[120];
        Timer timer = new Timer();

        List<Station> stations = new List<Station>();
        List<Train> trains = new List<Train>();

        public Form1()
        {
            InitializeComponent();

            for (int r = 0; r < rails.Length; r++)
            {
                rails[r] = new Rail(r);
            }

            timer.Interval = 200;
            timer.Tick += MoveTrains;

            stations.Add(new Station("CL", true));
            rails[0].AddStation(stations[0]);

            stations.Add(new Station("KF", false));
            rails[20].AddStation(stations[1]);

            stations.Add(new Station("FT", true));
            rails[50].AddStation(stations[2]);

            stations.Add(new Station("BH", false));
            rails[70].AddStation(stations[3]);

            stations.Add(new Station("BR", false));
            rails[90].AddStation(stations[4]);

            stations.Add(new Station("JP", true));
            rails[116].AddStation(stations[5]);


            trains.Add(new Train("I1", true, true, new Station[] { stations[0], stations[2], stations[5], stations[2], stations[0] }, rails[0]));
            trains.Add(new Train("I2", true, true, new Station[] { stations[5], stations[2], stations[0], stations[2], stations[5] }, rails[116]));
            trains.Add(new Train("S1", false, true, new Station[] { stations[0], stations[1], stations[2], stations[1], stations[0] }, rails[0]));
            trains.Add(new Train("S2", false, true, new Station[] { stations[2], stations[3], stations[4], stations[5], stations[4], stations[3], stations[2] }, rails[40]));

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

            for (int t = 0; t < trains.Count; t++)
            {
                Train train = trains[t];
                bool right = train.GoingRight();
                int trainPos = train.GetRail().GetIndex();
                bool clear = true;

                if (right)
                {
                    for (int i = trainPos + 1; i < trainPos + 6; i++)
                    {
                        if (i < rails.Length)
                        {

                            if (!rails[i].IsClear(train))
                            {
                                clear = false;
                            }
                        }

                    }
                }
                else
                {
                    for (int i = trainPos - 1; i > trainPos - 6; i--)
                    {
                        if (i >= 0)
                        {
                            if (!rails[i].IsClear(train))
                            {
                                clear = false;
                            }
                        }
                    }
                }
                if (clear)
                {
                    //Als trein in het volgende station aangekomen is.
                    if (train.GetRail() == train.DestinationRail())
                    {
                        train.Arrive();
                        train.GetRail().GetStation().AddTrain(train);
                        train.SetNextDestination();
                    }
                    //Als trein niet in het station staat (aan het rijden is)
                    if (!train.InStation())
                    {
                        int factor = right ? 1 : -1;
                        train.SetRail(rails[train.GetRail().GetIndex() + factor]);
                    }
                    else if (train.GetRail().GetStation().ReadytoDepart(train))
                    {
                        train.GetRail().GetStation().DepartTrain(train);
                    }
                }
            }

            DrawTrains();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
        }

        private void DrawTrains()
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

        private void DrawTrainsDebug()
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
