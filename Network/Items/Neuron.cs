using System;
using Network.Base.Neurons;

namespace Network.Items
{
    /// <summary>
    /// Neuron work item.
    /// </summary>
    public class Neuron : NeuronBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Neuron() : base()
        {
            ForwardCalculation = __sigmoid;
            BackwardCalculation = __sigmoid_derivative;
        }

        /// <summary>
        /// Invokes forward propagation delegate function.
        /// </summary>
        /// <returns></returns>
        public double Invoke()
        {
            return ForwardCalculation.Invoke(InputValue);
        }

        /// <summary>
        /// Invokes backward propagation delegate function.
        /// </summary>
        /// <returns></returns>
        public double Revoke()
        {
            return BackwardCalculation.Invoke(InputValue);
        }

        /// <summary>
        /// Sigmoid function.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected double __sigmoid(double value)
        {
            return 1/(1 + Math.Exp(-value));
        }

        /// <summary>
        /// Backward sigmoid function.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected double __sigmoid_derivative(double value)
        {
            return value*(1 - value);
        }
    }
}