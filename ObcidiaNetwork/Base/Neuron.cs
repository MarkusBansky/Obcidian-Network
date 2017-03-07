using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObcidiaNetwork.Helpers;

namespace ObcidiaNetwork.Base
{
    internal class Neuron
    {
        public List<Connection> InputConnections { get; set; }

        public List<Connection> OutputConnections { get; set; }

        public double Bias { get; set; }

        public double BiasDelta { get; set; }

        public double Gradient { get; set; }

        public double Value { get; set; }

        public Neuron ()
        {
            InputConnections = new List<Connection> ();
            OutputConnections = new List<Connection> ();
            Bias = FixedRandom.RandomDouble();
        }

        public Neuron (IEnumerable<Neuron> inputNeurons) : this()
		{
            foreach (var inputNeuron in inputNeurons)
            {
                var connection = new Connection (inputNeuron, this);
                inputNeuron.OutputConnections.Add (connection);
                InputConnections.Add (connection);
            }
        }

        public virtual double CalculateValue ()
        {
            return Value = Sigmoid.Output (InputConnections.Sum (a => a.Weight * a.InputNeuron.Value) + Bias);
        }

        public double CalculateError (double target)
        {
            return target - Value;
        }

        public double CalculateGradient (double? target = null)
        {
            if (target == null)
                return Gradient = OutputConnections.Sum (a => a.OutputNeuron.Gradient * a.Weight) * Sigmoid.Derivative (Value);

            return Gradient = CalculateError (target.Value) * Sigmoid.Derivative (Value);
        }

        public void UpdateWeights (double learnRate, double momentum)
        {
            var prevDelta = BiasDelta;
            BiasDelta = learnRate * Gradient;
            Bias += BiasDelta + momentum * prevDelta;

            Parallel.ForEach(InputConnections, (synapse) => {
                prevDelta = synapse.WeightDelta;
                synapse.WeightDelta = learnRate * Gradient * synapse.InputNeuron.Value;
                synapse.Weight += synapse.WeightDelta + momentum * prevDelta;
            });
        }

        public override string ToString()
        {
            return $"{nameof(InputConnections)}: {InputConnections}, {nameof(OutputConnections)}: {OutputConnections}, {nameof(Bias)}: {Bias}, {nameof(BiasDelta)}: {BiasDelta}, {nameof(Gradient)}: {Gradient}, {nameof(Value)}: {Value}";
        }

        private sealed class NeuronEqualityComparer : IEqualityComparer<Neuron>
        {
            public bool Equals(Neuron x, Neuron y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return Equals(x.InputConnections, y.InputConnections) && Equals(x.OutputConnections, y.OutputConnections) && x.Bias.Equals(y.Bias) && x.BiasDelta.Equals(y.BiasDelta) && x.Gradient.Equals(y.Gradient) && x.Value.Equals(y.Value);
            }

            public int GetHashCode(Neuron obj)
            {
                unchecked
                {
                    var hashCode = obj.InputConnections?.GetHashCode() ?? 0;
                    hashCode = (hashCode * 397) ^ (obj.OutputConnections?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ obj.Bias.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.BiasDelta.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.Gradient.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.Value.GetHashCode();
                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<Neuron> NeuronComparer { get; } = new NeuronEqualityComparer();

        protected bool Equals(Neuron other)
        {
            return Bias.Equals(other.Bias) && BiasDelta.Equals(other.BiasDelta) && Gradient.Equals(other.Gradient) && Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Neuron) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Bias.GetHashCode();
                hashCode = (hashCode * 397) ^ BiasDelta.GetHashCode();
                hashCode = (hashCode * 397) ^ Gradient.GetHashCode();
                hashCode = (hashCode * 397) ^ Value.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Neuron left, Neuron right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Neuron left, Neuron right)
        {
            return !Equals(left, right);
        }
    }
}