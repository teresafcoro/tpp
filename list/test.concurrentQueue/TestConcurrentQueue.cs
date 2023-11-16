using Microsoft.VisualStudio.TestTools.UnitTesting;
using list;
using master;

namespace test.concurrentQueue
{
    [TestClass]
    public class TestConcurrentQueue
    {
        ConcurrentQueue<int> queue;

        [TestInitialize]
        public void InitializeTests()
        {
            queue = new ConcurrentQueue<int>();
        }

        [TestMethod]
        public void ConstructorListTest()
        {
            Assert.AreEqual(true, queue.IsEmpty());
        }

        [TestMethod]
        public void IntTest()
        {
            queue.Enqueue(1);
            Assert.AreEqual(false, queue.IsEmpty());
            queue.Enqueue(2); queue.Enqueue(3); queue.Enqueue(4); queue.Enqueue(5);
            queue.Enqueue(6); queue.Enqueue(7); queue.Enqueue(8); queue.Enqueue(9);
            queue.Enqueue(10); queue.Enqueue(11); queue.Enqueue(12); queue.Enqueue(13);
            queue.Enqueue(14); queue.Enqueue(15); queue.Enqueue(16); queue.Enqueue(17);
            Assert.AreEqual(false, queue.IsEmpty());
            Assert.AreEqual(1, queue.Peek());
            queue.Dequeue();
            Assert.AreEqual(false, queue.IsEmpty());
            Assert.AreEqual(2, queue.Peek());

            //TestSingleThread
            Master<int> master = new Master<int>(queue, 1);
            Assert.AreEqual(120, master.ComputeModulus());

            //TestFourThreads
            master = new Master<int>(queue, 4);
            Assert.AreEqual(120, master.ComputeModulus());

            //TestMaxNumberOfThreads�
            master = new Master<int>(queue, queue.NumberOfElements());
            Assert.AreEqual(120, master.ComputeModulus());
        }
    }
}
