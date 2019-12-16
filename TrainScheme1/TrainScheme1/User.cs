using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheme1
{
    class User
    {
        private double balans;
        private Station checkIn;
        private Station checkUit;

        public void InCheck(Station bufferStationIn)
        {
            checkIn = bufferStationIn;
        }

        public void UitCheck(Station bufferStationUit)
        {
            
            checkUit = bufferStationUit;

            int checkinIndex = checkIn.GetRailIndex();
            int checkuitIndex = checkUit.GetRailIndex();
            int verschil = 0;
            if (checkinIndex > checkuitIndex)
            {
                verschil = checkinIndex - checkuitIndex;
            }
            else
            {
                verschil = checkuitIndex - checkinIndex;
            }
            balans -= verschil * 0.1;

        }

        private double GetBalans()
        {

            return placeholder;
        }

    }
}
//bedrag ophalen, welke trein, weten waar ingecheckt, weten waar uitgecheckt, bedrag uitrekenen
