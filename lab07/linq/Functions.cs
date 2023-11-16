using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace TPP.Laboratory.Functional.Lab07
{

    static public class Functions
    {

        public static IEnumerable<TResult> Map<TElement, TResult>(this IEnumerable<TElement> collection, Func<TElement, TResult> function) {
            /* Implementación previa:
             * 
                TResult[] result = new TResult[collection.Count()];
                uint i = 0;
                foreach (TElement d in collection)
                {
                    result[i] = function(d);
                    i++;
                }
                return result;
            */

            // Nueva implementación -> Usando generadores:
            foreach (TElement d in collection)
            {
                yield return function(d);
            }
        }

        /* public static void ForEach<TElement>(this IEnumerable<TElement> collection, Action<TElement> action)
        {
            foreach (TElement d in collection)
            {
                action(d);
            }            
        } */

        public static IEnumerable<TElement> ForEach<TElement>(this IEnumerable<TElement> collection, Action<TElement> action)
        {
            foreach (TElement d in collection)
            {
                action(d);
                yield return d;
            }            
        }

        public static IEnumerable<TElement> Filter<TElement>(this IEnumerable<TElement> collection, Predicate<TElement> function)
        {
            foreach (TElement d in collection)
            {
                if (function(d))
                    yield return d;
            }
        }

        public static double MyAverage(this IEnumerable<int> collection)
        {
            // int count = collection.Count();
            int count = 0; // Haciendo manualmente el count se evita consumir dos veces la colección
            double total = 0;
            foreach (var x in collection)
            {
                total += x;
                count++;
            }
            return total / count;
        }

        public static IEnumerable<int> MyRandom()
        {
            var random = new Random();
            while (true)
            {
                yield return random.Next(1, 5);
            }
        }
    }

}
