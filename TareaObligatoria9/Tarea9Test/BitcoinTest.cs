using Lab09;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tarea9Test
{
    [TestClass]
    public class BitcoinTest
    {
        private BitcoinValueData[] data = Utils.GetBitcoinData();

        [TestMethod]
        public void TestSingleThread()
        {
            Master master = new Master(data, 1, 7000);
            Assert.AreEqual(2854, master.ComputeModulus());
        }

        [TestMethod]
        public void TestFourThreads()
        {
            Master master = new Master(data, 4, 7000);
            Assert.AreEqual(2854, master.ComputeModulus());
        }

        [TestMethod]
        public void TestMaxNumberOfThreads()
        {
            Master master = new Master(data, data.Length, 7000);
            Assert.AreEqual(2854, master.ComputeModulus());
        }
    }
}
