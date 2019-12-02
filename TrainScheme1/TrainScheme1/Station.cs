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
        private Rail[,] rails = new Rail[2,4];

        /// <summary>
        /// constructs a new Station object
        /// </summary>
        /// <param name="name">Name of the new Station</param>
        /// <param name="centralStation">If true => new station is a central Station</param>
        public Station(string name, bool centralStation, Rail[,] rails)
        {
            this.name = name;
            this.centralStation = centralStation;
            this.rails = rails;
            for (int r = 0; r < this.rails.GetLength(0); r++)
            {
                for (int ri = 0; ri < this.rails.GetLength(1); ri++)
                {
                    this.rails[r, ri].AddStation(this);
                }
            }
        }

        /// <summary>
        /// Adds a train to this station. And creates a waittime int for that train.
        /// </summary>
        /// <param name="train">Train object to add</param>
        public void AddTrain(Train train)
        {
            trains.Add(train);

            
            int waitTime = centralStation ? train.Intercity() ? 7 : 25 : 10;
            trainWaitTime.Add(waitTime);

            for (int t = 0; t < trains.Count; t++)
            {

            }

            /*
            List<Train> trainsRight = new List<Train>();
            List<Train> trainsLeft = new List<Train>();
            for (int t = 0; t < trains.Count; t++)
            {
                if (trains[t].GoingRight())
                {
                    trainsRight.Add(trains[t]);
                }
                else
                {
                    trainsLeft.Add(trains[t]);
                }
            }
            for (int i = 0; i < trainsRight.Count; i++)
            {
                trainsRight[i].SetRail(rails[1,i]);
            }
            for (int i = 0; i < trainsLeft.Count; i++)
            {
                trainsLeft[i].SetRail(rails[0,i]);
            }*/
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
        /// Gets main rail of this station
        /// </summary>
        /// <returns>Rail object</returns>
        public int GetPosition()
        {
            return rails[0,0].GetIndex();
        }

        public bool Arrived(Train train)
        {
            bool arrived = false;
            for (int r = 0; r < rails.GetLength(0); r++)
            {
                for (int ri = 0; ri < rails.GetLength(1); ri++)
                {
                    if(train.GetRail() == rails[r, ri])
                    {
                        arrived = true;
                    }
                }
            }
            return arrived;
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
