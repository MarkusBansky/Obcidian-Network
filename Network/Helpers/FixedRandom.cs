#region Licence
//  /*************************************************************************
//  * 
//  * AFGOR CONFIDENTIAL
//  * __________________
//  * 
//  *  [2015] - [2016] Markus Benovsky, Afgor Entertainment
//  *  All Rights Reserved.
//  * 
//  * NOTICE:  All information contained herein is, and remains
//  * the property of Afgor Entertainment and its suppliers,
//  * if any.  The intellectual and technical concepts contained
//  * herein are proprietary to Afgor Entertainment
//  * and its suppliers and may be covered by UA and Foreign Patents,
//  * patents in process, and are protected by trade secret or copyright law.
//  * Dissemination of this information or reproduction of this material
//  * is strictly forbidden unless prior written permission is obtained
//  * from Afgor Entertainment.
//  * 
//  * Code written by Markus Benovsky for ObsidiaNetwork project in NauralNetworks
//  * 2016 11 25
//  */
#endregion
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
