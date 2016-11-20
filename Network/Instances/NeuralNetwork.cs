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
