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

            timer.Interval = 1;
            timer.Tick += MoveTrains;

            stations.Add(new Station("CL", true));
            rails[0].AddStation(stations[0]);

            stations.Add(new Station("KF", true));
            rails[20].AddStation(stations[1]);

            trains.Add(new Train("I1", true, true, new Station[] { stations[0], stations[1], stations[0] }, rails[0]));
            trains.Add(new Train("I2", true, true, new Station[] { stations[0], stations[1], stations[0] }, rails[0]));
            trains.Add(new Train("S1", false, true, new Station[] { stations[0], stations[1], stations[0] }, rails[0]));
            trains.Add(new Train("S2", false, true, new Station[] { stations[0], stations[1], stations[0] }, rails[0]));
            //rails[0].GetStation().AddTrain(trains[0]);

        }




        void MoveTrains(object source, EventArgs e)
        {

            for (int t = 0; t < trains.Count; t++)
            {
                Train train = trains[t];
                bool right = train.GetRail().GetIndex() < train.DestinationRail().GetIndex();
                int trainPos = train.GetRail().GetIndex();
                bool clear = true;
                //CHECK FOR LEFT!!!
                for (int i = trainPos + 1; i < trainPos + 6; i++)
                {
                    if (rails[i].GetTrain() != null && !rails[i].GetTrain().InStation())
                    {
                        clear = false;
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
                if (rails[r].GetTrain() != null)
                {
                    for (int i = 0; i < rails.Length; i++)
                    {
                        if (r != i)
                        {
                            Debug.Write("-- ");
                        }
                        else
                        {
                            Debug.Write(rails[r].GetTrain().name + " ");
                        }
                    }
                    Debug.WriteLine("");
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
        }


    }
}
