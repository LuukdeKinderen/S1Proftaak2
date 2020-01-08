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
        private int forward;
        private Station station;
        private List<Train> trains = new List<Train>();
        private List<Wagon> wagons = new List<Wagon>();

        /// <summary>
        /// Constructs a rail object
        /// </summary>
        /// <param name="index">The index of the Rail object {0-120}</param>
        public Rail(int forward, int index)
        {
            this.index = index;
            this.forward = forward;
        }

        /// <summary>
        /// Adds a station object to this Rail and sets rail on station
        /// </summary>
        /// <param name="station">The station to add</param>
        public void AddStation(Station station)
        {
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
            return forward;
        }

        /// <summary>
        /// Checks if this Rail is safe to enter for specific train.
        /// </summary>
        /// <returns>true if Rail is safe</returns>
        public bool IsClear()
        {
            bool clear = true;
            if (trains.Count > 0)
            {
                clear = false;
                if (station != null)
                {
                    if (station.CentralStation())
                    {
                        clear = true;
                    }
                }
            }
            if (wagons.Count > 0)
            {
                clear = false;
                Rail rail = wagons[0].Gettrain().GetRail();
                if (rail.GetStation() != null)
                {
                    if (rail.GetStation().CentralStation())
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

        public void AddWagon(Wagon[] wagons, Wagon wagon)
        {
            int index = Array.IndexOf(wagons, wagon);
            Rail oldRail = wagon.GetRail();

            wagon.SetRail(this);
            this.wagons.Add(wagon);

            index++;
            if (index != wagons.Length)
            {
                oldRail.AddWagon(wagons, wagons[index]);
            }

            oldRail.RemoveWagon(wagons[index - 1]);

        }

        private void RemoveWagon(Wagon wagon)
        {
            wagons.Remove(wagon);
        }

        public Wagon GetWagon()
        {
            if (wagons.Count > 0)
            {
                return wagons[0];
            }
            else
            {
                return null;
            }

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
