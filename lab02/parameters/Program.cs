using System;

namespace parameters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 5;
            int y = 7;
            Console.WriteLine($"x = {x}; y = {y}");
            Swap(ref x, ref y);
            Console.WriteLine($"Llamada a Swap - Uso 'ref': x = {x}; y = {y}");

            /* 
             * Dos formas de declararlo:
             * 1.
             *      string a, b, c;
             *      AskData(out a, out b, out c);
             * 2.
             */
            AskData(out string a, out string b, out string c);
            Console.WriteLine($"Llamada a AskData - Uso 'out': a = {a}; b= {b}; c = {c}");
        }

        // ref -> Permite lectura y escritura
        static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        // out -> Permite solamente escritura
        static void AskData(out string name, out string surname, out string id)
        {
            Console.Write("Name: ");
            // In -> Fichero de entrada en el que se guarda la entrada por consola del usuario
            name = Console.In.ReadLine();
            Console.Write("Surname: ");
            surname = Console.In.ReadLine();
            Console.Write("Id: ");
            id = Console.In.ReadLine();
        }
    }
}
