using System.Collections.Generic;
using ObcidiaNetwork.Helpers;

namespace ObcidiaNetwork.Base
{
    internal class Connection
    {
        public Neuron InputNeuron { get; set; }

        public Neuron OutputNeuron { get; set; }

        public double Weight { get; set; }

        public double WeightDelta { get; set; }

        public Connection (Neuron inputNeuron, Neuron outputNeuron)
        {
            InputNeuron = inputNeuron;
            OutputNeuron = outputNeuron;
            Weight = FixedRandom.RandomDouble();
        }

        public override string ToString()
        {
            return $"{nameof(InputNeuron)}: {InputNeuron}, {nameof(OutputNeuron)}: {OutputNeuron}, {nameof(Weight)}: {Weight}, {nameof(WeightDelta)}: {WeightDelta}";
        }

        private sealed class ConnectionEqualityComparer : IEqualityComparer<Connection>
        {
            public bool Equals(Connection x, Connection y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return Equals(x.InputNeuron, y.InputNeuron) && Equals(x.OutputNeuron, y.OutputNeuron) && x.Weight.Equals(y.Weight) && x.WeightDelta.Equals(y.WeightDelta);
            }

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

        public static IEqualityComparer<Connection> ConnectionComparer { get; } = new ConnectionEqualityComparer();

        protected bool Equals(Connection other)
        {
            return Equals(InputNeuron, other.InputNeuron) && Equals(OutputNeuron, other.OutputNeuron) && Weight.Equals(other.Weight) && WeightDelta.Equals(other.WeightDelta);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Connection) obj);
        }

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

        public static bool operator ==(Connection left, Connection right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Connection left, Connection right)
        {
            return !Equals(left, right);
        }
    }
}