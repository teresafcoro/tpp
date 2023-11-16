using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TPP.Laboratory.Concurrency.Lab11 {

    /** 
     * Cambiar a Release
     * Funciona inicialmente de manera secuencial
     * Pasarlo a concurrente / paralelizarlo
     * ¿Qué parte es paralelizable? Transformaciones de las imágenes
     * Elemento con el que trabajar: En paralelo con cada fichero
     * ¿Productor-Consumidor o Master-Worker? Master-Worker
     * Usando TPL (Task Paralell Library): For, Foreach e Invoke
     * https://learn.microsoft.com/es-es/dotnet/api/system.threading.tasks.parallel?view=net-7.0
     */
    class Program {

        static void Main() {
            Stopwatch chrono = new Stopwatch();
            string[] files = Directory.GetFiles(@"..\..\..\..\pics", "*.jpg");
            string newDirectory = @"..\..\..\..\pics\rotated";
            Directory.CreateDirectory(newDirectory);

            // Secuencial:
            chrono.Start();
            foreach (string file in files) {
                string fileName = Path.GetFileName(file);
                using (Bitmap bitmap = new Bitmap(file)) {
                    // Evitar la e/s a consola
                    // Console.WriteLine("Processing the file \"{0}\".", fileName);
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDirectory, fileName));
                }
            }
            chrono.Stop();
            Console.WriteLine("Elapsed time: {0:N} milliseconds.", chrono.ElapsedMilliseconds);

            // Concurrente:
            chrono.Restart(); // Reset + Start!!! Con solo Start no va a funcionar...
            Parallel.ForEach(files, (string file) =>
            {
                string fileName = Path.GetFileName(file);
                using (Bitmap bitmap = new Bitmap(file))
                {
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDirectory, fileName));
                }
                // ¿Recurso compartido? No parece... Funciona (:
            });
            chrono.Stop();
            Console.WriteLine("Elapsed time: {0:N} milliseconds.", chrono.ElapsedMilliseconds);

            // Concurrente (con recurso compartido y, por tanto, lock):
            var threads = new HashSet<int>(); // Creo un conjunto compartido
            // Las listas pueden tener elementos repetidos, los conjuntos no
            chrono.Restart();
            Parallel.ForEach(files, (string file) =>
            {
                lock (threads) // Recurso compartido
                    threads.Add(Thread.CurrentThread.ManagedThreadId); // id de cada hilo
                string fileName = Path.GetFileName(file);
                using (Bitmap bitmap = new Bitmap(file))
                {
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDirectory, fileName));
                }

            });
            chrono.Stop();
            Console.WriteLine("Elapsed time: {0:N} milliseconds.", chrono.ElapsedMilliseconds);
            Console.WriteLine("Using {0} threads.", threads.Count);

            // Para medir tiempos se debería usar la opcón sin lock ya que no tendría sentido...
        }

    }

}
