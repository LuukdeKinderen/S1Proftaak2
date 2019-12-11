using System;
using System.Collections.Generic;
using System.Drawing;
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

        public Color GetColor()
        {
            Color c = train.GetColor();
            switch (croudLevel)
            {
                case CroudLevel.L:
                    return ChangeColorBrightness(c, -.5f);
                case CroudLevel.M:
                    return ChangeColorBrightness(c, -.3f);
                case CroudLevel.H:
                    return ChangeColorBrightness(c, -.1f);
            }
            return c;
        }

        public Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }


    }
}
