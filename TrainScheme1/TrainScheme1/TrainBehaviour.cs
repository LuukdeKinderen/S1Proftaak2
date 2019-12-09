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



            stations.Add(new Station("CL", true, 0, rails));
            stations.Add(new Station("KF", false, 20, rails));
            stations.Add(new Station("FR", true, 46, rails));
            stations.Add(new Station("BH", false, 70, rails));
            stations.Add(new Station("BR", false, 90, rails));
            stations.Add(new Station("JP", true, 116, rails));



            trains.Add(new Train("I1", "00fffb", true, new Station[] { stations[0], stations[2], stations[5], stations[2], stations[0] }));
            trains.Add(new Train("I2", "00ff22", true, new Station[] { stations[5], stations[2], stations[0], stations[2], stations[5] }));
            trains.Add(new Train("S1", "ddff00", false, new Station[] { stations[0], stations[1], stations[2], stations[1], stations[0] }));
            trains.Add(new Train("S2", "ff0000", false, new Station[] { stations[2], stations[3], stations[4], stations[5], stations[4], stations[3], stations[2] }));
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
                        int factor = right ? 1 : -1;
                        train.SetRail(rails[right ? 1 : 0, train.GetRail().GetIndex() + factor]);
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


    }
}
