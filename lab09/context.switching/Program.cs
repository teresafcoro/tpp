using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TPP.Laboratory.Concurrency.Lab09 {

    class Program {

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        static void Main(string[] args) {
            const int maxNumberOfThreads = 50;
            short[] vector = VectorModulusProgram.CreateRandomVector(100000, -10, 10);
            ShowLine(Console.Out, "Number of threads", "Ticks", "Result");
            for (int numberOfThreads = 1; numberOfThreads <= maxNumberOfThreads; numberOfThreads++) {
                Master master = new Master(vector, numberOfThreads);
                long before = 0;
                QueryPerformanceCounter(out before);
                double result = master.ComputeModulus();
                long after = 0;
                QueryPerformanceCounter(out after);
                ShowLine(Console.Out, numberOfThreads, (after - before), result);
                // No tiene por qué pasar, se le sugiere que lo haga ya que terminamos la medición
                GC.Collect(); // Garbage collection ; Recolector de basura ; Es un hilo
                GC.WaitForFullGCComplete(); // Similar al Join
            }
        }

        private const string CSV_SEPARATOR = ";";

        static void ShowLine(TextWriter stream, string numberOfThreadsTitle, string ticksTitle, string resultTitle) {
            stream.WriteLine("{0}{3}{1}{3}{2}{3}", numberOfThreadsTitle, ticksTitle, resultTitle, CSV_SEPARATOR);
        }

        static void ShowLine(TextWriter stream, int numberOfThreads, long ticks, double result) {
            stream.WriteLine("{0}{3}{1:N0}{3}{2:N2}{3}", numberOfThreads, ticks, result, CSV_SEPARATOR);
        }
    }

    /* Pasos a seguir:
     * Modo release.
     * ReCompilar solución.
     * En cmd (/bin) ejecutar varias veces el c.s.exe.
     * Ejecutar varias veces redireccionando a un fichero ; c.s.exe > output.csv.
     * En Excel se puede crear una gráfica con las dos primes cols.
     * NO guardar en formato .csv o no se guardarán las modificaciones hechas
     */

}
