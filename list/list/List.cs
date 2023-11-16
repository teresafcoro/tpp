using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace list
{
    /// <summary>
    /// Clase List simplemente enlazada polimórfica, colecciona nodos (Node) de tipo genérico
    /// </summary>
    public class List<T> : IEnumerable<T>
    {
        public uint NumberOfElements
        {
            get
            {
                uint count = 0;
                Node<T> aux = FirstNode;
                while (aux != null)
                {
                    count++;
                    aux = aux.NextNode;
                }
                return count;
            }
        }

        internal Node<T> FirstNode;         // * primer elemento/node de la lista
        internal Node<T> LastNode;          // * ultimo elemento/node de la lista

        public List()
        {
            Invariante();
        }

        public void Add(T elem)
        {
            Invariante();
            if (elem == null)   // Precondición
                throw new ArgumentException("No se puede añadir un elemento nulo");
            uint n = NumberOfElements;
            Node<T> nodeToBeAdded = new Node<T> { InfoNode = elem };
            if (NumberOfElements == 0)
                FirstNode = LastNode = nodeToBeAdded;
            else
                LastNode = LastNode.NextNode = nodeToBeAdded;
            Debug.Assert(NumberOfElements != 0 & NumberOfElements == n + 1,
                    $"Operación erronea: Add({elem})");   // Postcondición
            Invariante();
        }

        public void AddTop(T elem)
        {
            Invariante();
            if (elem == null)   // Precondición
                throw new ArgumentException("No se puede añadir un elemento nulo");
            uint n = NumberOfElements;
            Node<T> nodeToBeAdded = new Node<T> { InfoNode = elem };
            nodeToBeAdded.NextNode = FirstNode;
            FirstNode = nodeToBeAdded;
            Debug.Assert(NumberOfElements != 0 & NumberOfElements == n + 1
                    & FirstNode.InfoNode.Equals(elem), $"Operación erronea: AddTop({elem})");   // Postcondición
            Invariante();
        }

        public bool Remove(T elem)
        {
            Invariante();
            if (NumberOfElements == 0 | elem == null)
                return false; // Precondición
            uint n = NumberOfElements;
            if (FirstNode.InfoNode.Equals(elem))
            {
                FirstNode = FirstNode.NextNode;
                Debug.Assert(n == NumberOfElements + 1, "No se eliminó correctamente"); // Postcondición
                Invariante();
                return true;
            }
            Node<T> prev = FirstNode;
            Node<T> temp = prev.NextNode;
            for (int i = 1; i < NumberOfElements; i++)
            {   // * i = 1 porque ya comprobamos FirstNode
                if (temp.InfoNode.Equals(elem))
                {
                    prev.NextNode = temp.NextNode;
                    Debug.Assert(n == NumberOfElements + 1, "No se eliminó correctamente"); // Postcondición
                    Invariante();
                    return true;
                }
                prev = temp;
                temp = temp.NextNode;
            }
            Invariante();
            return false;
        }

        public T RemoveTop()
        {
            Invariante();
            if (NumberOfElements == 0)  // Precondición
                throw new InvalidOperationException("Lista vacía");
            uint n = NumberOfElements;
            Node<T> tmp = FirstNode;
            FirstNode = FirstNode.NextNode;
            Debug.Assert(n == NumberOfElements + 1, "No se eliminó correctamente"); // Postcondición
            Invariante();
            return tmp.InfoNode;
        }

        public T GetElement(uint pos)
        {
            Node<T> aux = FirstNode;
            uint size = NumberOfElements;
            for (int i = 0; i < size; i++)
            {
                if (i == pos)
                    return aux.InfoNode;
                aux = aux.NextNode;
            }
            throw new ArgumentException("No se pudo acceder a la posición " + pos);
        }

        public bool Contains(T elem)
        {
            if (elem == null)
                throw new ArgumentException("Se ha recibido un elemento nulo");
            Node<T> aux = FirstNode;
            uint size = NumberOfElements;
            for (int i = 0; i < size; i++)
            {
                if (elem.Equals(aux.InfoNode))
                    return true;
                aux = aux.NextNode;
            }
            return false;
        }

        public IEnumerable<P> Map<P>(Func<T, P> function)
        {
            /*
            P[] result = new P[NumberOfElements];
            uint i = 0;
            foreach (var element in this) {
                result[i] = function(element);
                i++;
            }
            return result;
            */
            foreach (var element in this)
                yield return function(element);
        }

        public void Show()
        {
            foreach (T element in this)
                Console.Write($"{element} ");
            Console.WriteLine();
        }

        public T Find(Predicate<T> function)
        {
            foreach (T i in this)
            {
                if (function(i))
                    return i;
            }
            return default;
        }

        public IEnumerable<T> Filter(Predicate<T> function)
        {
            /*
            T[] result = new T[NumberOfElements];
            int elems = 0;
            foreach (T i in this)
            {
                if (function(i))
                    result[elems++] = i;
            }
            Array.Resize(ref result, elems);
            return result;
            */
            foreach (T i in this)
            {
                if (function(i))
                    yield return i;
            }
        }

        public P Reduce<P>(Func<T, P, P> function, P semilla = default)
        {
            foreach (T i in this)
                semilla = function(i, semilla);
            return semilla;
        }

        public IEnumerable<T> Invert()
        {
            T[] result = this.ToArray();
            for (int i = 0; i < result.Length / 2; i++)
            {
                T tmp = result[i];
                result[i] = result[result.Length - i - 1];
                result[result.Length - i - 1] = tmp;
            }
            return result;
        }

        /*
        public void ForEach(Action<T> accion)
        {
            foreach (T i in this)
                accion(i);
        }
        */

        public IEnumerable<T> ForEach(Action<T> accion)
        {
            foreach (T i in this) {
                accion(i);
                yield return i;
            }
        }

        public override string ToString()
        {
            if (NumberOfElements == 0)
                return "Lista vacía";
            string s = FirstNode.ToString();
            Node<T> aux = FirstNode.NextNode;
            while (aux != null)
            {
                s += " , " + aux.ToString();
                aux = aux.NextNode;
            }
            return s;
        }

        private void Invariante()
        {
            Debug.Assert((NumberOfElements == 0 & FirstNode == null)
                | (NumberOfElements > 0 & FirstNode != null),
                "La lista contiene errores");
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new IEnumeratorList<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class IEnumeratorList<T> : IEnumerator<T>
    {
        private int CurrentIndex;
        private T Element;
        private List<T> List;

        public IEnumeratorList(List<T> List)
        {
            this.List = List;
            CurrentIndex = -1;
        }
        
        // Obtiene el elemento de la lista en la posición del enumerator
        public T Current
        {
            get { return Element; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        // Libera recursos gestionador por el objeto
        public void Dispose() { Console.WriteLine("exit"); }

        // Avanza el enumerator al siguiente elemento de la lista
        public bool MoveNext()
        {
            if ((CurrentIndex + 1) < List.NumberOfElements)
            {
                CurrentIndex++;
                Element = List.GetElement((uint)CurrentIndex);
                return true;
            }
            return false;
        }

        // Devuelve el enumerator a su posicion inicial, que es anterior al primer elemento de la lista
        public void Reset()
        {
            CurrentIndex = -1;
        }
    }
}
