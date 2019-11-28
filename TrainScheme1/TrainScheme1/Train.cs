using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheme1
{
    class Train
    {
        /// <summary>
        /// name this train
        /// </summary>
        public string name;
        private bool intercity;
        private Rail rail;

        private Station[] route;
        private int nextDestination = 0;
        private bool inStation;

        /// <summary>
        /// Constructs a new Train object
        /// </summary>
        /// <param name="name">name of the new Train</param>
        /// <param name="intercity">When true => new Train is intercity</param>
        /// <param name="route">Array of stations to cover on full train cycle, please include return</param>
        /// <param name="rail">Rail to start on</param>
        public Train(string name, bool intercity, Station[] route,Rail rail)
        {
            this.name = name;
            this.intercity = intercity;
            this.route = route;
            this.rail = rail;
        }

        /// <summary>
        /// Remove train from old rail and Add train to new rail
        /// </summary>
        /// <param name="rail">new rail</param>
        public void SetRail(Rail rail)
        {
            this.rail.RemoveTrain(this);
            this.rail = rail;
            this.rail.AddTrain(this);
        }

        /// <summary>
        /// Sets next destination and loops back to start when cycle has been completed
        /// </summary>
        public void SetNextDestination()
        {
            nextDestination++;
            if (nextDestination == route.Length - 1)
            {
                nextDestination = 0;
            }
        }
        
        /// <summary>
        /// Gets rail of next station in cycle
        /// </summary>
        /// <returns>Rail object of station</returns>
        public Rail DestinationRail()
        {
            return route[nextDestination].GetRail();
        }

        /// <summary>
        /// calculates if next station in cycle is to the right of this train
        /// </summary>
        /// <returns>true if station is to the right</returns>
        public bool GoingRight()
        {
            return rail.GetIndex() < DestinationRail().GetIndex();
        }

        /// <summary>
        /// Gets current rail object that train is on
        /// </summary>
        /// <returns>Rail object</returns>
        public Rail GetRail()
        {
            return rail;
        }

        /// <summary>
        /// retruns true if this train is in station
        /// </summary>
        /// <returns></returns>
        public bool InStation()
        {
            return inStation;
        }

        /// <summary>
        /// this train leaves the station
        /// </summary>
        public void Depart()
        {
            inStation = false;
        }

        /// <summary>
        /// this train has arrived on a station
        /// </summary>
        public void Arrive()
        {
            inStation = true;
        }
    }
}
