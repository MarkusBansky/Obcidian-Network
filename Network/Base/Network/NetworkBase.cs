using System.Collections.Generic;
using System.Linq;
using Network.Base.Connections;
using Network.Items;

namespace Network.Base.Network
{
    public class NetworkBase
    {
        /// <summary>
        /// Input neurons list.
        /// </summary>
        protected List<Neuron> InputNeurons;

        /// <summary>
        /// Output neurons list.
        /// </summary>
        protected List<Neuron> OutputNeurons;

        /// <summary>
        /// Computational neurons list for layers.
        /// </summary>
        protected List<Neuron> ComputationalNeurons;

        /// <summary>
        /// Neurons connections mappings.
        /// </summary>
        protected List<NeuralConnection> NeuralConnections;

        /// <summary>
        /// [] overloaded method.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Neuron this[int index]
        {
            get
            {
                if (index < InputNeurons.Count)
                    return InputNeurons[index];
                return index < ComputationalNeurons.Count + InputNeurons.Count ? ComputationalNeurons[index - InputNeurons.Count] : OutputNeurons[index - (InputNeurons.Count + ComputationalNeurons.Count)];
            }
            set
            {
                if (index < InputNeurons.Count)
                    InputNeurons[index] = value;
                else if (index < ComputationalNeurons.Count + InputNeurons.Count)
                    ComputationalNeurons[index - InputNeurons.Count] = value;
                else
                    OutputNeurons[index - (InputNeurons.Count + ComputationalNeurons.Count)] = value;
            }
        }
    }
}
