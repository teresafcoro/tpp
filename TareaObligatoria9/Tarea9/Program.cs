using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Lab09
{
    class Program
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        /*
         * This program processes Bitcoin value information obtained from the
         * url https://api.kraken.com/0/public/OHLC?pair=xbteur&interval=5.
         */
        static void Main(string[] args)
        {
            var data = Utils.GetBitcoinData();
            //foreach (var d in data)
            //Console.WriteLine(d);

            Master master = new Master(data, 1, 7000);
            DateTime before = DateTime.Now;
            int result = master.ComputeModulus();
            DateTime after = DateTime.Now;
            Console.WriteLine("Result with 1 thread: {0}.", result);
            Console.WriteLine("Elapsed time: {0:N0} ticks.", (after - before).Ticks);

            master = new Master(data, 4, 7000);
            before = DateTime.Now;
            result = master.ComputeModulus();
            after = DateTime.Now;
            Console.WriteLine("Result with 4 threads: {0}.", result);
            Console.WriteLine("Elapsed time: {0:N0} ticks.", (after - before).Ticks);

            const int maxNumberOfThreads = 50;
            ShowLine(Console.Out, "Number of threads", "Ticks", "Result");
            for (int numberOfThreads = 1; numberOfThreads <= maxNumberOfThreads; numberOfThreads++)
            {
                master = new Master(data, numberOfThreads, 7000);
                long before_time = 0;
                QueryPerformanceCounter(out before_time);
                result = master.ComputeModulus();
                long after_time = 0;
                QueryPerformanceCounter(out after_time);
                ShowLine(Console.Out, numberOfThreads, (after_time - before_time), result);
                GC.Collect();
                GC.WaitForFullGCComplete();
            }
        }

        private const string CSV_SEPARATOR = ";";

        static void ShowLine(TextWriter stream, string numberOfThreadsTitle, string ticksTitle, string resultTitle)
        {
            stream.WriteLine("{0}{3} {1}{3} {2}{3}", numberOfThreadsTitle, ticksTitle, resultTitle, CSV_SEPARATOR);
        }

        static void ShowLine(TextWriter stream, int numberOfThreads, long ticks, double result)
        {
            stream.WriteLine("{0}{3} {1:N0}{3} {2:N2}{3}", numberOfThreads, ticks, result, CSV_SEPARATOR);
        }
    }
}
