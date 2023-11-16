using System;
using System.Collections.Generic;
using System.Linq;

namespace prac05
{
    public static class Delegates
    {
        /* Map:
         * Recibe una colección y un delegado.
         * Le aplica la función (delegado) a los elementos de la colección.
         * Retorna una nueva colección con los elementos transformados.
         */
        public static IEnumerable<TRange> Map<TDomain, TRange>(this IEnumerable<TDomain> collection, Func<TDomain, TRange> transformation)
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

        /* Find:
         * A partir de una colección de elementos, nos devuelve el primero que cumpla un criterio dado,
         * o su valor por defecto en caso de no existir.
         */
        public static TDomain Find<TDomain>(this IEnumerable<TDomain> collection, Predicate<TDomain> function)
        {
            foreach (TDomain x in collection)
            {
                if (function(x))
                    return x;
            }
            return default;
        }

        /* Filter:
         * A partir de una colección de elementos, nos devuelve todos aquellos que cumplan un criterio dado
         * (siendo éste parametrizable).
         */
        public static IEnumerable<TDomain> Filter<TDomain>(this IEnumerable<TDomain> collection, Predicate<TDomain> function)
        {
            TDomain[] result = new TDomain[collection.Count()];
            int elems = 0;
            foreach (TDomain x in collection)
            {
                if (function(x))
                    result[elems++] = x;
            }
            Array.Resize(ref result, elems);
            return result;
        }

        /*
         * Reduce:
         * Recibe una colección de elementos y una función que recibe un primer parámetro del tipo que queremos
         * obtener y un segundo parámetro del tipo de la colección.
         * Su tipo devuelto es el propio del que queremos obtener.
         */
        public static TRange Reduce<TDomain, TRange>(this IEnumerable<TDomain> collection, Func<TRange, TDomain, TRange> function, TRange optional = default)
        {
            foreach (TDomain x in collection)
                optional = function(optional, x);
            return optional;
        }

        public static void Show<T>(this IEnumerable<T> collection)
        {
            foreach (T element in collection)
                Console.Write($"{element} ");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            double[] values = new double[] { -3, -2, -1, 1, 2, 3 };
            values.Map(x => x * x).Show(); // función lambda
            values.Select(x => x * x).Show(); // using System.Linq;

            new string[] { "hola", "mundo" }.Map(x => x.Length).Show();

            Show(Map(new double[] { 1, 8, 3, 5 }, d => d * 2));
        }
    }
}
