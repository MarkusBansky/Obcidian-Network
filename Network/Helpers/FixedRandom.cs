using System;

namespace Network.Helpers
{
    /// <summary>
    /// Fixed Random class
    /// </summary>
    public class FixedRandom
    {
        //Function to get random number
        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();

        /// <summary>
        /// Returns a fixed random integer random between min and max values.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int RandomNumber(int min, int max)
        {
            lock (SyncLock)
            {
                return Random.Next(min, max);
            }
        }

        /// <summary>
        /// Returns a fixed double random.
        /// </summary>
        /// <returns></returns>
        public static double RandomDouble()
        {
            lock (SyncLock)
            {
                return Random.NextDouble();
            }
        }
    }
}
