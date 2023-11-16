using System;
using System.Diagnostics;

namespace TPP.Laboratory.Concurrency.Lab10
{

    /**
     * Otra forma de crear un Master - Worker!
     */
    class Program {

        static void Main() {
            const int value = 100000;

            Summation summation = new SummationLocked(value, 1);
            Stopwatch chrono = new Stopwatch();
            chrono.Start();
            summation.Compute();
            chrono.Stop();
            Console.WriteLine("Value: {0}. Elapsed milliseconds: {1}.", summation.Value, chrono.ElapsedMilliseconds);

            summation = new SummationLocked(value, 1000);
            chrono.Restart();
            summation.Compute();
            chrono.Stop();
            Console.WriteLine("Value: {0}. Elapsed milliseconds: {1}.", summation.Value, chrono.ElapsedMilliseconds);

            summation = new SummationLocked(value, 1000);
            chrono.Restart();
            summation.Compute();
            chrono.Stop();
            Console.WriteLine("Value: {0}. Elapsed milliseconds: {1}.", summation.Value, chrono.ElapsedMilliseconds);

            summation = new SummationInterlocked(value, 1000);
            chrono.Restart();
            summation.Compute();
            chrono.Stop();
            Console.WriteLine("Value: {0}. Elapsed milliseconds: {1}.", summation.Value, chrono.ElapsedMilliseconds);
        }

    }
}
