using System;
using System.Collections.Generic;
using System.Linq;

namespace ObcidiaNetwork.Controllers
{
    internal class NeuronsController : NeuronsControllerBase
    {
        public NeuronsController (int inputSize, int hiddenSize, int outputSize, double? learnRate = null, double? momentum = null) : base(inputSize, hiddenSize, outputSize, learnRate, momentum)
        {
            Console.WriteLine($"[Created New Neural Network Controller]");
        }

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

        public double[] PerformCalculations(double[] inputs)
        {
            ForwardPropagate (inputs);
            return OutputLayer.Select (a => a.Value).ToArray ();
        }

        public void ForwardPropagate(double[] inputValues)
        {
            int index = 0;
            InputLayer.ForEach (a => a.Value = inputValues[index++]);
            HiddenLayer.ForEach (a => a.CalculateValue ());
            OutputLayer.ForEach (a => a.CalculateValue ());
        }

        public void BackwardPropagate(double[] trainingResults)
        {
            int index = 0;
            OutputLayer.ForEach (a => a.CalculateGradient (trainingResults[index++]));
            HiddenLayer.ForEach (a => a.CalculateGradient ());
            HiddenLayer.ForEach (a => a.UpdateWeights (LearnRate, Momentum));
            OutputLayer.ForEach (a => a.UpdateWeights (LearnRate, Momentum));
        }
    }
}