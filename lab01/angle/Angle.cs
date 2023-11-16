using System;

namespace lab01
{
    public class Angle
    {
        private double rads;

        //public double GetRads()
        //{
        //    return rads;
        //}

        //public double GetDegs()
        //{
        //    return rads / Math.PI * 180;
        //}

        public double Rads { get { return rads; } }
        public double Degs { get { return rads / Math.PI * 180; } }

        public Angle(double rads)
        {
            Console.WriteLine("Constructor");
            this.rads = rads;            
        }

        ~Angle()
        {
            Console.WriteLine("Destructor");
        }

        public double Sin()
        {
            return Math.Sin(rads);
        }

        public double Cos()
        {
            return Math.Cos(rads);
        }

        public double Tan()
        {
            return Sin() / Cos();
        }
    }
}
