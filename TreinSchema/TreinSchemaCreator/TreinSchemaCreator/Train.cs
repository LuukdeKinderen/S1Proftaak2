using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TreinSchemaCreator
{
    class Train
    {
        private int timeToWait = 10;
        public string name { get; }
        public bool interCity { get; }
        public Station[] route { get; }
        public int nextDestination { get; set; }
        public Rail rail { get; set; }
        /// <summary>
        /// true = right
        /// </summary>
        public bool direction { get; set; }
        private bool inStation;

        public Train(string name, bool interCity, Station[] route, int nextDestination, bool direction, Rail startRail)
        {
            this.name = name;
            this.interCity = interCity;
            this.route = route;
            this.nextDestination = nextDestination;
            this.direction = direction;





            startRail.SetTrain(this);
            inStation = true;

        }

        public void Move()
        {
            if (inStation)
            {
                timeToWait--;
                return;
            }
            if (rail == route[nextDestination].GetRail())
            {
                route[nextDestination].trains.Add(this);
                nextDestination++;
                if (nextDestination == route.Length)
                {
                    nextDestination = 0;
                }
                inStation = true;
                timeToWait = 10;
                return;
            }

            SetDirection();
            int factor = direction ? 1 : -1;

            for (int r = 0; r < 4; r++)
            {
                Rail railPiece = Program.rails[rail.GetPos() + (r * factor)];
                if (railPiece.GetTrains().Count > 0)
                {
                    for (int t = 0; t < railPiece.GetTrains().Count; t++)
                    {
                        Train train = railPiece.GetTrains()[t];
                        if (train.direction == direction && !train.inStation)
                        {
                            return;
                        }
                    }
                }
            }

            Program.rails[rail.GetPos() + factor].SetTrain(this);


        }
        public void Depart(Station station)
        {

            if (timeToWait <= 0)
            {

                inStation = false;
                station.trains.Remove(this);
            }
        }

        public void SetDirection()
        {
            direction = rail.GetPos() < route[nextDestination].GetRailPos();
        }

        public int GetRailPos()
        {
            return rail.GetPos();
        }

        
    }
}
