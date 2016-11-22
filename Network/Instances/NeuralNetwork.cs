using System.Collections.Generic;
using Network.Base.Connections;
using Network.Base.Network;

namespace Network.Instances
{
    public class NeuralNetwork : NetworkBase
    {
        public NeuralNetwork(int numberOfInputValues, int numberOfOutputNeurons)
        {
            ComputationalNeurons = new List<Neuron>();
            NeuralConnections = new List<NeuralConnection>();

            InputNeurons = new List<Neuron>();
            for(int i = 0; i < numberOfInputValues; i++)
                InputNeurons.Add(new Neuron());

            OutputNeurons = new List<Neuron>();
            for (int i = 0; i < numberOfOutputNeurons; i++)
                OutputNeurons.Add(new Neuron());
        }
        
        public void AddNeuron(Neuron neuron)
        {
            ComputationalNeurons.Add(neuron);
        }

        public void AddConnection(int first, int second)
        {
            NeuralConnections.Add(new NeuralConnection(first, second));
        }

        public void AddConnections(int[,] connectionPairsArray)
        {
            int height = connectionPairsArray.Length / 2;

            for (int y = 0; y < height; y++)
            {
                NeuralConnections.Add(new NeuralConnection(connectionPairsArray[y, 0], connectionPairsArray[y, 1]));
            }
        }

        public double[] TrainPropagation(double[] inputDoubles, double[] outputDoubles)
        {
            for (int i = 0; i < InputNeurons.Count; i++)
                SetInputValue(i, inputDoubles[i]);

            // Perform forward propagation
            ForwardPropagation();

            // Perform backward propagation
            BackPropagation(outputDoubles);

            return GetOutputValues();
        }
    }
}
