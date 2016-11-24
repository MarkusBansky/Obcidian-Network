﻿using System;

namespace Network.Base.Neurons
{
    /// <summary>
    /// Basic neuron class.
    /// </summary>
    public class NeuronBase : IEquatable<NeuronBase>
    {
        /// <summary>
        /// Unique Identificator.
        /// </summary>
        public readonly string Guid;

        /// <summary>
        /// Neuron value after sigmoid calculations.
        /// </summary>
        public double Value;

        /// <summary>
        /// Input value calculated as sum of connections * multipliers.
        /// </summary>
        public double InputValue = 0;

        /// <summary>
        /// Calculation function delegat.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public delegate double Calculation(double value);

        /// <summary>
        /// Forward calculation function.
        /// </summary>
        public Calculation ForwardCalculation;

        /// <summary>
        /// Backward calculation function.
        /// </summary>
        public Calculation BackwardCalculation;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NeuronBase()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Sigmoid function.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected double __sigmoid(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }

        /// <summary>
        /// Backward sigmoid function.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected double __sigmoid_derivative(double value)
        {
            return value * (1 - value);
        }

        /// <summary>
        /// Implemented IEquatable interface for IEnumerable collections.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NeuronBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || string.Equals(Guid, other.Guid);
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
            return obj.GetType() == GetType() && Equals((NeuronBase) obj);
        }

        /// <summary>
        /// Overloaded hash code method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Guid?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// Overloaded == operator.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(NeuronBase left, NeuronBase right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Overloaded != operator.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(NeuronBase left, NeuronBase right)
        {
            return !Equals(left, right);
        }
    }
}
