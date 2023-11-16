using System;
using System.Threading;

namespace Lab09
{
    public class Master
    {
        private BitcoinValueData[] data;
        private int numberOfThreads;
        private int valor;

        public Master(BitcoinValueData[] data, int numberOfThreads, int valor)
        {
            if (numberOfThreads < 1 || numberOfThreads > data.Length)
                throw new ArgumentException("The number of threads must be lower or equal to the number of elements in the vector.");
            this.data = data;
            this.numberOfThreads = numberOfThreads;
            this.valor = valor;
        }

        public int ComputeModulus()
        {
            Worker[] workers = new Worker[numberOfThreads];
            int itemsPerThread = data.Length / numberOfThreads;
            for (int i = 0; i < numberOfThreads; i++)
                workers[i] = new Worker(data,
                    i * itemsPerThread,
                    (i < numberOfThreads - 1) ? (i + 1) * itemsPerThread - 1 : data.Length - 1, // last one
                    valor
                    );

            Thread[] threads = new Thread[workers.Length];
            for (int i = 0; i < workers.Length; i++)
            {
                threads[i] = new Thread(workers[i].Compute);
                threads[i].Name = "Worker Vector Modulus " + (i + 1);
                threads[i].Priority = ThreadPriority.BelowNormal;
                threads[i].Start();
            }
            foreach (Thread thread in threads)
                thread.Join();
            int result = 0;
            foreach (Worker worker in workers)
                result += worker.Result;
            return result;
        }
    }
}
