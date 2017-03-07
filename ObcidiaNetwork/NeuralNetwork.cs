using System;
using System.Collections.Generic;
using System.IO;
using ObcidiaNetwork.Controllers;
using ObcidiaNetwork.IO;

namespace ObcidiaNetwork
{
    /// <summary>
    /// Obcidian Network main class. Creates a new neural network.
    /// </summary>
    public class NeuralNetwork
    {
        /// <summary>
        /// Network controller that holds all network and methods.
        /// </summary>
        private NeuronsController _controller;

        /// <summary>
        /// Creates new neural network object.
        /// </summary>
        /// <param name="inputsCount">Number of input neurons.</param>
        /// <param name="computationalCount">Number of computational neurons and their biases.</param>
        /// <param name="outputsCount">Number of output neurons.</param>
        public NeuralNetwork (int inputsCount, int computationalCount, int outputsCount)
        {
            _controller = new NeuronsController(inputsCount, computationalCount, outputsCount);
        }

        /// <summary>
        /// Computes the network values and writes results to output neurons.
        /// </summary>
        /// <param name="inputValues">Input values.</param>
        /// <returns></returns>
        public double[] Calculate (double[] inputValues)
        {
            return _controller.PerformCalculations(inputValues);
        }

        /// <summary>
        /// Performs training propagation. Firstly sets the input values and after calculates expected output and adjusts weights.
        /// </summary>
        /// <param name="trainingData">Data used for training consists of input values and expected output values.</param>
        /// <param name="cyclesCount">Number of times the network should process this data.</param>
        public void Train (KeyValuePair<double[], double[]>[] trainingData, int cyclesCount)
        {
            _controller.ProcessTraining(trainingData, cyclesCount);
        }

        /// <summary>
        /// Performs training propagation. Firstly sets the input values and after calculates expected output and adjusts weights.
        /// </summary>
        /// <param name="trainingData">Data used for training consists of input values and expected output values.</param>
        /// <param name="minimalError">Value for minimal error.</param>
        public void Train (KeyValuePair<double[], double[]>[] trainingData, double minimalError)
        {
            _controller.ProcessTraining (trainingData, minimalError);
        }

        /// <summary>
        /// Returns raw json string for export purposes.
        /// </summary>
        /// <param name="pathToFile">Path to file to wich it should be exported.</param>
        public void ExportToFile(string pathToFile)
        {
            NnExporter exporter = new NnExporter(_controller);
            exporter.Export(pathToFile);
        }

        /// <summary>
        /// Loads this network from fole. Rewrites all current states and neurons with connections. Be careful using this method.
        /// </summary>
        /// <param name="pathToFile">Path to file wich to open</param>
        public void ImportFromFile(string pathToFile)
        {
            NnImporter importer = new NnImporter(_controller);
            _controller = importer.Import(pathToFile);
        }
    }
}
