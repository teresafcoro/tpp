using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using domainObjects;
using System.Collections.Generic;

namespace TestMExtensoresLinq
{
    [TestClass]
    public class TestMExtensoresLinq
    {
        private Person[] personas;
        private Angle[] angulos;

        [TestInitialize]
        public void InitializeTests()
        {
            personas = Factory.CreatePeople();
            angulos = Factory.CreateAngles();
        }

        [TestMethod]
        public void PersonTest()
        {
            // Find ~ FirstOrDefault
            // Búsqueda de personas por nombre
            Person p = new Person("Juan", "Pérez", "103478387F");
            Assert.AreEqual(p.ToString(), personas.FirstOrDefault(p => "Juan".Equals(p.FirstName)).ToString());
            // Búsqueda de personas cuyo nif termina en una letra dada
            Assert.AreEqual(p.ToString(), personas.FirstOrDefault(p => p.IDNumber.EndsWith("F")).ToString());

            // Filter ~ Where
            // Búsqueda de personas por nombre
            Person[] pFilter = { p, new Person("Juan", "Hevia", "9376384K") };
            Assert.AreEqual(pFilter.ToString(), personas.Where(x => "Juan".Equals(x.FirstName)).ToArray().ToString());
            // Búsqueda de personas cuyo nif termina en una letra dada
            Person[] pFilter1 = { p, new Person("Cristina", "Sánchez", "56837645F") };
            Assert.AreEqual(pFilter1.ToString(), personas.Where(x => p.IDNumber.EndsWith("F")).ToArray().ToString());

            // Reduce ~ Aggregate
            // Conocer la distribución de personas por nombre (esto es, decir que hay 10 personas con nombre “María”, 3 con nombre “Pedro”...)
            Dictionary<string, int> distribucion = new Dictionary<string, int>()
            {
                { "María", 2 }, { "Juan", 2 }, { "Pepe", 1 }, { "Luis", 1 }, { "Carlos", 1 }, { "Miguel", 1 }, { "Cristina", 1 }
            };
            Assert.AreEqual(distribucion.ToString(), personas.Aggregate
                (new Dictionary<string, int>(), (dic, p) => UpdateKey(dic, p)).ToString());

            // Map ~ Select
            // Obtener los “apellidos, nombre” (como un único String) de cada una de las personas de la lista
            string[] pList = { "Díaz, María", "Pérez, Juan", "Hevia, Pepe", "García, Luis", "Rodríguez, Carlos",
                                  "Pérez, Miguel", "Sánchez, Cristina", "Díaz, María","Hevia, Juan"};
            Assert.AreEqual(pList.ToString(), personas.Select(x => x.Surname + ", " + x.FirstName).ToArray().ToString());
        }

        private Dictionary<string, int> UpdateKey(Dictionary<string, int> dic, Person p)
        {
            if (dic.ContainsKey(p.FirstName))
                dic[p.FirstName]++;
            else
                dic[p.FirstName] = 1;
            return dic;
        }

        [TestMethod]
        public void AngleTest()
        {
            // Find ~ FirstOrDefault
            // Búsqueda de ángulos rectos
            Assert.AreEqual(new Angle(90).ToString(), angulos.FirstOrDefault(a => a.Degrees == 90).ToString());
            // Búsqueda de ángulos en un cuadrante
            Assert.AreEqual(new Angle(0).ToString(), angulos.FirstOrDefault(a => a.Quadrant == 1).ToString());

            // Filter ~ Where
            // Búsqueda de ángulos rectos
            Angle[] ang = { new Angle(90) };
            Assert.AreEqual(ang.ToString(), angulos.Where(a => a.Degrees == 90).ToArray().ToString());
            // Búsqueda de ángulos en un cuadrante
            Assert.AreEqual(new Angle(0).ToString(), angulos.Where(a => a.Quadrant == 1).First().ToString());

            // Reduce ~ Aggregate
            // Calcular la suma de todos los grados de los ángulos de la colección
            Assert.AreEqual(64980, angulos.Aggregate(0.0, (suma, a) => suma += a.Degrees));
            // Calcular el seno máximo
            Assert.AreEqual(1, angulos.Aggregate(0.0, (comp, a) => comp > a.Sine() ? comp : a.Sine()));

            // Map ~ Select
            // Obtener la lista de los cuadrantes de los ángulos
            int[] cuadrantes = { 1, 2, 3, 4 };
            Assert.AreEqual(cuadrantes.ToString(), angulos.Select(a => a.Quadrant).ToArray().ToString());
        }
    }
}
