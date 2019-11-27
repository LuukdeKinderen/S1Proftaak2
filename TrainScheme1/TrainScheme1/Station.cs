﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TrainScheme1
{
    class Station
    {
        public string name { get; }
        private bool centralStation;
        private List<Train> trains = new List<Train>();
        private List<int> trainWaitTime = new List<int>();
        private Rail rail;

        public Station(string name, bool centralStation)
        {
            this.name = name;
            this.centralStation = centralStation;
        }

        public void AddTrain(Train train)
        {
            trains.Add(train);
            int i = centralStation?20:10;
            trainWaitTime.Add(i);
        }
        public void DepartTrain(Train train)
        {
            train.Depart();
            trainWaitTime.RemoveAt(trains.IndexOf(train));
            trains.Remove(train);
        }

        public void SetRail(Rail rail)
        {
            this.rail = rail;
        }

        public Rail GetRail()
        {
            return rail;
        }

        public bool ReadytoDepart(Train train)
        {
            trainWaitTime[trains.IndexOf(train)]--;
            return trainWaitTime[trains.IndexOf(train)] < 0;
        }

        public bool CentralStation()
        {
            return centralStation;
        }

    }
}
