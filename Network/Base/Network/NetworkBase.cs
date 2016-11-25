using System.Collections.Generic;
using System.Linq;
using Network.Base.Connections;
using Network.Items;

namespace Network.Base.Network
{
    public class NetworkBase
    {
        protected List<Neuron> InputNeurons;
        protected List<Neuron> OutputNeurons;

        protected List<Neuron> ComputationalNeurons;
        protected List<NeuralConnection> NeuralConnections;

        public Neuron this[int index]
        {
            get { return InputNeurons[index] ?? (InputNeurons[index] = new Neuron()); }
            set { InputNeurons[index] = value; }
        }
        

    }
}
