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

        Rail[,] rails = new Rail[2, 120];


        List<Station> stations = new List<Station>();
        List<Train> trains = new List<Train>();


        public TrainBehaviour()
        {
            for (int ri = 0; ri < rails.GetLength(0); ri++)
            {
                for (int r = 0; r < rails.GetLength(1); r++)
                {
                    rails[ri, r] = new Rail(ri, r);
                }
            }

            stations.Add(new Station("CL", true, new Rail[,] { { rails[0, 0], rails[0, 1], rails[0, 2], rails[0, 3] }, { rails[1, 0], rails[1, 1], rails[1, 2], rails[1, 3] } }));
            stations.Add(new Station("KF", false, new Rail[,] { { rails[0, 20], rails[0, 21], rails[0, 22], rails[0, 23] }, { rails[1, 20], rails[1, 21], rails[1, 22], rails[1, 23] } }));
            stations.Add(new Station("FR", true, new Rail[,] { { rails[0, 50], rails[0, 51], rails[0, 52], rails[0, 53] }, { rails[1, 50], rails[1, 51], rails[1, 52], rails[1, 53] } }));
            stations.Add(new Station("BH", false, new Rail[,] { { rails[0, 70], rails[0, 71], rails[0, 72], rails[0, 73] }, { rails[1, 70], rails[1, 71], rails[1, 72], rails[1, 73] } }));
            stations.Add(new Station("BR", false, new Rail[,] { { rails[0, 90], rails[0, 91], rails[0, 92], rails[0, 93] }, { rails[1, 90], rails[1, 91], rails[1, 92], rails[1, 93] } }));
            stations.Add(new Station("JP", true, new Rail[,] { { rails[0, 116], rails[0, 117], rails[0, 118], rails[0, 119] }, { rails[1, 116], rails[1, 117], rails[1, 118], rails[1, 119] } }));




            trains.Add(new Train("I1", "fcba03", true, new Station[] { stations[0], stations[2], stations[5], stations[2], stations[0] }, rails[0, 0]));
            trains.Add(new Train("I2", "fc0303", true, new Station[] { stations[5], stations[2], stations[0], stations[2], stations[5] }, rails[1, 116]));
            trains.Add(new Train("S1", "03fce7", false, new Station[] { stations[0], stations[1], stations[2], stations[1], stations[0] }, rails[0, 0]));
            trains.Add(new Train("S2", "fc03f4", false, new Station[] { stations[2], stations[3], stations[4], stations[5], stations[4], stations[3], stations[2] }, rails[0, 50]));
        }

        public void MoveTrains()
        {
            for (int t = 0; t < trains.Count; t++)
            {
                Train train = trains[t];
                bool right = train.NeedsToGoRight();
                int trainPos = train.GetRail().GetIndex();
                bool clear = true;

                if (right)
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
                if (clear)
                {
                    //Als trein in het volgende station aangekomen is.
                    if (train.Arrived())
                    {
                        train.Arrive();
                    }
                    //Als trein niet in het station staat (aan het rijden is)
                    if (!train.InStation())
                    {
                        int factor = right ? 1 : -1;
                        train.SetRail(rails[right ? 1 : 0, train.GetRail().GetIndex() + factor]);
                    }
                    else if (train.GetRail().GetStation().ReadytoDepart(train))
                    {
                        train.GetRail().GetStation().DepartTrain(train);
                    }
                    else
                    {

                        //train.SetRail(rails[train.GetRail().GetRight(), train.GetRail().GetIndex() - train.GetRail().GetStation().GetTrains().IndexOf(train)+3]);
                    }
                }
            }
        }

        public Rail[,] GetRails()
        {
            return rails;
        }


    }
}
