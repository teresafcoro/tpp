
namespace Lab09
{
    internal class Worker
    {
        // The vector whose modulus is going to be computed
        private BitcoinValueData[] data;

        private int fromIndex, toIndex;

        private int result;

        // The value of the comparation
        private int value;

        internal int Result
        {
            get { return result; }
        }

        internal Worker(BitcoinValueData[] data, int fromIndex, int toIndex, int value)
        {
            this.data = data;
            this.fromIndex = fromIndex;
            this.toIndex = toIndex;
            this.value = value;
        }

        /// <summary>
        /// Method that computes the count of the true condition
        /// </summary>
        internal void Compute()
        {
            result = 0;
            for (int i = fromIndex; i <= toIndex; i++)
            {
                if (data[i].Value >= value)
                {
                    result++;
                }
            }
        }
    }
}
