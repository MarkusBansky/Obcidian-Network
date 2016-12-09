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
using Network.Helpers;
using Network.Items;

namespace Network.Base
{
    /// <summary>
    /// Basic connection class.
    /// </summary>
    public class ConnectionBase : IEquatable<NeuralConnection>
    {
        /// <summary>
        /// Previous neuron index.
        /// </summary>
        public readonly int PreviousNeuron;

        /// <summary>
        /// Next neuron index.
        /// </summary>
        public readonly int NextNeuron;

        /// <summary>
        /// Connection weight multiplier.
        /// </summary>
        public double Multiplier;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="next"></param>
        public ConnectionBase(int previous, int next)
        {
            PreviousNeuron = previous;
            NextNeuron = next;

            Multiplier = FixedRandom.RandomDouble();
        }

        /// <summary>
        /// Implemented IEquatable interface for IEnumerable collections.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NeuralConnection other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return PreviousNeuron == other.PreviousNeuron && NextNeuron == other.NextNeuron && Multiplier.Equals(other.Multiplier);
        }

        /// <summary>
        /// Overloaded equals method.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((NeuralConnection)obj);
        }

        /// <summary>
        /// Overloaded hash code method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = PreviousNeuron;
                hashCode = (hashCode * 397) ^ NextNeuron;
                hashCode = (hashCode * 397) ^ Multiplier.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Overloaded == operator.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(ConnectionBase left, ConnectionBase right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Overloaded != operator.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(ConnectionBase left, ConnectionBase right)
        {
            return !Equals(left, right);
        }
    }
}
