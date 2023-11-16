using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace prac05.test
{
    [TestClass]
    public class DelegatesTest
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
            Person p = new Person("Juan", "Pérez", "103478387F");
            Assert.AreEqual(p.ToString(), Delegates.Find(personas, x => "Juan".Equals(x.FirstName)).ToString());
            Assert.AreEqual(p.ToString(), Delegates.Find(personas, x => x.IDNumber.EndsWith("F")).ToString());            
            Person[] pFilter = { p, new Person("Juan", "Hevia", "9376384K") };
            Assert.AreEqual(pFilter.ToString(), Delegates.Filter(personas, x => "Juan".Equals(x.FirstName)).ToString());
            Person[] pFilter1 = { p, new Person("Cristina", "Sánchez", "56837645F") };
            Assert.AreEqual(pFilter1.ToString(), Delegates.Filter(personas, x => x.IDNumber.EndsWith("F")).ToString());
        }

        [TestMethod]
        public void AngleTest()
        {
            Assert.AreEqual(new Angle(90).ToString(), Delegates.Find(angulos, a => a.Degrees == 90).ToString());
            Assert.AreEqual(new Angle(0).ToString(), Delegates.Find(angulos, a => a.Quadrant == 1).ToString());
            Angle[] ang = { new Angle(90) };
            Assert.AreEqual(ang.ToString(), Delegates.Filter(angulos, a => a.Degrees == 90).ToString());
            Assert.AreEqual(ang.ToString(), Delegates.Filter(angulos, a => a.Quadrant == 1).ToString());
            Assert.AreEqual(64980, Delegates.Reduce<Angle, double>(angulos, (suma, a) => suma += a.Degrees));
            Assert.AreEqual(1, Delegates.Reduce(angulos, (double comp, Angle a) => comp > a.Sine() ? comp : a.Sine()));
        }
    }
}
