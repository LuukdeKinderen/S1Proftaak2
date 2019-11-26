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

            trains.Add(new Train("S1", true, true, new Station[] { stations[0], stations[1], stations[0] }));
            rails[0].GetStation().AddTrain(trains[0]);

        }




        void MoveTrains(object source, EventArgs e)
        {
            for (int t = 0; t < trains.Count; t++)
            {
                Train train = trains[t];
                if (!train.InStation())
                {
                    int factor = train.GetRail().GetIndex() < train.DestinationRail().GetIndex() ? 1 : -1;
                    train.SetRail(rails[train.GetRail().GetIndex() + factor]);
                }
                else
                {
                    train.GetRail().GetStation().DepartTrain(train);
                }
                Debug.WriteLine(train.GetRail().GetIndex());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
        }


    }
}
