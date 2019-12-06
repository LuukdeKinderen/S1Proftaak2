using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheme1
{
    enum CroudLevel
    {
        L,
        M,
        H
    }
    class Wagon
    {
        Random r = new Random();

        private CroudLevel croudLevel;
        private Rail rail;
        private Train train;

        public Wagon(Rail rail, Train trian)
        {
            Random r = new Random();
            SetRandomCroudLevel(r.Next(0,3));
            this.rail = rail;
            this.train = trian;
        }

        public void SetRandomCroudLevel(int level)
        {
            int randomCroudLevel = level;
            croudLevel = (CroudLevel)randomCroudLevel;
        }

        public void AddPerson()
        {
            //get Current
            int currentCroudLevel = (int)croudLevel;
            //add one
            currentCroudLevel++;
            //Set max
            currentCroudLevel = currentCroudLevel > 2 ? 2 : currentCroudLevel;

            //set
            croudLevel = (CroudLevel)currentCroudLevel;
        }

        public CroudLevel GetCroudLevel()
        {
            return croudLevel;
        }

        public Rail GetRail()
        {
            return rail;
        }

        public Train Gettrain()
        {
            return train;
        }


        public void SetRail(Rail rail)
        {
            this.rail = rail;
        }

        public string GetHEX()
        {
            switch (croudLevel)
            {
                case CroudLevel.L:
                    return "00ff00";
                    break;
                case CroudLevel.M:
                    return "fbff00";
                    break;
                case CroudLevel.H:
                    return "ff0000";
                    break;
            }
            return "";
        }


    }
}
