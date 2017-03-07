using System;
using System.Collections.Generic;
using System.Linq;
using ObcidiaNetwork.Base;

namespace ObcidiaNetwork.Controllers
{
    internal class NeuronsControllerBase
    {
        public double LearnRate { get; set; }

        public double Momentum { get; set; }

        public List<Neuron> InputLayer { get; set; }

        public List<Neuron> HiddenLayer { get; set; }

        public List<Neuron> OutputLayer { get; set; }

        public NeuronsControllerBase()
        {
            InputLayer = null;
            HiddenLayer = null;
            OutputLayer = null;

            LearnRate = 0;
            Momentum = 0;
        }

        public NeuronsControllerBase (int inputSize, int hiddenSize, int outputSize, double? learnRate = null, double? momentum = null)
        {
            LearnRate = learnRate ?? 0.3;
            Momentum = momentum ?? 0.9;
            InputLayer = new List<Neuron> ();
            HiddenLayer = new List<Neuron> ();
            OutputLayer = new List<Neuron> ();

            for (var i = 0; i < inputSize; i++)
                InputLayer.Add (new Neuron ());

            for (var i = 0; i < hiddenSize; i++)
                HiddenLayer.Add (new Neuron (InputLayer));

            for (var i = 0; i < outputSize; i++)
                OutputLayer.Add (new Neuron (HiddenLayer));
        }

        public double CalculateError (params double[] targets)
        {
            int i = 0;
            return OutputLayer.Sum (a => Math.Abs (a.CalculateError (targets[i++])));
        }
    }
}