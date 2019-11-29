using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TrainScheme1
{
    class Station
    {
        /// <summary>
        /// Name of this station
        /// </summary>
        public string name { get; }
        private bool centralStation;
        private List<Train> trains = new List<Train>();
        private List<int> trainWaitTime = new List<int>();
        private Rail rail;

        /// <summary>
        /// constructs a new Station object
        /// </summary>
        /// <param name="name">Name of the new Station</param>
        /// <param name="centralStation">If true => new station is a central Station</param>
        public Station(string name, bool centralStation)
        {
            this.name = name;
            this.centralStation = centralStation;
        }

        /// <summary>
        /// Adds a train to this station. And creates a waittime int for that train.
        /// </summary>
        /// <param name="train">Train object to add</param>
        public void AddTrain(Train train)
        {
            trains.Add(train);
            int i = centralStation?20:10;
            trainWaitTime.Add(i);
        }

        /// <summary>
        /// Departs train from this station; removes train object and waittime object;
        /// </summary>
        /// <param name="train">Train object to depart</param>
        public void DepartTrain(Train train)
        {
            train.Depart();
            trainWaitTime.RemoveAt(trains.IndexOf(train));
            trains.Remove(train);
        }

        /// <summary>
        /// Sets main rail for this station.
        /// </summary>
        /// <param name="rail">Rail object</param>
        public void SetRail(Rail rail)
        {
            this.rail = rail;
        }

        /// <summary>
        /// Gets main rail of this station
        /// </summary>
        /// <returns>Rail object</returns>
        public Rail GetRail()
        {
            return rail;
        }

        /// <summary>
        /// Checks if the waittime for a specific train in this station is over and decreases the current waittime for that train
        /// </summary>
        /// <param name="train">Train object to check for</param>
        /// <returns>true if Waittime is over</returns>
        public bool ReadytoDepart(Train train)
        {
            trainWaitTime[trains.IndexOf(train)]--;
            return trainWaitTime[trains.IndexOf(train)] < 0;
        }

        /// <summary>
        /// Checks if this Station is a central Station
        /// </summary>
        /// <returns>true when this Station is a central Station</returns>
        public bool CentralStation()
        {
            return centralStation;
        }

        /// <summary>
        /// returns list of all trians on this staion
        /// </summary>
        /// <returns></returns>
        public List<Train> GetTrains()
        {
            return trains;
        }

    }
}
