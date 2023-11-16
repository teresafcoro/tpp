using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace test.vector
{
    [TestClass]
    public class TestVector
    {
        // * Instancia de la interfaz IList
        private IList<int> ListaI;

        [TestInitialize]
        public void InitializeTests()
        {
            ListaI = new List<int>();
        }

        [TestMethod]
        public void IntTest()
        {
            Assert.AreEqual(0, ListaI.Count);
            Assert.AreEqual(false, ListaI.Contains(1));
            ListaI.Add(1);
            Assert.AreEqual(true, ListaI.Contains(1));
            ListaI.Add(2); ListaI.Add(3); ListaI.Add(4); ListaI.Add(5);
            Assert.AreEqual(true, ListaI.Contains(4));
            Assert.AreEqual(false, ListaI.Contains(8));
            Assert.AreEqual(5, ListaI.Count);
            Assert.AreEqual(true, ListaI.Remove(1));
            Assert.AreEqual(false, ListaI.Remove(9));
            Assert.AreEqual(4, ListaI.Count);
            Assert.AreEqual(false, ListaI.Contains(1));
            ListaI.Insert(1, 9);
            Assert.AreEqual(true, ListaI.Contains(9));
            Assert.AreEqual(5, ListaI.Count);
            Assert.AreEqual(true, ListaI.Contains(3));
            Assert.AreEqual(2, ListaI.IndexOf(3));
            ListaI.RemoveAt(4);
            Assert.AreEqual(false, ListaI.Contains(5));
            Assert.AreEqual(4, ListaI.Count);
            int suma = 0;
            foreach (int i in ListaI)
            {
                suma += i;
            }
            Assert.AreEqual(18, suma);
            ListaI.Add(9);
            Assert.AreEqual(1, ListaI.IndexOf(9));
        }
    }
}
