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

        private WebRequest request = new WebRequest("http://192.168.50.6");

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
            this.balans = request.GetData(this.UID);

            if (this.balans >= 15)
            {
                checkIn = bufferStationIn;
            } else
            {
                Console.WriteLine("Te weinig instaptarief");
                checkIn = null;
            }
            

        }

        private void UitCheck(Station bufferStationUit)
        {
            this.balans = request.GetData(this.UID);
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
            request.SendGetData(this.UID, this.balans.ToString());

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
