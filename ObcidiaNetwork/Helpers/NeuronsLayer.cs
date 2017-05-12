using System.Collections.Generic;
using ObcidiaNetwork.Base;

namespace ObcidiaNetwork.Helpers
{
    internal class NeuronsLayer
    {
        /// <summary>
        /// Neurons container for this layer
        /// </summary>
        public List<Neuron> Neurons;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="numberOfNeurons"></param>
        /// <param name="lastNeurons"></param>
        public NeuronsLayer(int numberOfNeurons, List<Neuron> lastNeurons)
        {
            Neurons = new List<Neuron>();

            for (var i = 0; i < numberOfNeurons; i++)
                Neurons.Add(new Neuron(lastNeurons));
        }
    }
}