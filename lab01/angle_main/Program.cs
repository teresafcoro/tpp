using System;

namespace lab01
{
    class Program
    {
        static void Main()
        {
            Angle a = new Angle(Math.PI);
            Console.WriteLine("Angle (rad): {0,10:F6}", a.Rads); // a.GetRads()
            Console.WriteLine("Angle (deg): {0,10:F2}", a.Degs); // a.GetDegs()
            Console.WriteLine("Sin:         {0,10:F}", a.Sin());
            Console.WriteLine("Cos:         {0,10:F}", a.Cos());
            Console.WriteLine("Tan:         {0,10:F}", a.Tan());
        }
    }
}
