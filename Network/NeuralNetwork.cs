using System.Collections.Generic;
using System.Linq;
using Network.Base.Connections;
using Network.Base.Neurons;

namespace Network
{
    public class NeuralNetwork
    {
        private readonly Neuron[] _inputNeurons;
        private readonly Neuron _outputNeuron;

        private readonly List<ComputationalNeuron> _computationalNeurons;
        private readonly List<NeuralConnection> _neuralConnections;

        public NeuralNetwork(int numberOfInputValues)
        {
            _computationalNeurons = new List<ComputationalNeuron>();
            _neuralConnections = new List<NeuralConnection>();

            _inputNeurons = new Neuron[numberOfInputValues];
            _outputNeuron = new Neuron();
        }

        #region Overloaded Methods
        public Neuron this[int index]
        {
            get { return _inputNeurons[index] ?? (_inputNeurons[index] = new Neuron()); }
            set { _inputNeurons[index] = value; }
        }
        #endregion

        public double GetOutputValue()
        {
            return _outputNeuron.Value;
        }

        public void AddNeuron(ComputationalNeuron neuron)
        {
            _computationalNeurons.Add(neuron);
        }

        public void AddConnection(int first, int second)
        {
            _neuralConnections.Add(new NeuralConnection(first, second));
        }

        public void AddConnections(int[,] connectionPairsArray)
        {
            // TODO: Write connection pairs to connections
        }

        public List<double> GetMultiplierValues()
        {
            return _neuralConnections.Select(nc => nc.Multiplier).ToList();
        }

        private Neuron GetNeuron(int index)
        {
            if (index < _inputNeurons.Length)
                return _inputNeurons[index];
            else if (index < _computationalNeurons.Count + _inputNeurons.Length)
                return _computationalNeurons[index - _inputNeurons.Length];
            else
                return _outputNeuron;
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
            _outputNeuron.Value = _neuralConnections.Where(n => GetNeuron(n.NextNeuron).Guid.Equals(_outputNeuron.Guid)).Sum(nc => GetNeuron(nc.PreviousNeuron).Value * nc.Multiplier);
        }

        public void BackPropagation(double target)
        {
            // Fixing connections after input
            foreach (ComputationalNeuron cn in _computationalNeurons)
            {
                List<NeuralConnection> previousConnections = _neuralConnections.Where(nc => GetNeuron(nc.NextNeuron).Guid.Equals(cn.Guid)).ToList();
                List<NeuralConnection> nextConnections = _neuralConnections.Where(nc => GetNeuron(nc.PreviousNeuron).Guid.Equals(cn.Guid)).ToList();

                double errorMargin = target - _outputNeuron.Value;
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
                if (!GetNeuron(nc.NextNeuron).Guid.Equals(_outputNeuron.Guid)) continue;

                ComputationalNeuron cn =
                    _computationalNeurons.Find(c => c.Guid.Equals(GetNeuron(nc.PreviousNeuron).Guid));

                double errorMargin = target - _outputNeuron.Value;
                double deltaSum = cn.Invoke()*errorMargin;
                double deltaWeight = deltaSum/GetNeuron(nc.PreviousNeuron).Value;

                nc.Multiplier += deltaWeight;
            }
        }
    }
}
