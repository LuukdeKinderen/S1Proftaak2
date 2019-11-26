using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TreinSchemaCreator
{
    class Program
    {
        static List<Station> stations;
        static List<Train> trains;
        public static List<Rail> rails = new List<Rail>();



        static void Main(string[] args)
        {
            for (int i = 0; i < 120; i++)
            {
                rails.Add(new Rail(i));
            }
            stations = new List<Station>(){
                new Station("CL",rails[0],true),
                new Station("KF",rails[24],false),
                new Station("FT",rails[40],true),
                new Station("VH",rails[70],false),
                new Station("KP",rails[90],false),
                new Station("JP",rails[116],true),
            };
            trains = new List<Train>
            {
               new Train("S1",false,new Station[]{ stations[1], stations[2], stations[1], stations[0]},0,true,rails[0]),
               new Train("S2",false,new Station[]{ stations[1], stations[2], stations[1], stations[0]},0,true,rails[0]),
               new Train("S3",false,new Station[]{ stations[1], stations[2], stations[1], stations[0]},0,true,rails[0]),
               new Train("S4",false,new Station[]{ stations[1], stations[2], stations[1], stations[0]},0,true,rails[0]),
               new Train("I1",true,new Station[]{ stations[2], stations[5], stations[2], stations[0]},0,true,rails[0]),
               new Train("S5",false,new Station[]{ stations[1], stations[2], stations[1], stations[0]},0,true,rails[0]),
               new Train("S6",false,new Station[]{ stations[1], stations[2], stations[1], stations[0]},0,true,rails[0]),
            };

            do
            {

                DrawTrains();
                for (int s = 0; s < stations.Count; s++)
                {
                   stations[s].CheckForDeparcures();
                }

                for (int i = 0; i < trains.Count; i++)
                {
                    trains[i].Move();
                }
            } while (true);

            

            Console.ReadLine();

        }


        public static void DrawTrains()
        {
            string[] rails = new string[120];

            for (int i = 0; i < 120; i++)
            {
                bool Station = false;
                for (int p = 0; p < stations.Count; p++)
                {

                    if (i == stations[p].GetRailPos())
                    {
                        rails[i] = stations[p].GetName();

                        Station = true;
                    }


                }
                rails[i] = Station ? rails[i] + " " : "|| ";
            }

            string[,] tTrains = new string[trains.Count, 120];



            for (int t = 0; t < trains.Count; t++)
            {
                for (int i = 0; i < 120; i++)
                {
                    bool trainhere = false;
                    Train train = trains[t];
                    if (i == train.GetRailPos())
                    {
                        tTrains[t, i] = train.name + " ";
                        trainhere = true;
                    }

                    if (!trainhere)
                    {
                        tTrains[t, i] = "-  ";
                    }
                }
            }


            for (int i = 0; i < rails.Length; i++)
            {
                Console.Write(rails[i]);
            }

            Console.WriteLine();
            for (int t = 0; t < tTrains.GetLength(0); t++)
            {
                for (int i = 0; i < tTrains.GetLength(1); i++)
                {
                    Console.Write(tTrains[t, i]);
                }
                Console.WriteLine();
            }

            System.Threading.Thread.Sleep(100);
            Console.Clear();



        }

    }
}
