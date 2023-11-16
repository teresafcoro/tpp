using System;
using System.Diagnostics;

namespace list
{
    public class ConcurrentQueue<T> // Cola thread safe
    {
        private List<T> list = new List<T>();
        public bool IsEmpty() { return list.NumberOfElements == 0; }
        public uint NumberOfElements() { return list.NumberOfElements; }

        public void Enqueue(T obj)
        {
            if (obj == null)   // Precondición
                throw new ArgumentException();
            if (IsEmpty())
                list.FirstNode = list.LastNode = new Node<T> { InfoNode = obj };
            else
                list.LastNode = list.LastNode.NextNode = new Node<T> { InfoNode = obj };
            Invariante();
            if (IsEmpty())    // Postcondición
                throw new InvalidOperationException();
        }

        public T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException();
            Node<T> aux = list.FirstNode;
            if (list.NumberOfElements > 1)
                list.FirstNode = list.FirstNode.NextNode;
            Invariante();
            return aux.InfoNode;
        }

        public T Peek() // devuelve el primer objeto de la cola sin eliminarlo
        {
            if (IsEmpty())
                throw new InvalidOperationException();
            return list.FirstNode.InfoNode;
        }

        private void Invariante()
        {
            if (list.NumberOfElements > 1 && list.FirstNode.Equals(null) && list.FirstNode.Equals(list.LastNode))
                Debug.Assert(false, "La cola contiene errores");
            if (list.NumberOfElements > 0 && IsEmpty())
                Debug.Assert(false, "La cola contiene errores");
        }
    }
}
