using System.Collections.Generic;
using ObcidiaNetwork.Helpers;

namespace ObcidiaNetwork.Base
{
    /// <summary>
    /// A neural synapse
    /// </summary>
    internal class Connection
    {
        /// <summary>
        /// Input neuron reference.
        /// </summary>
        public Neuron InputNeuron { get; set; }

        /// <summary>
        /// Output neuron reference.
        /// </summary>
        public Neuron OutputNeuron { get; set; }

        /// <summary>
        /// Weight of synapse.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Weight delta of synapse.
        /// </summary>
        public double WeightDelta { get; set; }

        /// <summary>
        /// Creates new neuron connection.
        /// </summary>
        /// <param name="inputNeuron">From neuron.</param>
        /// <param name="outputNeuron">To neuron.</param>
        public Connection (Neuron inputNeuron, Neuron outputNeuron)
        {
            InputNeuron = inputNeuron;
            OutputNeuron = outputNeuron;
            Weight = FixedRandom.RandomDouble();
        }

        /// <summary>
        /// Overrriden ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return 
                $" {nameof(InputNeuron)}: {InputNeuron}," +
                $" {nameof(OutputNeuron)}: {OutputNeuron}," +
                $" {nameof(Weight)}: {Weight}," +
                $" {nameof(WeightDelta)}: {WeightDelta}";
        }

        /// <summary>
        /// Equality comparer class.
        /// </summary>
        private sealed class ConnectionEqualityComparer : IEqualityComparer<Connection>
        {
            /// <summary>
            /// Overriden Equals method.
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public bool Equals(Connection x, Connection y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return Equals(x.InputNeuron, y.InputNeuron) && Equals(x.OutputNeuron, y.OutputNeuron) && x.Weight.Equals(y.Weight) && x.WeightDelta.Equals(y.WeightDelta);
            }

            /// <summary>
            /// Overriden hash code method.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int GetHashCode(Connection obj)
            {
                unchecked
                {
                    var hashCode = (obj.InputNeuron != null ? obj.InputNeuron.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.OutputNeuron != null ? obj.OutputNeuron.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ obj.Weight.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.WeightDelta.GetHashCode();
                    return hashCode;
                }
            }
        }

        /// <summary>
        /// Equality comparer instance.
        /// </summary>
        public static IEqualityComparer<Connection> ConnectionComparer { get; } = new ConnectionEqualityComparer();

        /// <summary>
        /// New equality method for connections.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(Connection other)
        {
            return Equals(InputNeuron, other.InputNeuron) && Equals(OutputNeuron, other.OutputNeuron) && Weight.Equals(other.Weight) && WeightDelta.Equals(other.WeightDelta);
        }

        /// <summary>
        /// Overriden equality method.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Connection) obj);
        }

        /// <summary>
        /// Overriden get hash code method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = InputNeuron?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (OutputNeuron?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ Weight.GetHashCode();
                hashCode = (hashCode * 397) ^ WeightDelta.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Connection left, Connection right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Non-equality operator
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Connection left, Connection right)
        {
            return !Equals(left, right);
        }
    }
}