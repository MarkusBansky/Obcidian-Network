using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObcidiaNetwork.Controllers
{
    /// <summary>
    /// Neurons controller extention class.
    /// </summary>
    internal class NeuronsController : NeuronsControllerBase
    {
        /// <summary>
        /// Controller contructor.
        /// </summary>
        /// <param name="layersCount">Number of layer of computational neurons in the network.</param>
        /// <param name="inputSize">Number of input neurons.</param>
        /// <param name="hiddenSize">Number of hidden neurons.</param>
        /// <param name="outputSize">Number of output neurons.</param>
        /// <param name="learnRate">Learning rate (0.3 default)</param>
        /// <param name="momentum">Momentum (0.9 default)</param>
        public NeuronsController (int layersCount, int inputSize, int hiddenSize, int outputSize, double? learnRate = null, double? momentum = null) : base(layersCount, inputSize, hiddenSize, outputSize, learnRate, momentum)
        {
            Console.WriteLine($"[Created New Neural Network Controller]");
        }

        /// <summary>
        /// Processes the training algorithm by propagations count.
        /// </summary>
        /// <param name="data">Training data.</param>
        /// <param name="propagationsCount">Number of propagations to loop.</param>
        public void ProcessTraining(KeyValuePair<double[], double[]>[] data, int propagationsCount)
        {
            for (var i = 0; i < propagationsCount; i++)
            {
                foreach (var pair in data)
                {
                    ForwardPropagate (pair.Key);
                    BackwardPropagate (pair.Value);
                }
                Console.WriteLine ($@"[PERFORMING TRAINING SET COMMAND] Index {i + 1} out of {propagationsCount}...");
            }
        }

        /// <summary>
        /// Processes the training algorithm by minimal error.
        /// </summary>
        /// <param name="data">Training data.</param>
        /// <param name="minimalErrorValue">value of minimal error.</param>
        public void ProcessTraining (KeyValuePair<double[], double[]>[] data, double minimalErrorValue)
        {
            double error = 1.0;
            int propagationsCount = 0;

            while (error > minimalErrorValue && propagationsCount < int.MaxValue)
            {
                var errors = new List<double> ();
                foreach (var pair in data)
                {
                    ForwardPropagate (pair.Key);
                    BackwardPropagate (pair.Value);
                    errors.Add (CalculateError (pair.Value));
                }
                error = errors.Average ();
                propagationsCount++;

                Console.WriteLine ($@"[PERFORMING TRAINING SET COMMAND] Index {propagationsCount} out of many...   [ERROR]:{error}; [MINIMAL]:{minimalErrorValue};");
            }
        }

        /// <summary>
        /// Performs calculations.
        /// </summary>
        /// <param name="inputs">Input values.</param>
        /// <returns></returns>
        public double[] PerformCalculations(double[] inputs)
        {
            ForwardPropagate (inputs);
            return OutputLayer.Select (a => a.Value).ToArray ();
        }

        /// <summary>
        /// Performs forward propagation.
        /// </summary>
        /// <param name="inputValues">Input values.</param>
        public void ForwardPropagate(double[] inputValues)
        {
            int index = 0;
            InputLayer.ForEach (a => a.Value = inputValues[index++]);
            HiddenLayers.ForEach (l => l.Neurons.ForEach(n => n.CalculateValue()));
            OutputLayer.ForEach (a => a.CalculateValue ());
        }

        /// <summary>
        /// Performs backward propagation.
        /// </summary>
        /// <param name="trainingResults">Training result values.</param>
        public void BackwardPropagate(double[] trainingResults)
        {
            int index = 0;
            OutputLayer.ForEach (a => a.CalculateGradient (trainingResults[index++]));

            for (int i = HiddenLayers.Count - 1; i > 0; i--)
                HiddenLayers[i].Neurons.ForEach(a => a.CalculateGradient());
            for (int i = HiddenLayers.Count - 1; i > 0; i--)
                HiddenLayers[i].Neurons.ForEach (a => a.UpdateWeights (LearnRate, Momentum));

            OutputLayer.ForEach (a => a.UpdateWeights (LearnRate, Momentum));
        }
    }
}