using System;
using System.Threading;
using list;

namespace master
{
    public class Master<T>
    {
        private ConcurrentQueue<T> queue;
        private uint numberOfThreads;

        public Master(ConcurrentQueue<T> queue, uint numberOfThreads)
        {
            if (numberOfThreads < 1 || numberOfThreads > queue.NumberOfElements())
                throw new ArgumentException("The number of threads must be lower or equal to the number of elements in the queue.");
            this.queue = queue;
            this.numberOfThreads = numberOfThreads;
        }

        public double ComputeModulus()
        {
            Worker<T>[] workers = new Worker<T>[numberOfThreads];
            int elementsPerThread = (int)(queue.NumberOfElements() / numberOfThreads);
            for (int i = 0; i < this.numberOfThreads; i++)
                workers[i] = new Worker<T>(queue,
                    i * elementsPerThread,
                    (i < numberOfThreads - 1) ? (i + 1) * elementsPerThread - 1 : (int)(queue.NumberOfElements() - 1) // last one
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
            long result = 0;
            foreach (Worker<T> worker in workers)
                result += worker.Result;
            return result;
        }
    }
}
