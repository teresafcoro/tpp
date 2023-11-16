using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace test.diccionario
{
    [TestClass]
    public class DiccionarioTest
    {
        // * Instancia de la interfaz IDictionary
        private IDictionary<string, string> DicS;

        [TestInitialize]
        public void InitializeTests()
        {
            DicS = new Dictionary<string, string>();
        }

        [TestMethod]
        public void StringTest()
        {
            Assert.AreEqual(0, DicS.Count);
            Assert.AreEqual(false, DicS.ContainsKey("Alumno1"));
            DicS.Add("Alumno1", "María");
            Assert.AreEqual(true, DicS.ContainsKey("Alumno1"));
            DicS.Add("Alumno2", "Juan"); DicS.Add("Alumno3", "Ana");
            DicS.Add("Alumno4", "Carlos"); DicS.Add("Alumno5", "Fer");
            Assert.AreEqual(true, DicS.ContainsKey("Alumno3"));
            Assert.AreEqual(false, DicS.ContainsKey("Alumno20"));
            Assert.AreEqual(5, DicS.Count);
            Assert.AreEqual(true, DicS.Remove("Alumno3"));
            Assert.AreEqual(false, DicS.Remove("Alumno30"));
            Assert.AreEqual(4, DicS.Count);
            Assert.AreEqual(false, DicS.ContainsKey("Alumno3"));
            string dicValues = "";
            foreach (KeyValuePair<string, string> s in DicS)
            {
                dicValues += " " + s.Value;
            }
            Assert.AreEqual(" María Juan Carlos Fer", dicValues);
        }
    }
}
