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
        private int right;
        private Station station;
        private List<Train> trains = new List<Train>();

        /// <summary>
        /// Constructs a rail object
        /// </summary>
        /// <param name="index">The index of the Rail object {0-120}</param>
        public Rail(int right, int index)
        {
            this.index = index;
            this.right = right;
        }
        /// <summary>
        /// Adds a station object to this Rail and sets rail on station
        /// </summary>
        /// <param name="station">The station to add</param>
        public void AddStation(Station station)
        {
            station.SetRail(right,this);
            this.station = station;
        }

        /// <summary>
        /// Gets station when one is present
        /// </summary>
        /// <returns>Station or NULL</returns>
        public Station GetStation()
        {
            return station;
        }

        /// <summary>
        /// Gets index of this rail
        /// </summary>
        /// <returns>Index of this rail</returns>
        public int GetIndex()
        {
            return index;
        }

        public int GetRight()
        {
            return right;
        }

        /// <summary>
        /// Checks if this Rail is safe to enter for specific train.
        /// </summary>
        /// <param name="train">The train that it needs to check for</param>
        /// <returns>true if Rail is safe</returns>
        public bool IsClear()
        {
            bool clear = true;
            if(trains.Count > 0)
            {
                //for (int t = 0; t < trains.Count; t++)
                //{
                //    //Als treinen in dezelfde richting rijden
                //    if(trains[t].GoingRight() == train.GoingRight())
                //    {
                        clear = false;
                //    }
                //}
                

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

        /// <summary>
        /// Adds train to trains list on this rail
        /// </summary>
        /// <param name="train">train to add</param>
        public void AddTrain(Train train)
        {
            this.trains.Add(train);
        }

        /// <summary>
        /// Removes train form trains list on this rail
        /// </summary>
        /// <param name="train">train to remove</param>
        public void RemoveTrain(Train train)
        {
            this.trains.Remove(train);
        }

        /// <summary>
        /// Gets all trains on this rail
        /// </summary>
        /// <returns>list of trains</returns>
        public List<Train> GetTrains()
        {
            return trains;
        }
    }
}
