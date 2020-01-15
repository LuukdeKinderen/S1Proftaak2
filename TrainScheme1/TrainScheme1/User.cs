using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
            this.balans = 2000;
            this.UID = UID;
        }

        public string checken(Station bufferStation)
        {
            if (checkIn == null)
            {
                if (InCheck(bufferStation))
                {

                    return "ingechecked";
                }
                else
                {
                    return "saldo";
                }
            }
            else
            {
                UitCheck(bufferStation);
                return "uitgechecked";
            }
        }

        private bool InCheck(Station bufferStationIn)
        {
            this.balans = request.GetData(this.UID);
            
            if (this.balans >= 1500)
            {
                checkIn = bufferStationIn;
                return true;
            } else
            {
                checkIn = null;
                return false;
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

            balans -= verschil * 10;
            request.SendGetData(this.UID, this.balans.ToString(),"0");

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
