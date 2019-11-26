using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinSchemaCreator
{
    class Station
    {
        private string name { get; }
        private Rail rail;
        private bool centralStation;

        public List<Train> trains = new List<Train>();

        public Station(string name, Rail rail, bool centralStation)
        {
            this.name = name;
            this.rail = rail;
            this.centralStation = centralStation;
            rail.SetStation(this);
        }

        public void CheckForDeparcures()
        {
            Train departureLeft = null;
            Train departureRight = null;
            foreach (Train train in trains)
            {
                train.SetDirection();
            }

            foreach(Train train in trains)
            {
                if (departureLeft == null && !train.direction)
                {
                    departureLeft = train;
                }
                else if(departureLeft != null && !departureLeft.interCity && train.interCity)
                {
                    departureLeft = train;
                }

                if (departureRight == null && train.direction)
                {
                    departureRight = train;
                }
                else if (departureRight != null && !departureRight.interCity && train.interCity)
                {
                    departureRight = train;
                }
            }
            if (departureLeft != null)
            {
                departureLeft.Depart(this);
            }
            if (departureRight != null)
            {
                departureRight.Depart(this);
            }

        }

        public Rail GetRail()
        {
            return rail;
        }

        public int GetRailPos()
        {
            return rail.GetPos();
        }

        public string GetName()
        {
            return name;
        }
        public bool CentralStation()
        {
            return centralStation;
        }
    }
}
