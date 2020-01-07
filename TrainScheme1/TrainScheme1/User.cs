using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheme1
{
    class User
    {
        private string UID;

        private double balans;
        private Station checkIn = null;
        private Station checkUit = null;

        public User(string UID, int bal)
        {
            this.balans = bal;
            this.UID = UID;
        }

        public User(string UID)
        {
            this.balans = 20;
            this.UID = UID;
        }

        public string checken(Station bufferStation)
        {
            if (checkIn == null)
            {
                InCheck(bufferStation);
                return UID + " ingechecked op " + bufferStation.name;
            }
            else
            {
                UitCheck(bufferStation);
                return UID + " uitgechecked op: " + bufferStation.name + "\nNieuw saldo: " + balans;
            }
        }

        private void InCheck(Station bufferStationIn)
        {
            checkIn = bufferStationIn;
        }

        private void UitCheck(Station bufferStationUit)
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
            checkIn = null;
            checkUit = null;
        }

        public override string ToString()
        {
            return UID;
        }
    }
}
//bedrag ophalen, welke trein, weten waar ingecheckt, weten waar uitgecheckt, bedrag uitrekenen
