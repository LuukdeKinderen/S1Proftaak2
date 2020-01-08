using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace TrainScheme1
{
    class WebRequest
    {
        private static readonly HttpClient client = new HttpClient();

        string baseIP;
        string file;

        public WebRequest(string bufferBaseIP)
        {
            baseIP = bufferBaseIP + "/"; // 192.168.50.6/
        }

        public double SendGetData(string UID, string bal)
        {
            string checkByte = "**TByte*";
            int[] multiplyVal = { 100000000, 10000000, 1000000, 100000, 10000, 1000, 100, 10, 1 };

            file = "fetch.php?";
            string payloadString = baseIP + file + $"uid={UID}&bal={bal}";
            string response = RequestAsync(payloadString).Result;

            bool TBFound = true;
            for (int i = 0; i < 8; i++)
            {
                if (response[i] != checkByte[i])
                {
                    TBFound = false;
                }
            }

            if (!TBFound)
            {
                return 0;
            }

            int balLen = response[10] - '0';
            char[] balString = { '0', '0', '0', '0', '0', '0', '0', '0', '0' };

            for (int i = 0; i < balLen; i++)
            {
                balString[(9 - balLen) + i] = response[i + 12];
            }

            double balVal = 0;
            for (int i = 0; i < 9; i++)
            {
                balVal = balVal + ((balString[i] - '0') * multiplyVal[i]);
            }

            return balVal;
        }


        public double GetData(string bufferUID)
        {
            return SendGetData(bufferUID, "-404");
        }


        public List<string> GetUserList()
        {
            List<string> dataList = new List<string> { };
            string bufferUid = "";
            string bufferBal = "";
            bool uidFound = false;

            string payloadString = baseIP + "fetchUserList.php";
            string response = RequestAsync(payloadString).Result;

            for (int i = 0; i < response.Length; i++)
            {
                if (response[i] != '_')
                {
                    if (!uidFound)
                    {
                        bufferUid += response[i];
                    }
                    else
                    {
                        if (response[i] != ';')
                        {
                            bufferBal += response[i];
                        }
                        else
                        {
                            dataList.Add(bufferBal);
                            uidFound = false;
                            bufferBal = "";
                        }
                    }
                }
                else
                {
                    dataList.Add(bufferUid);
                    uidFound = true;
                    bufferUid = "";
                }
            }
            return dataList;
        }


        public void NewUser(string bufferUID, string bufferBal, string bufferIO)
        {
            string payloadString = baseIP + $"newEntry.php?uid={bufferUID}&bal={bufferBal}";
            string response = RequestAsync(payloadString).Result;
        }



        public async Task<string> RequestAsync(string bufferPayloadString)
        {
            string response = client.GetStringAsync(bufferPayloadString).Result;
            response = response.Trim();
            return response;
        }

    }
}
