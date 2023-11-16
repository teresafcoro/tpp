using System;

namespace lab04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var x = 5;
            var y = 7;
            Swap(ref x, ref y);
            Console.WriteLine($"x={x}, y={y}");

            int z = (int) Max(x, y);
            Console.WriteLine($"Max({x},{y})={z}");
        }


        static void Swap<T>(ref T a, ref T b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }

        static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

        /*
         * Anteriores formas de implementar el método Max (erroneas):
         * 
            static IComparable<T> Max<T>(IComparable<T> a, IComparable<T> b)
            {
	            return a.CompareTo((T) b) > 0 ? a : b;
            }

            static T Max<T>(IComparable<T> a, IComparable<T> b)
            {
	            return (T) (a.CompareTo((T) b) > 0 ? a : b);
            }
        */
    }
}
