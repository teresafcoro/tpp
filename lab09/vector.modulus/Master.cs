using System;
using System.Threading;

namespace TPP.Laboratory.Concurrency.Lab09 {

    public class Master {

        private short[] vector;

        private int numberOfThreads;

        public Master(short[] vector, int numberOfThreads) {
            if (numberOfThreads < 1 || numberOfThreads > vector.Length)
                throw new ArgumentException("The number of threads must be lower or equal to the number of elements in the vector.");
            this.vector = vector;
            this.numberOfThreads = numberOfThreads;
        }

        public double ComputeModulus() {
            Worker[] workers = new Worker[this.numberOfThreads];
            int itemsPerThread = this.vector.Length/numberOfThreads;
            for(int i=0; i < this.numberOfThreads; i++)
                workers[i] = new Worker(this.vector, 
                    i*itemsPerThread, 
                    (i<this.numberOfThreads-1) ? (i+1)*itemsPerThread-1: this.vector.Length-1 // last one
                    ); // Worker -> Tarea a realizar

            Thread[] threads = new Thread[workers.Length]; // Hilos que permitirán que se ejecuten los workers
            for(int i=0;i<workers.Length;i++) {
                threads[i] = new Thread(workers[i].Compute); // Parte paralelizable ; Composición de Thread y Worker
                threads[i].Name = "Worker Vector Modulus " + (i+1); 
                threads[i].Priority = ThreadPriority.BelowNormal; 
                threads[i].Start();
            }

            // * Solución: Esperar a que terminen todos los Worker para poder continuar
            // una vez ya están todos lanzados (por eso es otro bucle)
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            // Partes no paralelizables

            long result = 0;
            foreach (Worker worker in workers) {
                result += worker.Result;
            }
            return Math.Sqrt(result);
        }

    }

    /* Condición de carrera...
     * Porque se consulta el valor antes de ser producido.
     * Se debe esperar a que lo esté.
     * Usar Join (no siempre soluciona la condición de carrera...).
     * 
     * Depuración:
     *  - Poner puntos de interrupción en los puntos conflictivos.
     *  - Abrir ventana de subprocesos.
     *  - Click derecho en ella y seleccionar mostrar subprocesos en código fuente.
     *      (O seleccionarlo al lado de las flechas para continuar con el debug).
     *  - Tiene que aparecer el icono de los subprocesos.
     */

}
