using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using domainObjects;
using list;

namespace test.stack
{
    /// <summary>
    /// Test de la clase Stack
    /// </summary>
    [TestClass]
    public class TestStack
    {
        // * Instancia de la clase stack
        private list.Stack<string> stackString;
        private list.Stack<int> stackInt;
        private list.Stack<double> stackDouble;
        private list.Stack<Person> stackPerson;

        [TestInitialize]
        public void InitializeTests()
        {
            stackString = new list.Stack<string>(4);
            stackInt = new list.Stack<int>(4);
            stackDouble = new list.Stack<double>(4);
            stackPerson = new list.Stack<Person>(4);
        }

        [TestMethod]
        public void ConstructorStackTest()
        {
            Assert.IsTrue(stackInt.IsEmpty());
            Assert.IsFalse(stackInt.IsFull());

            Assert.IsTrue(stackString.IsEmpty());
            Assert.IsFalse(stackString.IsFull());

            Assert.IsTrue(stackDouble.IsEmpty());
            Assert.IsFalse(stackDouble.IsFull());

            Assert.IsTrue(stackPerson.IsEmpty());
            Assert.IsFalse(stackPerson.IsFull());
        }

        [TestMethod]
        public void StringTest()
        {
            stackString.Push("hola");
            Assert.IsFalse(stackString.IsEmpty());
            Assert.IsFalse(stackString.IsFull());
            stackString.Push("que"); stackString.Push("tal"); stackString.Push("?");
            Assert.IsTrue(stackString.IsFull());
            Assert.IsFalse(stackString.IsEmpty());

            Assert.AreEqual("?", stackString.Pop().ToString());
            Assert.IsFalse(stackString.IsEmpty());
            Assert.IsFalse(stackString.IsFull());
            Assert.AreEqual("tal", stackString.Pop());
            Assert.IsFalse(stackString.IsEmpty());
            Assert.IsFalse(stackString.IsFull());
        }

        [TestMethod]
        public void PersonTest()
        {
            Person p1 = new Person("Maria", "Garcia");
            Person p2 = new Person("Lucia", "Prieto");
            Person p3 = new Person("Pedro", "Suarez");
            Person p4 = new Person("Manuel", "Garcia");

            stackPerson.Push(p1);
            Assert.IsFalse(stackPerson.IsEmpty());
            Assert.IsFalse(stackPerson.IsFull());
            stackPerson.Push(p2); stackPerson.Push(p3); stackPerson.Push(p4);
            Assert.IsTrue(stackPerson.IsFull());
            Assert.IsFalse(stackPerson.IsEmpty());

            Assert.AreEqual(p4, stackPerson.Pop());
            Assert.IsFalse(stackPerson.IsEmpty());
            Assert.IsFalse(stackPerson.IsFull());
            Assert.AreEqual(p3, stackPerson.Pop());
            Assert.IsFalse(stackPerson.IsEmpty());
            Assert.IsFalse(stackPerson.IsFull());
        }

        [TestMethod]
        public void IntTest()
        {
            stackInt.Push(3);
            Assert.IsFalse(stackInt.IsEmpty());
            Assert.IsFalse(stackInt.IsFull());
            stackInt.Push(2); stackInt.Push(5); stackInt.Push(4);
            Assert.IsTrue(stackInt.IsFull());
            Assert.IsFalse(stackInt.IsEmpty());

            Assert.AreEqual(4, stackInt.Pop());
            Assert.IsFalse(stackInt.IsEmpty());
            Assert.IsFalse(stackInt.IsFull());
            Assert.AreEqual(5, stackInt.Pop());
            Assert.IsFalse(stackInt.IsEmpty());
            Assert.IsFalse(stackInt.IsFull());
        }

        [TestMethod]
        public void DoubleTest()
        {
            stackDouble.Push(3.2);
            Assert.IsFalse(stackDouble.IsEmpty());
            Assert.IsFalse(stackDouble.IsFull());
            stackDouble.Push(7.8); stackDouble.Push(5.9); stackDouble.Push(1.4);
            Assert.IsTrue(stackDouble.IsFull());
            Assert.IsFalse(stackDouble.IsEmpty());

            Assert.AreEqual(1.4, stackDouble.Pop());
            Assert.IsFalse(stackDouble.IsEmpty());
            Assert.IsFalse(stackDouble.IsFull());
            Assert.AreEqual(5.9, stackDouble.Pop());
            Assert.IsFalse(stackDouble.IsEmpty());
            Assert.IsFalse(stackDouble.IsFull());
        }
    }
}
