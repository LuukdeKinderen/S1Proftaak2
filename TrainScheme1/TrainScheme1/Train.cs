using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheme1
{
    class Train
    {
        public string name;
        private bool intercity;
        private bool right;
        private Rail rail;

        private Station[] route;
        private int nextDestination = 0;
        private bool inStation;


        public Train(string name, bool intercity, bool right, Station[] route,Rail rail)
        {
            this.name = name;
            this.intercity = intercity;
            this.right = right;
            this.route = route;
            this.rail = rail;
        }

        public void SetRail(Rail rail)
        {
            this.rail.RemoveTrain(this);
            this.rail = rail;
            this.rail.AddTrain(this);
            //if (rail.GetStation() == route[nextDestination])
            //{
            //    inStation = true;
            //    rail.GetStation().AddTrain(this);
            //    nextDestination++;
            //    if(nextDestination == route.Length-1)
            //    {
            //        nextDestination = 0;
            //    }
            //}
        }
        public void SetNextDestination()
        {
            nextDestination++;
            if (nextDestination == route.Length - 1)
            {
                nextDestination = 0;
            }
        }
        

        public Rail DestinationRail()
        {
            return route[nextDestination].GetRail();
        }
        public bool GoingRight()
        {
            return rail.GetIndex() < DestinationRail().GetIndex();
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
        public void Arrive()
        {
            inStation = true;
        }

    }
}
