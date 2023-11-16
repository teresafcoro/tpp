using System;
using System.Linq;
using list;

namespace use.list
{
    /// <summary>
    /// Clase Program
    /// Ejemplo de uso de la clase List y Stack
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            Console.WriteLine("Creo la lista y está vacia: {0}", list.NumberOfElements == 0);
            Console.WriteLine("Añado un elemento a la lista...");
            list.Add(3);
            Console.WriteLine("ToString de la lista: {0}", list.ToString());
            Console.WriteLine("La lista está vacia: {0}", list.NumberOfElements == 0);
            Console.WriteLine("Añado más elementos a la lista...");
            list.Add(2);
            list.Add(5);
            list.Add(4);
            list.Add(1);
            Console.WriteLine("ToString de la lista: {0}", list.ToString());
            Console.WriteLine("La lista está vacia: {0}", list.NumberOfElements == 0);
            Console.WriteLine("La lista contiene {0} elementos", list.NumberOfElements);
            Console.WriteLine("Elimino el elemento 3 de la lista: {0}", list.Remove(3));
            Console.WriteLine("ToString de la lista: {0}", list.ToString());
            Console.WriteLine("La lista contiene {0} elementos", list.NumberOfElements);
            Console.WriteLine("Elimino el elemento 4 de la lista: {0}", list.Remove(4));
            Console.WriteLine("ToString de la lista: {0}", list.ToString());
            Console.WriteLine("La lista contiene {0} elementos", list.NumberOfElements);
            Console.WriteLine("Obtengo el elemento de la posición 1: {0}", list.GetElement(1));
            Console.WriteLine("Obtengo el elemento de la posición 2: {0}", list.GetElement(2));
            Console.WriteLine("¿Contiene el elemento '4'? {0}", list.Contains(4));
            Console.WriteLine("¿Contiene el elemento '5'? {0}", list.Contains(5));

            foreach(int i in list)
            {
                Console.WriteLine($"i:{i}");
            }

            Console.WriteLine("ForEach de la propia lista:");
            list.ForEach(i => Console.WriteLine($"i:{i}")).Last();

            Console.WriteLine("\n\t--------------------------------------------\n");

            Stack<int> stack = new Stack<int>(4);
            Console.WriteLine("Creo la pila y está vacia: {0}", stack.IsEmpty());
            Console.WriteLine("Añado un elemento a la pila...");
            stack.Push(3);
            Console.WriteLine("ToString de la pila: {0}", stack.ToString());
            Console.WriteLine("La pila está vacia: {0}", stack.IsEmpty());
            Console.WriteLine("Añado más elementos a la pila...");
            stack.Push(2);
            stack.Push(5);
            stack.Push(4);
            Console.WriteLine("ToString de la pila: {0}", stack.ToString());
            Console.WriteLine("La pila está vacia: {0}", stack.IsEmpty());
            Console.WriteLine("La pila está llena: {0}", stack.IsFull());
            Console.WriteLine("Saco un elemento de la pila: {0}", stack.Pop());
            Console.WriteLine("ToString de la pila: {0}", stack.ToString());
            Console.WriteLine("La pila está llena: {0}", stack.IsFull());
        }
    }
}
