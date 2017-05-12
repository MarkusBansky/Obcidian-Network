using System;
using System.Collections.Generic;
using System.Linq;
using ObcidiaNetwork.Base;
using ObcidiaNetwork.Helpers;

namespace ObcidiaNetwork.Controllers
{
    /// <summary>
    /// Main controller class.
    /// </summary>
    internal class NeuronsControllerBase
    {
        /// <summary>
        /// Local learning rate.
        /// </summary>
        public double LearnRate { get; set; }

        /// <summary>
        /// Local momentum value.
        /// </summary>
        public double Momentum { get; set; }

        /// <summary>
        /// List of input neurons.
        /// </summary>
        public List<Neuron> InputLayer { get; set; }

        /// <summary>
        /// List of hidden layer neurons.
        /// </summary>
        public List<NeuronsLayer> HiddenLayers { get; set; }

        /// <summary>
        /// List of output neurons.
        /// </summary>
        public List<Neuron> OutputLayer { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NeuronsControllerBase()
        {
            InputLayer = null;
            HiddenLayers = null;
            OutputLayer = null;

            LearnRate = 0;
            Momentum = 0;
        }

        /// <summary>
        /// Main contructor.
        /// </summary>
        /// <param name="layersCount"></param>
        /// <param name="inputSize"></param>
        /// <param name="hiddenSize"></param>
        /// <param name="outputSize"></param>
        /// <param name="learnRate"></param>
        /// <param name="momentum"></param>
        public NeuronsControllerBase (int layersCount, int inputSize, int hiddenSize, int outputSize, double? learnRate = null, double? momentum = null)
        {
            LearnRate = learnRate ?? 0.3;
            Momentum = momentum ?? 0.9;
            InputLayer = new List<Neuron> ();
            HiddenLayers = new List<NeuronsLayer> ();
            OutputLayer = new List<Neuron> ();

            for (var i = 0; i < inputSize; i++)
                InputLayer.Add (new Neuron ());

            for (var i = 0; i < layersCount; i++)
                HiddenLayers.Add (new NeuronsLayer (hiddenSize, i == 0 ? InputLayer : HiddenLayers.Last().Neurons));

            for (var i = 0; i < outputSize; i++)
                OutputLayer.Add (new Neuron (HiddenLayers.Last().Neurons));
        }

        /// <summary>
        /// Calculates error.
        /// </summary>
        /// <param name="targets">Target values.</param>
        /// <returns></returns>
        public double CalculateError (params double[] targets)
        {
            int i = 0;
            return OutputLayer.Sum (a => Math.Abs (a.CalculateError (targets[i++])));
        }
    }
}