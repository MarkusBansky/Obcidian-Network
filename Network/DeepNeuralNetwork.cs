using System.Collections.Generic;
using System.Linq;
using NeuralComponents.Base;
using NeuralComponents.Base.Neurons;
using NeuralComponents.Connections;

namespace Network
{
    public class DeepNeuralNetwork
    {
        public InputNeuron[] InputNeurons;
        public OutputNeuron OutputNeuron;

        private readonly List<ComputationalNeuron> _computationalNeurons;
        private readonly List<NeuralConnection> _neuralConnections;

        public DeepNeuralNetwork(int numberOfInputValues)
        {
            _computationalNeurons = new List<ComputationalNeuron>();
            _neuralConnections = new List<NeuralConnection>();

            InputNeurons = new InputNeuron[numberOfInputValues];
            OutputNeuron = new OutputNeuron();
        }

        public void SetInputValue(int index, double value)
        {
            if(InputNeurons[index] == null)
                InputNeurons[index] = new InputNeuron();

            InputNeurons[index].Value = value;
        }

        public double GetOutputValue()
        {
            return OutputNeuron.Value;
        }

        public void AddComputationalNeuron(ComputationalNeuron neuron)
        {
            _computationalNeurons.Add(neuron);
        }

        public void AddConnection(int first, int second)
        {
            _neuralConnections.Add(new NeuralConnection(first, second));
        }

        public List<double> GetMultiplierValues()
        {
            List<double> output = new List<double>();

            foreach(NeuralConnection nc in _neuralConnections)
                output.Add(nc.Multiplier);

            return output;
        }

        private Neuron GetNeuron(int index)
        {
            if (index < InputNeurons.Length)
                return InputNeurons[index];
            else if (index < _computationalNeurons.Count + InputNeurons.Length)
                return _computationalNeurons[index - InputNeurons.Length];
            else
                return OutputNeuron;
        }

        public void ForwardPropagation()
        {
            foreach (ComputationalNeuron cn in _computationalNeurons)
            {
                // Calculate neuron input value for computations
                //cn.InputValue =_neuralConnections.Where(n => GetNeuron(n.NextNeuron).Guid.Equals(cn.Guid)).Sum(nc => GetNeuron(nc.PreviousNeuron).Value*nc.Multiplier);
                double sum = 0;
                foreach (NeuralConnection nc in _neuralConnections)
                {
                    if (GetNeuron(nc.NextNeuron).Guid.Equals(cn.Guid))
                    {
                        sum += GetNeuron(nc.PreviousNeuron).Value*nc.Multiplier;
                    }
                }
                cn.InputValue = sum;

                // Calculate the VALUE of neuron from assigned function
                cn.Value = cn.Invoke();
            }

            // Calculate the output
            OutputNeuron.Value = _neuralConnections.Where(n => GetNeuron(n.NextNeuron).Guid.Equals(OutputNeuron.Guid)).Sum(nc => GetNeuron(nc.PreviousNeuron).Value * nc.Multiplier);
        }

        public void BackPropagation(double target)
        {
            // Fixing connections after input
            foreach (ComputationalNeuron cn in _computationalNeurons)
            {
                List<NeuralConnection> previousConnections = _neuralConnections.Where(nc => GetNeuron(nc.NextNeuron).Guid.Equals(cn.Guid)).ToList();
                List<NeuralConnection> nextConnections = _neuralConnections.Where(nc => GetNeuron(nc.PreviousNeuron).Guid.Equals(cn.Guid)).ToList();

                double errorMargin = target - OutputNeuron.Value;
                double deltaOutputSum = cn.Invoke() * errorMargin;

                double deltaHiddenSum = nextConnections.Sum(nc => deltaOutputSum / nc.Multiplier * cn.Invoke());

                foreach (NeuralConnection nc in previousConnections)
                {
                    double deltaWeight = deltaHiddenSum / GetNeuron(nc.PreviousNeuron).Value;
                    nc.Multiplier += deltaWeight;
                }
            }

            // Fixing connections before output
            foreach (NeuralConnection nc in _neuralConnections)
            {
                if (!GetNeuron(nc.NextNeuron).Guid.Equals(OutputNeuron.Guid)) continue;

                ComputationalNeuron cn =
                    _computationalNeurons.Find(c => c.Guid.Equals(GetNeuron(nc.PreviousNeuron).Guid));

                double errorMargin = target - OutputNeuron.Value;
                double deltaSum = cn.Invoke()*errorMargin;
                double deltaWeight = deltaSum/GetNeuron(nc.PreviousNeuron).Value;

                nc.Multiplier += deltaWeight;
            }
        }
    }
}
