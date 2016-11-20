using System;
using Network.Instances;

namespace Network.Base.Neurons
{
    public class NeuronBase : IEquatable<NeuronBase>
    {
        public string Guid;
        public double Value;

        public double InputValue = 0;

        public delegate double Calculation(double value);
        public Calculation ForwardCalculation;
        public Calculation BackwardCalculation;

        public override bool Equals(object obj)
        {
            Neuron neuron = obj as Neuron;
            return neuron != null && (obj.GetType() == typeof(Neuron) && neuron.Guid.Equals(Guid));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Guid?.GetHashCode() ?? 0) * 397) ^ Value.GetHashCode();
            }
        }

        bool IEquatable<NeuronBase>.Equals(NeuronBase other)
        {
            return other != null && (string.Equals(Guid, other.Guid) && Value.Equals(other.Value));
        }

        protected double __sigmoid(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }

        protected double __sigmoid_derivative(double value)
        {
            return value * (1 - value);
        }
    }
}
