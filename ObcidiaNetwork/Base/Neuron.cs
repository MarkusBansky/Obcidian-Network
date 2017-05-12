using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObcidiaNetwork.Helpers;

namespace ObcidiaNetwork.Base
{
    /// <summary>
    /// Main neuron class.
    /// </summary>
    internal class Neuron
    {
        /// <summary>
        /// Input connections.
        /// </summary>
        public List<Connection> InputConnections { get; set; }

        /// <summary>
        /// Output connections.
        /// </summary>
        public List<Connection> OutputConnections { get; set; }

        /// <summary>
        /// Bias value.
        /// </summary>
        public double Bias { get; set; }

        /// <summary>
        /// Bias delta value.
        /// </summary>
        public double BiasDelta { get; set; }

        /// <summary>
        /// Gradient value.
        /// </summary>
        public double Gradient { get; set; }

        /// <summary>
        /// Neuron main output value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Default contructor.
        /// </summary>
        public Neuron ()
        {
            InputConnections = new List<Connection> ();
            OutputConnections = new List<Connection> ();
            Bias = FixedRandom.RandomDouble();
        }

        /// <summary>
        /// Main contructor.
        /// </summary>
        /// <param name="inputNeurons">Input neurons to create connections.</param>
        public Neuron (IEnumerable<Neuron> inputNeurons) : this()
		{
            foreach (var inputNeuron in inputNeurons)
            {
                var connection = new Connection (inputNeuron, this);
                inputNeuron.OutputConnections.Add (connection);
                InputConnections.Add (connection);
            }
        }

        /// <summary>
        /// Calculate output value.
        /// </summary>
        /// <returns></returns>
        public virtual double CalculateValue ()
        {
            return Value = Sigmoid.Output (InputConnections.Sum (a => a.Weight * a.InputNeuron.Value) + Bias);
        }

        /// <summary>
        /// Calculate error.
        /// </summary>
        /// <param name="target">Target value.</param>
        /// <returns></returns>
        public double CalculateError (double target)
        {
            return target - Value;
        }

        /// <summary>
        /// Calculate gradient function value.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public double CalculateGradient (double? target = null)
        {
            if (target == null)
                return Gradient = OutputConnections.Sum (a => a.OutputNeuron.Gradient * a.Weight) * Sigmoid.Derivative (Value);

            return Gradient = CalculateError (target.Value) * Sigmoid.Derivative (Value);
        }

        /// <summary>
        /// Update neuron connections weights.
        /// </summary>
        /// <param name="learnRate"></param>
        /// <param name="momentum"></param>
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

        /// <summary>
        /// Overriden ToString() method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return
                $" {nameof(InputConnections)}: {InputConnections}," +
                $" {nameof(OutputConnections)}: {OutputConnections}," +
                $" {nameof(Bias)}: {Bias}," +
                $" {nameof(BiasDelta)}: {BiasDelta}," +
                $" {nameof(Gradient)}: {Gradient}," +
                $" {nameof(Value)}: {Value}";
        }

        /// <summary>
        /// Equality comparer class.
        /// </summary>
        private sealed class NeuronEqualityComparer : IEqualityComparer<Neuron>
        {
            /// <summary>
            /// Overriden equals method.
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public bool Equals(Neuron x, Neuron y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return Equals(x.InputConnections, y.InputConnections) && Equals(x.OutputConnections, y.OutputConnections) && x.Bias.Equals(y.Bias) && x.BiasDelta.Equals(y.BiasDelta) && x.Gradient.Equals(y.Gradient) && x.Value.Equals(y.Value);
            }

            /// <summary>
            /// Overriden get hash code method.
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
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

        /// <summary>
        /// Equality comparer instance.
        /// </summary>
        public static IEqualityComparer<Neuron> NeuronComparer { get; } = new NeuronEqualityComparer();

        /// <summary>
        /// New equals method.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(Neuron other)
        {
            return Bias.Equals(other.Bias) && BiasDelta.Equals(other.BiasDelta) && Gradient.Equals(other.Gradient) && Value.Equals(other.Value);
        }

        /// <summary>
        /// Overriden equals method.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Neuron) obj);
        }

        /// <summary>
        /// Overriden get hash code method.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Neuron left, Neuron right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Non-equality operator.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Neuron left, Neuron right)
        {
            return !Equals(left, right);
        }
    }
}