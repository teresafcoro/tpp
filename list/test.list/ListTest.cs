using Microsoft.VisualStudio.TestTools.UnitTesting;
using list;
using domainObjects;
using System.Linq;

namespace test.list
{
    /// <summary>
    /// Test de la clase List
    /// </summary>
    [TestClass]
    public class ListTest
    {       
        // * Instancias de la clase List
        private List<string> listString;
        private List<int> listInt;
        private List<double> listDouble;
        private List<Person> listPerson;
        private List<Person> personas;
        private List<Angle> angulos;

        [TestInitialize]
        public void InitializeTests()
        {
            listString = new List<string>();
            listInt = new List<int>();
            listDouble = new List<double>();
            listPerson = new List<Person>();
            personas = Factory.CreatePeopleList();
            angulos = Factory.CreateAnglesList();
        }

        [TestMethod]
        public void StringTest()
        {
            listString.Add("hola");
            Assert.AreEqual("hola", listString.GetElement(0));
            Assert.IsTrue(listString.Contains("hola"));
            listString.Add("que"); listString.Add("tal"); listString.Add("estas"); listString.Add("?");
            Assert.AreEqual("que", listString.GetElement(1));
            Assert.AreEqual("tal", listString.GetElement(2));
            Assert.AreEqual("estas", listString.GetElement(3));
            Assert.IsTrue(listString.Contains("estas"));
            Assert.AreEqual("?", listString.GetElement(4));

            listString.Remove("hola");
            Assert.AreEqual("que", listString.GetElement(0));
            Assert.IsFalse(listString.Contains("hola"));
            listString.Remove("tal");
            Assert.AreEqual("estas", listString.GetElement(1));
            Assert.IsFalse(listString.Contains("tal"));
            Assert.IsTrue(listString.Contains("estas"));

            // Prueba método Invert
            List<string> invertList = new List<string>
            {
                "estas",
                "que"
            };
            Assert.AreEqual(invertList.ToArray().ToString(), listString.Invert().ToArray().ToString());
        }

        [TestMethod]
        public void PersonTest()
        {
            Person p1 = new Person("Maria", "Garcia");
            listPerson.Add(p1);
            Assert.AreEqual(p1, listPerson.GetElement(0));
            Assert.IsTrue(listPerson.Contains(p1));
            Person p2 = new Person("Lucia", "Prieto");
            listPerson.Add(p2);
            Person p3 = new Person("Pedro", "Suarez");
            listPerson.Add(p3);
            Person p4 = new Person("Manuel", "Garcia");
            listPerson.Add(p4);
            Assert.AreEqual(p4, listPerson.GetElement(3));
            Assert.IsTrue(listPerson.Contains(p3));

            listPerson.Remove(p1);
            Assert.AreEqual(p2, listPerson.GetElement(0));
            listPerson.Remove(p3);
            Assert.AreEqual(p4, listPerson.GetElement(1));
            Assert.IsTrue(listPerson.Contains(p4));
            Assert.IsFalse(listPerson.Contains(p3));
        }

        [TestMethod]
        public void IntTest()
        {
            listInt.Add(3);
            Assert.IsTrue(listInt.Contains(3));
            listInt.Add(2); listInt.Add(5); listInt.Add(4); listInt.Add(1);
            Assert.AreEqual(2, listInt.GetElement(1));
            Assert.AreEqual(4, listInt.GetElement(3));
            Assert.AreEqual(1, listInt.GetElement(4));
            Assert.IsTrue(listInt.Contains(5));

            listInt.Remove(3);
            Assert.AreEqual(2, listInt.GetElement(0));
            Assert.IsTrue(listInt.Contains(2));
            Assert.IsFalse(listInt.Contains(3));
            listInt.Remove(4);
            Assert.AreEqual(1, listInt.GetElement(2));
            Assert.IsTrue(listInt.Contains(5));
        }

        [TestMethod]
        public void DoubleTest()
        {
            listDouble.Add(3.1);
            Assert.AreEqual(3.1, listDouble.GetElement(0));
            Assert.IsTrue(listDouble.Contains(3.1));
            Assert.IsFalse(listDouble.Contains(5.9));
            listDouble.Add(2.8); listDouble.Add(5.9); listDouble.Add(4.3); listDouble.Add(1000);
            Assert.AreEqual(5.9, listDouble.GetElement(2));
            Assert.IsTrue(listDouble.Contains(1000));

            listDouble.Remove(3.1);
            Assert.AreEqual(2.8, listDouble.GetElement(0));
            listDouble.Remove(4.3);
            Assert.AreEqual(1000, listDouble.GetElement(2));
            Assert.IsTrue(listDouble.Contains(1000));
            Assert.IsFalse(listDouble.Contains(3.1));
        }

