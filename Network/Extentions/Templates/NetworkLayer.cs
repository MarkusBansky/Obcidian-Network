using System;
using System.Collections.Generic;
using System.Linq;
using Network.Base;
using Network.Enumerations;
using Network.Extentions.Interfaces;
using Network.Items;

namespace Network.Extentions.Templates
{
    public class NetworkLayer : INetworkLayer
    {
        public int LayerIndex;
        public List<NeuronBase> Neurons;

        public NetworkLayer(NeuronFunctions functionType, int neuronsCount)
        {
            Neurons = new List<NeuronBase>(neuronsCount);
            for (int i = 0; i < neuronsCount; i++)
            {
                Neurons[i] = new Neuron(functionType);
            }
        }

        public NeuronBase this[int index]
        {
            get { return Neurons[index]; }
            set { Neurons[index] = value; }
        }

        public void ForwardCalculation()
        {
            throw new System.NotImplementedException();
        }

        public double[] GetValues()
        {
            return Neurons.Select(n => n.Value).ToArray();
        }

        public double[] GetInputValues()
        {
            return Neurons.Select(n => n.InputValue).ToArray();
        }
    }
}
