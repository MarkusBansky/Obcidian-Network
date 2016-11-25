namespace Network.Extentions.Interfaces
{
    /// <summary>
    /// Neuron inner function.
    /// </summary>
    public interface INeuronFunction
    {
        /// <summary>
        /// Function for forward propagation
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        double ForwardFunction(double value);

        /// <summary>
        /// Function for backward propagation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        double BackwardFunction(double value);
    }
}
