using System;

namespace TPP.Laboratory.ObjectOrientation.Lab01
{

    /// <summary>
    /// Class that computes the power of a number
    /// </summary>
    class Power
    {

        static void Main()
        {
            uint theBase = 2;
            uint exponent = 32;

            // Con uint no puede almacenar el valor a retornar tras el cálculo, overflow.
            // En consola se muestra por eso un 0.
            // Antes: uint result = 1;
            // Cambio realizado para arreglarlo:
            ulong result = 1;

            if (theBase == 0)
            {
                Console.WriteLine("Power: 0.");
                return;
            }

            while (exponent > 0)
            {
                result *= theBase; // Punto de interrupción, se le puede añadir una condición (exponente == 2).
                exponent--;
            }

            Console.WriteLine("Power: {0}.", result);
        }
    }

}
