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
        private Station station;


        public Rail(int index)
        {
            this.index = index;
        }

        public void AddStation(Station station)
        {
            station.SetRail(this);
            this.station = station;
        }
        public Station GetStation()
        {
            return station;
        }
        public int GetIndex()
        {
            return index;
        }
    }
}
