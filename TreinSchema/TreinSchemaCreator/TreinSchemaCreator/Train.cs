using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinSchemaCreator
{
    class Train
    {
        private int timeToWait = 10;
        public string name { get; }
        public bool interCity { get; }
        public Station[] route { get; }
        public int nextDestination { get; set; }
        public int pos { get; set; }
        /// <summary>
        /// true = right
        /// </summary>
        public bool direction { get; set; }
        private bool inStation;

        public Train(string name, bool interCity, Station[] route, int nextDestination, bool direction, Station startStation)
        {
            this.name = name;
            this.interCity = interCity;
            this.route = route;
            this.nextDestination = nextDestination;
            this.direction = direction;



            pos = startStation.pos;

            startStation.trains.Add(this);
            inStation = true;

        }

        public void Move()
        {
            if (inStation)
            {
                timeToWait--;
                return;
            }
            if (pos == route[nextDestination].pos)
            {
                route[nextDestination].trains.Add(this);
                nextDestination++;
                if(nextDestination == route.Length)
                {
                    nextDestination = 0;
                }
                inStation = true;
                timeToWait = 10;
                return;
            }

            SetDirection();
            int factor = direction ? 1 : -1;
            pos += factor;

        }
        public void Depart(Station station)
        {
            inStation = false;
            station.trains.Remove(this);
        }

        public void SetDirection()
        {
            direction = pos < route[nextDestination].pos;
        }
    }
}
