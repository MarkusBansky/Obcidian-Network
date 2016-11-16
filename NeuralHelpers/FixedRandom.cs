using System;

namespace NeuralHelpers
{
    public class FixedRandom
    {
        //Function to get random number
        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();

        public static int RandomNumber(int min, int max)
        {
            lock (SyncLock)
            {
                return Random.Next(min, max);
            }
        }

        public static double RandomDouble()
        {
            lock (SyncLock)
            {
                return Random.NextDouble();
            }
        }
    }
}
