using System;
using Network.Helpers;
using Network.Instances;

namespace Network.Base.Connections
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
