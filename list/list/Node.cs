using System;

namespace list
{
    /// <summary>
    /// Clase interna Node con la información del nodo de la lista
    /// </summary>
    public class Node<T>
    {
        internal T InfoNode { get; set; }
        internal Node<T> NextNode { get; set; }

        ~Node()
        {
            Console.WriteLine("Ejecutando destructor de la clase Node");
        }

        public override string ToString()
        {
            return string.Format("Node: {0}", InfoNode);
        }
    }
}
