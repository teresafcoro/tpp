using list;

namespace master
{
    internal class Worker<T>
    {
        private int fromIndex, toIndex;
        private ConcurrentQueue<T> queue;

        internal long Result { get; set; }

        internal Worker(ConcurrentQueue<T> queue, int fromIndex, int toIndex)
        {
            this.queue = queue;
            this.fromIndex = fromIndex;
            this.toIndex = toIndex;
        }

        internal void Compute()
        {
            Result = 0;
            for (int i=this.fromIndex; i<=this.toIndex; i++)
                Result += i;
        }
    }
}