        [TestMethod]
        public void PersonasTest()
        {
            // Find ~ FirstOrDefault
            // Búsqueda de personas por nombre
            Person p = new Person("Juan", "Pérez", "103478387F");
            Assert.AreEqual(p.ToString(), personas.Find(p => "Juan".Equals(p.FirstName)).ToString());
            // Búsqueda de personas cuyo nif termina en una letra dada
            Assert.AreEqual(p.ToString(), personas.Find(p => p.IDNumber.EndsWith("F")).ToString());

            // Filter ~ Where
            // Búsqueda de personas por nombre
            Assert.AreEqual(new Person("Juan", "Hevia", "9376384K").ToString(), personas.Filter(p => "Juan".Equals(p.FirstName)).Last().ToString());
            // Búsqueda de personas cuyo nif termina en una letra dada
            Assert.AreEqual(new Person("Cristina", "Sánchez", "56837645F").ToString(), personas.Filter(p => p.IDNumber.EndsWith("F")).Last().ToString());

            // Reduce ~ Aggregate
            // Conocer la distribución de personas por nombre (esto es, decir que hay 10 personas con nombre “María”, 3 con nombre “Pedro”...)
            System.Collections.Generic.Dictionary<string, int> distribucion = new System.Collections.Generic.Dictionary<string, int>()
            {
                { "María", 2 }, { "Juan", 2 }, { "Pepe", 1 }, { "Luis", 1 }, { "Carlos", 1 }, { "Miguel", 1 }, { "Cristina", 1 }
            };
            Assert.AreEqual(distribucion.ToString(),
                personas.Reduce<System.Collections.Generic.Dictionary<string, int>>((dic, p) => UpdateKey(p, dic)).ToString());

            // Map ~ Select
            // Obtener los “apellidos, nombre” (como un único String) de cada una de las personas de la lista
            string[] pList = { "Díaz, María", "Pérez, Juan", "Hevia, Pepe", "García, Luis", "Rodríguez, Carlos",
                                  "Pérez, Miguel", "Sánchez, Cristina", "Díaz, María","Hevia, Juan"};
            Assert.AreEqual(pList.ToString(), personas.Map(x => x.Surname + ", " + x.FirstName).ToArray().ToString());
        }

        private System.Collections.Generic.Dictionary<string, int> UpdateKey(System.Collections.Generic.Dictionary<string, int> dic, Person p)
        {
            if (dic == null)
                dic = new System.Collections.Generic.Dictionary<string, int>();
            else if (dic.ContainsKey(p.FirstName))
                dic[p.FirstName]++;
            else
                dic[p.FirstName] = 1;
            return dic;
        }

        [TestMethod]
        public void AngulosTest()
        {
            // Find ~ FirstOrDefault
            // Búsqueda de ángulos rectos
            Assert.AreEqual(new Angle(90).ToString(), angulos.Find(a => a.Degrees == 90).ToString());
            // Búsqueda de ángulos en un cuadrante
            Assert.AreEqual(new Angle(0).ToString(), angulos.Find(a => a.Quadrant == 1).ToString());

            // Filter ~ Where
            // Búsqueda de ángulos rectos
            Assert.AreEqual(new Angle(90).ToString(), angulos.Filter(a => a.Degrees == 90).First().ToString());
            // Búsqueda de ángulos en un cuadrante
            Assert.AreEqual(new Angle(0).ToString(), angulos.Filter(a => a.Quadrant == 1).First().ToString());

            // Reduce ~ Aggregate
            // Calcular la suma de todos los grados de los ángulos de la colección
            Assert.AreEqual(64980, angulos.Reduce<double>((a, suma) => suma += a.Degrees));
            // Calcular el seno máximo
            Assert.AreEqual(1, angulos.Reduce<double>((a, comp) => comp > a.Sine() ? comp : a.Sine()));

            // Map ~ Select
            // Obtener la lista de los cuadrantes de los ángulos
            int[] cuadrantes = { 1, 2, 3, 4 };
            Assert.AreEqual(cuadrantes.ToString(), angulos.Map(a => a.Quadrant).ToArray().ToString());
        }
    }
}
