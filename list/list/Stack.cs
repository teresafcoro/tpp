using System;
using System.Diagnostics;

namespace list
{
    /// <summary>
    /// Clase Stack, con una lista simplemente enlazada polimórfica
    /// Se lleva a cabo programación por contratos validando precondiciones, postcondiciones e invariantes
    /// </summary>
    public class Stack<T>
    {
        private List<T> list;
        private uint maxNumberOfElements;
        public bool IsEmpty() { return list.NumberOfElements == 0; }
        public bool IsFull() { return list.NumberOfElements == maxNumberOfElements; }

        public Stack(uint maxNumberOfElements)
        {
            if (maxNumberOfElements == 0)
                throw new ArgumentException("No tiene sentido crear una pila con número máximo de elementos 0");
            this.maxNumberOfElements = maxNumberOfElements;
            list = new List<T>();
            Invariante();
        }        

        public void Push(T obj)
        {
            Invariante();
            if (obj == null)    // Precondición
                throw new ArgumentException("No se puede hacer push de un objeto nulo");
            if (IsFull())    // Precondición
                throw new InvalidOperationException("La pila está llena");
            uint n = list.NumberOfElements;
            list.AddTop(obj);  // añadir objeto en la cima
            Debug.Assert(!IsEmpty() | list.NumberOfElements == n + 1,
                    $"Operación erronea: Push({obj})");   // Postcondición
            Invariante();
        }

        public T Pop()
        {
            Invariante();
            if (IsEmpty()) // Precondición
                throw new InvalidOperationException("Pila vacía");
            uint n = list.NumberOfElements;
            T penultimo = list.GetElement(n - 2);
            T obj = list.RemoveTop(); // saco el primer elemento de la stack
            Debug.Assert(!IsFull() | list.NumberOfElements == n - 1
                    | list.GetElement(list.NumberOfElements - 1).Equals(penultimo),
                    "Operación erronea: Pop()");    // Postcondición
            Invariante();
            return obj;
        }

        public override string ToString()
        {
            return list.ToString();
        }

        private void Invariante()
        {
            Debug.Assert((IsEmpty() & !IsFull())
                | (!IsEmpty() & IsFull())
                | (!IsEmpty() & !IsFull())
                | (IsEmpty() & list.NumberOfElements == 0)
                | (IsFull() & list.NumberOfElements == maxNumberOfElements)
                | (list.NumberOfElements <= maxNumberOfElements),
                "La pila contiene errores");
        }
    }
}
