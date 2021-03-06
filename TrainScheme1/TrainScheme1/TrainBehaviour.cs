﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrainScheme1
{

    class TrainBehaviour
    {
        //Space to leave clear in front of train
        private int clearSpace = 6;

        private Rail[,] rails = new Rail[2, 120];


        private List<Station> stations = new List<Station>();
        private List<Train> trains = new List<Train>();


        public TrainBehaviour()
        {
            for (int ri = 0; ri < rails.GetLength(0); ri++)
            {
                for (int r = 0; r < rails.GetLength(1); r++)
                {
                    rails[ri, r] = new Rail(ri, r);
                }
            }



            stations.Add(new Station("Christopher laan", true, 0, rails));
            stations.Add(new Station("Knap Ford", false, 20, rails));
            stations.Add(new Station("Frow Tastic", true, 46, rails));
            stations.Add(new Station("Burgemeester v. Hoofdstraat", false, 70, rails));
            stations.Add(new Station("Blauwe Reiger", false, 90, rails));
            stations.Add(new Station("Jethoe Plein", true, 116, rails));



            trains.Add(new Train("I1", Color.FromArgb(255, 0, 0), true, new Station[] { stations[0], stations[2], stations[5], stations[2], stations[0] }));
            trains.Add(new Train("I2", Color.FromArgb(255,255, 0), true, new Station[] { stations[5], stations[2], stations[0], stations[2], stations[5] }));
            trains.Add(new Train("S1", Color.FromArgb(0, 255, 0), false, new Station[] { stations[0], stations[1], stations[2], stations[1], stations[0] }));
            trains.Add(new Train("S2", Color.FromArgb(0, 0, 255), false, new Station[] { stations[2], stations[3], stations[4], stations[5], stations[4], stations[3], stations[2] }));
        }

        public void MoveTrains()
        {
            for (int t = 0; t < trains.Count; t++)
            {
                Train train = trains[t];
                bool forward = train.NeedsToGoForward();
                int trainPos = train.GetRail().GetIndex();
                bool clear = true;

                if (forward)
                {
                    for (int i = trainPos + 1; i < trainPos + clearSpace; i++)
                    {
                        if (i < rails.GetLength(1))
                        {
                            if (!rails[1, i].IsClear())
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
                            if (!rails[0, i].IsClear())
                            {
                                clear = false;
                            }
                        }
                    }
                }

                //Als trein in het volgende station aangekomen is.
                if (train.Arrived())
                {
                    train.Arrive();
                }
                //Als trein niet in het station staat (aan het rijden is)
                if (!train.InStation())
                {
                    if (clear)
                    {
                        int factor = forward ? 1 : -1;
                        train.SetRail(rails[forward ? 1 : 0, train.GetRail().GetIndex() + factor]);
                    }
                }
                else if (train.GetRail().GetStation().ReadytoDepart(train))
                {
                    train.GetRail().GetStation().DepartTrain(train);
                }

            }
        }

        public Rail[,] GetRails()
        {
            return rails;
        }

        public Station GetStation(int i)
        {
            return stations[i];
        }

        public string StationStr()
        {
            string stationsStr = stations.Count.ToString();
            for (int i = 0; i < stations.Count; i++)
            {
                stationsStr += stations[i].GetRailIndex().ToString("000");
            }
            return stationsStr;
        }

    }
}
