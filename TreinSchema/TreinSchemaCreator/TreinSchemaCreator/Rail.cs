using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinSchemaCreator
{
    class Rail
    {
        private List<Train> trains = new List<Train>();
        private Station station;
        private int pos;

        public Rail( int pos)
        { 
            this.pos = pos;
        }

        public void SetTrain(Train train)
        {
            for (int r = 0; r < Program.rails.Count; r++)
            {
                Program.rails[r].RemoveTrain(train);
            }
            trains.Add(train);
            train.rail = this;
            if(station != null)
            {
                station.trains.Add(train);
            }

        }

        public void SetStation(Station station)
        {
            this.station = station;
        }

        private void RemoveTrain(Train train)
        {
            trains.Remove(train);
        }

        public int GetPos()
        {
            return pos;
        }
        public List<Train> GetTrains()
        {
            return trains;
        }
        public Station GetStation()
        {
            return station;
        }
    }
}
