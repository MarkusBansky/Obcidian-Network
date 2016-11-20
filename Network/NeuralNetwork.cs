using System;
using System.Collections.Generic;
using System.Linq;
using Network.Base.Connections;
using Network.Base.Neurons;

namespace Network
{
    public class NeuralNetwork
    {
        private readonly List<Neuron> _inputNeurons;
        private readonly List<Neuron> _outputNeurons;

        private readonly List<Neuron> _computationalNeurons;
        private List<NeuralConnection> _neuralConnections;

        public NeuralNetwork(int numberOfInputValues, int numberOfOutputNeurons)
        {
            _computationalNeurons = new List<Neuron>();
            _neuralConnections = new List<NeuralConnection>();

            _inputNeurons = new List<Neuron>();
            for(int i = 0; i < numberOfInputValues; i++)
                _inputNeurons.Add(new Neuron());

            _outputNeurons = new List<Neuron>();
            for (int i = 0; i < numberOfOutputNeurons; i++)
                _outputNeurons.Add(new Neuron());
        }

        #region Overloaded Methods
        public Neuron this[int index]
        {
            get { return _inputNeurons[index] ?? (_inputNeurons[index] = new Neuron()); }
            set { _inputNeurons[index] = value; }
        }
        #endregion

        public void SetInputValue(int index, double value)
        {
            if(_inputNeurons[index] == null)
                _inputNeurons[index] = new Neuron();

            _inputNeurons[index].Value = value;
        }

        public void AddNeuron(Neuron neuron)
        {
            _computationalNeurons.Add(neuron);
        }

        public void AddConnection(int first, int second)
        {
            _neuralConnections.Add(new NeuralConnection(first, second));
        }

        //public void AddConnections(int[,] connectionPairsArray)
        //{
        //    int height = connectionPairsArray.Length/2;

        //    for (int y = 0; y < height; y++)
        //    {
        //        _neuralConnections.Add(new NeuralConnection(connectionPairsArray[y, 0], connectionPairsArray[y, 1]));
        //    }
        //}

        public double[] GetOutputValues()
        {
            return _outputNeurons.Select(on => on.Value).ToArray();
        }

        public double[] TrainPropagation(double[] inputDoubles, double[] outputDoubles, double errorDelta)
        {
            for (int i = 0; i < _inputNeurons.Count; i++)
                 SetInputValue(i, inputDoubles[i]);

            // Perform forward propagation
            ForwardPropagation();

            // Calculate the total error
            //double _totalError = _outputNeurons.Sum(on => Math.Pow(outputDoubles[_outputNeurons.FindIndex(fi => Equals(fi, on))] - on.Value, 2)/2.0);

            // Perform backward propagation
            BackPropagation(outputDoubles);

            return GetOutputValues();
        }

        public double[] ForwardPropagation()
        {
            foreach (Neuron cn in _computationalNeurons)
            {
                cn.InputValue =_neuralConnections.Where(n => GetNeuron(n.NextNeuron).Guid.Equals(cn.Guid)).Sum(nc => GetNeuron(nc.PreviousNeuron).Value*nc.Multiplier);
                cn.Value = cn.Invoke();
            }
            
            _outputNeurons.ForEach(on =>
            {
                on.InputValue =
                    _neuralConnections.Where(n => GetNeuron(n.NextNeuron).Guid.Equals(on.Guid))
                        .Sum(nc => GetNeuron(nc.PreviousNeuron).Value*nc.Multiplier);
                on.Value = on.Invoke();
            });

            return GetOutputValues();
        }

        public void BackPropagation(double[] targetValues)
        {
            // Fixing connections
            List<NeuralConnection> newNeuralConnections = new List<NeuralConnection>(_neuralConnections);

            // For output connections
            for (int i = 0; i < targetValues.Length; i++)
            {
                NeuralConnection connection =_neuralConnections[_neuralConnections.Count - _outputNeurons.Count + i];
                double dEtotal = -(targetValues[i] - _outputNeurons[i].Value)*_outputNeurons[i].Value * (1 - _outputNeurons[i].Value)*GetNeuron(connection.PreviousNeuron).Value;

                newNeuralConnections[_neuralConnections.IndexOf(connection)].Multiplier -= 0.5*dEtotal;
            }

            // for other connections
            foreach(NeuralConnection connection in _neuralConnections.Where(nc => nc.NextNeuron < _inputNeurons.Count + _computationalNeurons.Count))
            {
                double dEtotal = 
                    DeltaTotalSum(GetNeuron(connection.NextNeuron), targetValues) 
                    * GetNeuron(connection.NextNeuron).Value 
                    * (1 - GetNeuron(connection.NextNeuron).Value) 
                    * GetNeuron(connection.PreviousNeuron).Value;
                newNeuralConnections[_neuralConnections.IndexOf(connection)].Multiplier -= 0.5*dEtotal;
            }

            _neuralConnections = newNeuralConnections;
        }

        private double DeltaTotalSum(Neuron neuron, double[] targetValues)
        {
            List<NeuralConnection> outputConnections = _neuralConnections.Where(nc => GetNeuron(nc.PreviousNeuron).Equals(neuron)).ToList();
            if (outputConnections.OrderBy(c => c.NextNeuron).Last().NextNeuron >=
                _computationalNeurons.Count + _inputNeurons.Count)
            {
                double sum = 0;
                for (int i = 0; i < targetValues.Length; i++)
                {
                    sum += -(targetValues[i] - _outputNeurons[i].Value)*_outputNeurons[i].Value *
                           (1 - _outputNeurons[i].Value) * _neuralConnections[_neuralConnections.Count - _outputNeurons.Count + i].Multiplier;
                }
                return sum;
            }
            else
            {
                double sum = 0;
                for (int i = 0; i < outputConnections.Count; i++)
                {
                    sum += -(targetValues[i] - _outputNeurons[i].Value) * neuron.Value *
                           (1 - neuron.Value) * outputConnections[i].Multiplier;
                }
                return sum;
            }
        }

        private Neuron GetNeuron(int index)
        {
            if (index < _inputNeurons.Count)
                return _inputNeurons[index];
            else if (index < _computationalNeurons.Count + _inputNeurons.Count)
                return _computationalNeurons[index - _inputNeurons.Count];
            else
                return _outputNeurons[index - (_inputNeurons.Count + _computationalNeurons.Count)];
        }
    }
}
