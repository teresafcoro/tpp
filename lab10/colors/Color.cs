using System;

namespace TPP.Laboratory.Concurrency.Lab10
{

    public class Color {

        private ConsoleColor color;

        public Color(ConsoleColor color) {
            this.color = color;
        }

        virtual public void Show() {
            lock (Console.Out)
            {
                ConsoleColor previousColor = Console.ForegroundColor;
                Console.ForegroundColor = this.color;
                Console.Write("{0}\t", this.color);
                Console.ForegroundColor = previousColor;
            }
        }

        /* Problema: Los colores no se corresponden con el nombre del color
         * Están accediendo al mismo recurso simultáneamente varios colores/hilos
         * Solución: Usar lock
         */

    }
}
