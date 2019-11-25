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
        private int[] spoor = new int[120];



        static void Main(string[] args)
        {

            stations = new List<Station>{
                new Station("CL",0,true),
                new Station("KF",24,false),
                new Station("FT",40,true),
                new Station("VH",70,false),
                new Station("KP",90,false),
                new Station("JP",116,true),
            };
            trains = new List<Train>
            {
               new Train("S1",false,new Station[]{ stations[1], stations[2], stations[1], stations[0]},0,true,stations[0]),
               new Train("S1",false,new Station[]{ stations[1], stations[2], stations[1], stations[0]},0,true,stations[0]),
               new Train("S1",false,new Station[]{ stations[1], stations[2], stations[1], stations[0]},0,true,stations[0]),
               new Train("S1",false,new Station[]{ stations[1], stations[2], stations[1], stations[0]},0,true,stations[0]),
               new Train("S1",true,new Station[]{ stations[1], stations[2], stations[1], stations[0]},0,true,stations[0]),
               new Train("S1",false,new Station[]{ stations[1], stations[2], stations[1], stations[0]},0,true,stations[0]),
               new Train("S1",false,new Station[]{ stations[1], stations[2], stations[1], stations[0]},0,true,stations[0]),
               //new Train("S1",false,new int[]{1,2,1,0},0,true,stations[0]),
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

                    if (i ==stations[p].pos)
                    {
                        rails[i] = stations[p].name;

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
                    if (i == train.pos)
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

        /* christopherlaan,
          knapford,
          frowtastic,
          vHoofstraat,
          kinderpaleis,
          jethoeplein
          */



        

        //struct WayPoint
        //{
        //    public Station station { get; }
        //    public bool turn { get; }

        //    public WayPoint(Station station) : this()
        //    {
        //        this.station = station;
        //    }

        //    public WayPoint(Station station, bool turn) : this(station)
        //    {
        //        this.turn = turn;
        //    }
        //}
    }
}
