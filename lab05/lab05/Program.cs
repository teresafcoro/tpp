using System;
using System.Collections.Generic;
using System.Linq;

namespace delegates
{
    static class Program
    {
        static void Main(string[] args)
        {
            double[] values = new double[] { -3, -2, -1, 1, 2, 3 };
            values.Map(Square).Show();
            values.Map(x => x * x).Show(); // función lambda
            values.Select(x => x * x).Show(); // using System.Linq;

            new string[] { "hola", "mundo" }.Map(x => x.Length).Show();
        }

        // Definición del delegado
        delegate double Function(double x);

        // Definición de un métoco con la signatura del delegado
        private static double Square(double x)
        {
            return x * x;
        }

        /* Función Map:
         *  Recibe una colección y un delegado.
         *  Le aplica la función (delegado) a los elementos de la colección.
         *  Retorna una nueva colección con los elementos transformados.
         */

        /* Inicialmente definimos el delegado:
         * static IEnumerable<TRange> Map<TDomain, TRange>(this IEnumerable<TDomain> collection, Function transformation)
         */

        static IEnumerable<TRange> Map<TDomain, TRange>(this IEnumerable<TDomain> collection, Func<TDomain, TRange> transformation)
        {
            TRange[] result = new TRange[collection.Count()];
            uint i = 0;
            foreach (TDomain x in collection)
            {
                result[i] = transformation(x);
                i++;
            }
            return result;
        }

        static void Show<T>(this IEnumerable<T> collection)
        {
            foreach (T element in collection)
            {
                Console.Write($"{element} ");
            }
            Console.WriteLine();
        }
    }
}
