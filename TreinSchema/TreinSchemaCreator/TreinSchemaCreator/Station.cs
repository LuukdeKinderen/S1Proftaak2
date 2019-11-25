using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinSchemaCreator
{
    class Station
    {
        public string name { get; }
        public int pos { get; }
        public bool centralStation { get; }

        public List<Train> trains = new List<Train>();

        public Station(string name, int pos, bool centralStation)
        {
            this.name = name;
            this.pos = pos;
            this.centralStation = centralStation;
        }

        public void CheckForDeparcures()
        {
            Train departureLeft = null;
            Train departureRight = null;
            foreach (Train train in trains)
            {
                train.SetDirection();
            }
            bool right = true;
            for (int p = pos; p < 6; p++)
            {
                
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
    }
}
