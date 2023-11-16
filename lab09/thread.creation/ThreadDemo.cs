using System;
using System.Threading;
using System.Text;

namespace TPP.Laboratory.Concurrency.Lab09 {

    class ThreadDemo {

        static void Main(string[] args) {
            if (args.Length > 0) {
                int numberOfThreads = Convert.ToInt32(args[0]);
                for (int i = 0; i < numberOfThreads; i++) 
                    new Thread(new MessageThread().Run).Start();
                // Start() inicia los hilos, Run es la accion de estos
            }
            new MessageThread().Run();  // Hilo principal
        }

    }
}
