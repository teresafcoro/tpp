using System;
using System.IO;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TPP.Laboratory.Concurrency.Lab11 {

    /** 
     * Cambiar a Release
     * Funciona inicialmente de manera secuencial
     * Pasarlo a concurrente / paralelizarlo
     * ¿Qué parte es paralelizable? Procesamiento del texto
     * Elemento con el que trabajar: En paralelo con cada tarea de procesamiento del texto
     * ¿Productor-Consumidor o Master-Worker? Master-Worker
     * Usando TPL invoke
     * Para distinguir este caso del anterior preguntarnos: ¿Paralelizable por datos o por tareas?
     */
    class Program {

        static void Main(string[] args) {
            // String text = Processing.ReadTextFile(@"..\..\..\clarin.txt"); // Cuidado con la ruta
            String text = Processing.ReadTextFile(@"..\..\..\..\clarin.txt");
            string[] words = Processing.DivideIntoWords(text);
            Stopwatch crono = new Stopwatch();
            
            // Secuencial:
            crono.Start();
            int punctuationMarks = Processing.PunctuationMarks(text);
            var longestWords = Processing.LongestWords(words);
            var shortestWords = Processing.ShortestWords(words);
            int greatestOccurrence, lowestOccurrence;
            var wordsAppearMoreTimes = Processing.WordsAppearMoreTimes(words, out greatestOccurrence);
            var wordsAppearFewerTimes = Processing.WordsAppearFewerTimes(words, out lowestOccurrence);
            crono.Stop();            
            ShowResults(punctuationMarks, shortestWords, longestWords, wordsAppearFewerTimes, lowestOccurrence,
                wordsAppearMoreTimes, greatestOccurrence);            
            Console.WriteLine("\nElapsed time: {0:N} milliseconds.\n", crono.ElapsedMilliseconds);

            // Paralelo:
            crono.Restart();
            Parallel.Invoke( // Usando cláusulas para cada tarea
                () => { punctuationMarks = Processing.PunctuationMarks(text); },
                () => { longestWords = Processing.LongestWords(words); },
                () => { shortestWords = Processing.ShortestWords(words); },
                () => { wordsAppearMoreTimes = Processing.WordsAppearMoreTimes(words, out greatestOccurrence); },
                () => { wordsAppearFewerTimes = Processing.WordsAppearFewerTimes(words, out lowestOccurrence); }
            );
            crono.Stop();
            ShowResults(punctuationMarks, shortestWords, longestWords, wordsAppearFewerTimes, lowestOccurrence,
                wordsAppearMoreTimes, greatestOccurrence);
            Console.WriteLine("\nElapsed time: {0:N} milliseconds.", crono.ElapsedMilliseconds);

            // ¿Recursos compartidos? Sí, todo practicamente pero no hace falta poner locks porque:
            // Porque los accesos a words son de lectura!
            // Condición de carrera: Recurso compartido + accesos DE ESCRITURA (al menos uno) a destiempo a él

            // Como ejemplo...
            // Añado como en el ejercicio anterior la colección para tener un recurso compartido y usar lock
            // Paralelo:
            // Concurrente (con recurso compartido y, por tanto, lock):
            var threads = new HashSet<int>(); // Creo un conjunto compartido
            crono.Restart();
            Parallel.Invoke( // Usando cláusulas para cada tarea
                () => {
                    lock (threads) // Recurso compartido
                        threads.Add(Thread.CurrentThread.ManagedThreadId); // id de cada hilo
                    punctuationMarks = Processing.PunctuationMarks(text);
                },
                () => {
                    lock (threads) // Recurso compartido
                        threads.Add(Thread.CurrentThread.ManagedThreadId); // id de cada hilo
                    longestWords = Processing.LongestWords(words);
                },
                () => {
                    lock (threads) // Recurso compartido
                        threads.Add(Thread.CurrentThread.ManagedThreadId); // id de cada hilo
                    shortestWords = Processing.ShortestWords(words);
                },
                () => {
                    lock (threads) // Recurso compartido
                        threads.Add(Thread.CurrentThread.ManagedThreadId); // id de cada hilo
                    wordsAppearMoreTimes = Processing.WordsAppearMoreTimes(words, out greatestOccurrence);
                },
                () => {
                    lock (threads) // Recurso compartido
                        threads.Add(Thread.CurrentThread.ManagedThreadId); // id de cada hilo
                    wordsAppearFewerTimes = Processing.WordsAppearFewerTimes(words, out lowestOccurrence);
                }
            );
            crono.Stop();
            ShowResults(punctuationMarks, shortestWords, longestWords, wordsAppearFewerTimes, lowestOccurrence,
                wordsAppearMoreTimes, greatestOccurrence);
            Console.WriteLine("\nElapsed time: {0:N} milliseconds.", crono.ElapsedMilliseconds);
            Console.WriteLine("Using {0} threads.", threads.Count);
        }

        public static void ShowResults(int punctuationMarks, string[] shortestWords, string[] longestWords,
                                       string[] wordsAppearFewerTimes, int lowestOccurrence,
                                       string[] wordsAppearMoreTimes, int greatestOccurrence) {
            const int maxNumberOfElementsToShow = 20;

            Console.WriteLine("> There were {0} punctuation marks. ", punctuationMarks);

            Console.Write("> {0} shortest words: ", shortestWords.Count());
            Show(shortestWords, Console.Out, maxNumberOfElementsToShow);
            Console.WriteLine();

            Console.Write("> {0} longest words: ", longestWords.Count());
            Show(longestWords, Console.Out, maxNumberOfElementsToShow);
            Console.WriteLine();

            Console.Write("> {0} words appeared fewer times ({1}): ", wordsAppearFewerTimes.Count(), lowestOccurrence);
            Show(wordsAppearFewerTimes, Console.Out, maxNumberOfElementsToShow);
            Console.WriteLine();

            Console.Write("> {0} words appreared more times ({1}): ", wordsAppearMoreTimes.Count(), greatestOccurrence);
            Show(wordsAppearMoreTimes, Console.Out, maxNumberOfElementsToShow);
            Console.WriteLine();

            // Para la tarea de casa:
            // Mejor paralelizar en el bucle de más hacia fuera
            /* Ejemplo:
             * for... <- mejor paralelizar el for! a paralelizar el metodo o ambos
             *  ...();
             * 
             */
        }

        private static void Show<T>(IEnumerable<T> collection, TextWriter stream, int maxNumberOfElements) {
            int i = 0;
            foreach (T element in collection) {
                stream.Write(element);
                i = i + 1;
                if (i == maxNumberOfElements) {
                    stream.Write("...");
                    break;
                }
                if (i < collection.Count())
                    stream.Write(", ");
            }
        }

    }

}
