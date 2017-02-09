using System;
using ObcidiaNetwork.Helpers;

namespace ObcidiaNetwork.Base
{
    internal class NeuronBase
    {
        public float InputValue;
        public float OutputValue;

        /// <summary>
        /// Calculation function delegate.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public delegate float Function (float value);

        /// <summary>
        /// Forward calculation function.
        /// </summary>
        public Function ComputeFunction;

        /// <summary>
        /// Backward calculation function.
        /// </summary>
        public Function PrimeFunction;

        public NeuronBase ()
        {
            InputValue = 0;
            OutputValue = 0;

            ComputeFunction = __sigmoid;
            PrimeFunction = __sigmoid_derivative;
        }

        /// <summary>
        /// Sigmoid function.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected float __sigmoid (float value)
        {
            return 1.0f / (float)(1.0f + Math.Exp (-value));
        }

        /// <summary>
        /// Backward sigmoid function.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected float __sigmoid_derivative (float value)
        {
            return value * (1 - value);
        }

        protected bool Equals(NeuronBase other)
        {
            return InputValue.Equals(other.InputValue) && OutputValue.Equals(other.OutputValue) && Equals(ComputeFunction, other.ComputeFunction) && Equals(PrimeFunction, other.PrimeFunction);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((NeuronBase) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = InputValue.GetHashCode();
                hashCode = (hashCode * 397) ^ OutputValue.GetHashCode();
                hashCode = (hashCode * 397) ^ (ComputeFunction != null ? ComputeFunction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PrimeFunction != null ? PrimeFunction.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(NeuronBase left, NeuronBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NeuronBase left, NeuronBase right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"{{\"input\":{InputValue},\"output\":{OutputValue},\"id\":\"{HashGenerator.GenerateString(GetHashCode().ToString())}\"}}";
        }
    }
}