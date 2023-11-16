using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace TPP.Laboratory.Concurrency.Lab10
{

    internal class Summation {

        protected virtual void Add(IEnumerable<int> collection, int from, int to) {
            if (from > to)
                throw new ArgumentException("From cannot be greater than to");
                        this.value = this.Value + collection.Skip(from).Take(to - from + 1).Aggregate(0L, (a, b) =>
            {
                Console.Write("");
                return a + b;
            });
        }

        /* Segundo problema: Condición de carrera (por value)
         * Sincronizar los hilos para que no accedan a la zona crítica a la vez (línea 14)
         */

        protected long value;
        internal long Value {
            get { return this.value; }
        }

        private int numberOfThreads;
        private IEnumerable<int> vector;

        internal Summation(int elements, int numberOfThreads) {
            if (numberOfThreads > elements)
                throw new ArgumentException("The number of threads is too high");
            this.numberOfThreads = numberOfThreads;
            this.vector = Enumerable.Range(1,elements);
            
        }

        internal long Compute() {
            int elementsPerThread = vector.Count() / numberOfThreads;
            Thread[] threads = new Thread[numberOfThreads];
            // i es una variable libre
            for (int i = 0; i < numberOfThreads; i++) {
                threads[i] = new Thread((object o) => {
                    int i = (int) o;
                    this.Add(this.vector, i * elementsPerThread,
                        i < numberOfThreads - 1 ? // last iteration
                        (i + 1) * elementsPerThread - 1 :
                        vector.Count() - 1);
                }); // Creación de la closure
            }
            for (int i = 0; i < numberOfThreads; i++)
                threads[i].Start(i); // Ejecución de la closure
            foreach (var thread in threads)
                thread.Join();
            return this.Value;
        }

        /* Primer problema: Depende del valor de i
         * Primero se crea la closure y en la línea 46 de ejecuta
         * i residualmente vale 1 al ejecutar la closure
         * Dentro de la closure i debe ser una variable local, le paso un parámetro
         * Desde Start se deberá entonces mandar el valor de i para cada ejecución
         */
    }
        
    internal class SummationLocked : Summation
    {
        internal SummationLocked(int elements, int numberOfThreads) : base(elements, numberOfThreads)
        {
        }

        private object monitor = new();

        protected override void Add(IEnumerable<int> collection, int from, int to)
        {
            if (from > to)
                throw new ArgumentException("From cannot be greater than to");
            
            lock (monitor)
            {
                this.value = this.Value + collection.Skip(from).Take(to - from + 1).Aggregate(0L, (a, b) =>
                {
                    Console.Write("");
                    return a + b;
                });
            }
            // Tercera corrección: Usando Interlocked
            //Interlocked.Add(ref this.value, this.Value + collection.Skip(from).Take(to - from + 1).Aggregate(0L, (a, b) => a + b) => { return a + b; }))
        }
    }

    internal class SummationInterlocked : Summation
    {

        internal SummationInterlocked(int elements, int numberOfThreads) : base(elements, numberOfThreads)
        {

        }

        private object monitor = new object();

        protected override void Add(IEnumerable<int> collection, int from, int to)
        {
            if (from > to)
                throw new ArgumentException("From cannot be greater than to");
            Interlocked.Add(ref this.value, collection.Skip(from).Take(to - from + 1).Aggregate(0L, (a, b) =>
            {
                Console.Write("");
                return a + b;
            }));
        }

    }
}
