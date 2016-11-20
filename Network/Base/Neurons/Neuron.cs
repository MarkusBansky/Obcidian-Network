using System;

namespace Network.Base.Neurons
{
    public class Neuron : IEquatable<Neuron>
    {
        public readonly string Guid;
        public double Value;

        public double InputValue = 0;

        public delegate double Calculation(double value);
        public Calculation ForwardCalculation;
        public Calculation BackwardCalculation;

        public Neuron()
        {
            Guid = System.Guid.NewGuid().ToString();

            ForwardCalculation = __sigmoid;
            BackwardCalculation = __sigmoid_derivative;
        }

        public override bool Equals(object obj)
        {
            Neuron neuron = obj as Neuron;
            return neuron != null && (obj.GetType() == typeof(Neuron) && neuron.Guid.Equals(Guid));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Guid?.GetHashCode() ?? 0)*397) ^ Value.GetHashCode();
            }
        }

        bool IEquatable<Neuron>.Equals(Neuron other)
        {
            return other != null && (string.Equals(Guid, other.Guid) && Value.Equals(other.Value));
        }

        public double Invoke()
        {
            return ForwardCalculation.Invoke(InputValue);
        }

        public double Revoke()
        {
            return BackwardCalculation.Invoke(InputValue);
        }

        private double __sigmoid(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }

        private double __sigmoid_derivative(double value)
        {
            return value * (1 - value);
        }
    }
}
