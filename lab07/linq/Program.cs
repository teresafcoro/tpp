using System.Collections.Generic;
using System;
using System.Linq;

namespace TPP.Laboratory.Functional.Lab07 {

    class Program {

        static void Main() {
            IEnumerable<int> integers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // integers.Map(x => x + 1); // .Map(x => seguido de una expresión ; o
            
            var x = integers.Map(x =>
            {
                Console.Write($"{x} ");
                return x;
            }).First(); // seguido de un bloque
            Console.WriteLine($"\n{x}");

            /* -> Al invocarlo no muestra lo que se espera (no muestra nada) por consola.
             * Se debe forzar que se ejecute, activar el generador.
             * Añado entonces la invocación al método Last() de System.Linq.
             * También existe el método First() que devuelve el primero y mata al generador,
             * no continúa calculando.
             */

            // Como estamos recorriendo y mostrando mejor usar ForEach (no es un generador):
            /* integers.ForEach(x =>
            {
                Console.Write($"{x} ");
            }); */
            // integers.ForEach(Console.WriteLine);

            // Nueva impl de ForEach (sí es un generador):
            integers.ForEach(x =>
            {
                Console.Write($"x1: {x} ");
            }).Map(x => x+1).ForEach(x =>
            {
                Console.Write($"x2: {x} ");
            }).Last(); // Acordarse de consumir el generador!
            // Va consumiendo de uno en uno los valores de la colección,
            // pasando por todos los generadores (pipeline)

            Console.WriteLine();

            // Ejemplo:
            var avg = integers.ForEach(x =>
            {
                Console.Write($"{x} ");
            }).MyAverage();
            Console.Write($"\navg: {avg} ");
            // Se está consumiendo dos veces el generador...
            // ¿dónde? en ForEach al consumir la colección varias veces (count + for)

            // Otro ejemplo por si se ve mejor...
            avg = Functions.MyRandom().Take(2).ForEach(x =>
            {
                Console.Write($"{x} ");
            }).MyAverage();
            Console.Write($"\navg: {avg} ");
        }
    }
}
