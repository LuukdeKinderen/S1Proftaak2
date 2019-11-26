using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheme1
{
    class Train
    {
        private string name;
        private bool intercity;
        private bool right;
        private Rail rail;

        private Station[] route;
        private int nextDestination = 0;
        private bool inStation;


        public Train(string name, bool intercity, bool right, Station[] route)
        {
            this.name = name;
            this.intercity = intercity;
            this.right = right;
            this.route = route;
            
        }

        public void SetRail(Rail rail)
        {
            this.rail = rail;
            if (rail.GetStation() == route[nextDestination])
            {
                inStation = true;
                
                nextDestination++;
                if(nextDestination == route.Length)
                {
                    nextDestination = 0;
                }
            }
        }

        public Rail DestinationRail()
        {
            return route[nextDestination].GetRail();
        }
        public Rail GetRail()
        {
            return rail;
        }

        public bool InStation()
        {
            return inStation;
        }
        public void Depart()
        {
            inStation = false;
        }


    }
}
