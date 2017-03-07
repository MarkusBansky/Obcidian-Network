using System.IO;
using ObcidiaNetwork.Items;

namespace ObcidiaNetwork
{
    public class NeuralNetwork
    {
        private readonly NeuronsController _controller;

        /// <summary>
        /// Creates new neural network object.
        /// </summary>
        /// <param name="inputsCount">Number of input neurons.</param>
        /// <param name="computationalCount">Number of computational neurons and their biases.</param>
        /// <param name="outputsCount">Number of output neurons.</param>
        /// <param name="useBiases">Use biases or not.</param>
        public NeuralNetwork (int inputsCount, int computationalCount, int outputsCount, bool useBiases = false)
        {
            _controller = new NeuronsController(inputsCount, computationalCount, outputsCount, useBiases);
        }

        /// <summary>
        /// Set values for input neurons that will be used for computing outputs.
        /// </summary>
        /// <param name="inputValues">Input values array.</param>
        public void SetInputValues(float[] inputValues)
        {
            _controller.SetInputValues(inputValues);
        }

        /// <summary>
        /// Returns output values.
        /// </summary>
        /// <returns></returns>
        public float[] GetOutputValues()
        {
            return _controller.GetOutputValues();
        }

        /// <summary>
        /// Computes the network values and writes results to output neurons.
        /// </summary>
        /// <param name="inputValues">Input values.</param>
        /// <returns></returns>
        public float[] CalculateOutputs (float[] inputValues)
        {
            _controller.SetInputValues (inputValues);
            _controller.ForwardPropagation ();
            return _controller.GetOutputValues ();
        }

        /// <summary>
        /// Computes the network values and writes results to output neurons.
        /// </summary>
        public float[] CalculateOutputs ()
        {
            _controller.ForwardPropagation();
            return _controller.GetOutputValues ();
        }

        /// <summary>
        /// Performs training propagation. Firstly sets the input values and after calculates expected output and adjusts weights.
        /// </summary>
        /// <param name="inputValues">Input values.</param>
        /// <param name="expectedValues">Expected output values.</param>
        public void Train (float[] inputValues, float[] expectedValues)
        {
            _controller.SetInputValues(inputValues);
            _controller.ForwardPropagation ();
            _controller.BackwardPropagation(expectedValues);
        }

        /// <summary>
        /// Returns raw json string for export purposes.
        /// </summary>
        /// <returns></returns>
        public string ExportJson()
        {
            return _controller.ToString();
        }

        /// <summary>
        /// Loads this network from fole. Rewrites all current states and neurons with connections. Be careful using this method.
        /// </summary>
        /// <param name="reader"></param>
        public void ImportJson(StreamReader reader)
        {
            string json = reader.ReadToEnd();
            _controller.ParseNetwork(json);
        }
    }
}
