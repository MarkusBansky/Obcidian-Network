using System;

namespace ObcidiaNetwork.Helpers
{
    /// <summary>
    /// Fixed Random class
    /// </summary>
    public class FixedRandom
    {
        //Function to get random number
        private static readonly Random Random = new Random ();
        private static readonly object SyncLock = new object ();

        /// <summary>
        /// Returns a fixed random integer between min and max values.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int RandomNumber (int min, int max)
        {
            lock (SyncLock)
            {
                return Random.Next (min, max);
            }
        }

        /// <summary>
        /// Returns a fixed random double ranged from -1 to 1.
        /// </summary>
        /// <returns></returns>
        public static double RandomDouble ()
        {
            lock (SyncLock)
            {
                return 2.0 * Random.NextDouble () - 1;
            }
        }
    }
}
