using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheme1
{
    class TrainBehaviour
    {
        //Space to leave clear in front of train
        private int clearSpace = 6;

        Rail[] rails = new Rail[120];


        List<Station> stations = new List<Station>();
        List<Train> trains = new List<Train>();


        public TrainBehaviour()
        {
            for (int r = 0; r < rails.Length; r++)
            {
                rails[r] = new Rail(r);
            }

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


            trains.Add(new Train("I1", "fcba03", true, new Station[] { stations[0], stations[2], stations[5], stations[2], stations[0] }, rails[0]));
            trains.Add(new Train("I2", "fc0303", true, new Station[] { stations[5], stations[2], stations[0], stations[2], stations[5] }, rails[116]));
            trains.Add(new Train("S1", "03fce7", false, new Station[] { stations[0], stations[1], stations[2], stations[1], stations[0] }, rails[0]));
            trains.Add(new Train("S2", "fc03f4", false, new Station[] { stations[2], stations[3], stations[4], stations[5], stations[4], stations[3], stations[2] }, rails[50]));
        }

        public void MoveTrains()
        {
            for (int t = 0; t < trains.Count; t++)
            {
                Train train = trains[t];
                bool right = train.GoingRight();
                int trainPos = train.GetRail().GetIndex();
                bool clear = true;

                if (right)
                {
                    for (int i = trainPos + 1; i < trainPos + clearSpace; i++)
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
                    for (int i = trainPos - 1; i > trainPos - clearSpace; i--)
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
        }

        public Rail[] GetRails()
        {
            return rails;
        }


    }
}
