using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheme1
{
    class Rail
    {
        private int index;
        private Station station;
        private List<Train> trains = new List<Train>();


        public Rail(int index)
        {
            this.index = index;
        }

        public void AddStation(Station station)
        {
            station.SetRail(this);
            this.station = station;
        }
        public Station GetStation()
        {
            return station;
        }
        public int GetIndex()
        {
            return index;
        }
        public bool IsClear(Train train)
        {
            bool clear = true;
            if(trains.Count > 0)
            {
                for (int t = 0; t < trains.Count; t++)
                {
                    //Als treinen in dezelfde richting rijden
                    if(trains[t].GoingRight() == train.GoingRight())
                    {
                        clear = false;
                    }
                }
                

                if(station != null)
                {
                    if (station.CentralStation())
                    {
                        clear = true;
                    }
                }
            }

            return clear;
        }

        public void AddTrain(Train train)
        {
            this.trains.Add(train);

        }
        public void RemoveTrain(Train train)
        {
            this.trains.Remove(train);
        }

        public List<Train> GetTrains()
        {
            return trains;
        }
    }
}
