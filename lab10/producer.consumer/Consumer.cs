using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPP.Laboratory.Concurrency.Lab10
{

    class Consumer {

        private Queue<Product> queue;

        public Consumer(Queue<Product> queue) {
            this.queue = queue;
        }

        public void Run() {
            Random random = new Random();
            while (true) {
                Console.WriteLine("- Dequeuing a product...");
                Product product = null;
                while (product == null)
                {
                    lock (queue)
                    {
                        if (queue.Count > 0)
                            product = queue.Dequeue(); // zona crítica
                    }
                    if (product == null)
                        Thread.Sleep(100);
                    Console.WriteLine("- Dequeued {0}.", product);
                    Thread.Sleep(random.Next(300, 700));
                }
            }
        }

    }
}
