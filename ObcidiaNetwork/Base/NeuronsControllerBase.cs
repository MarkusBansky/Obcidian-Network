using System;
using System.Collections.Generic;
using System.Text;

namespace ObcidiaNetwork.Base
{
    internal class NeuronsControllerBase
    {
        public NeuronBase[] NeuronsContainer;
        public List<ConnectionBase> ConnectionsContainer;

        public int InputsCount { get; }

        public int BiasesCount { get; }

        public int ComputationalCount { get; }

        public int OutputsCount { get; }

        public NeuronsControllerBase()
        {
            NeuronsContainer = null;
            ConnectionsContainer = null;

            InputsCount = 0;
            BiasesCount = 0;
            ComputationalCount = 0;
            OutputsCount = 0;
        }

        public NeuronsControllerBase (int inputs, int computational, int outputs, int biases)
        {
            InputsCount = inputs;
            BiasesCount = biases;
            ComputationalCount = computational;
            OutputsCount = outputs;

            NeuronsContainer = new NeuronBase[InputsCount + BiasesCount + ComputationalCount + OutputsCount];
            ConnectionsContainer = new List<ConnectionBase>();
            
            for (int i = 0; i < NeuronsContainer.Length; i++)
            {
                NeuronsContainer[i] = new NeuronBase ();
            }
        }

        public int NormalizeIndexForInputs(int index)
        {
            if (index >= 0 && index < InputsCount)
            {
                return index;
            }
            throw new IndexOutOfRangeException ();
        }

        public int NormalizeIndexForBiases (int index)
        {
            index += InputsCount;

            if (index >= InputsCount && index < InputsCount + BiasesCount)
            {
                return index;
            }
            throw new IndexOutOfRangeException ();
        }

        public int NormalizeIndexForComputational (int index)
        {
            index += InputsCount + BiasesCount;

            if (index >= InputsCount + BiasesCount && index < InputsCount + BiasesCount + ComputationalCount)
            {
                return index;
            }
            throw new IndexOutOfRangeException ();
        }

        public int NormalizeIndexForOutputs (int index)
        {
            index += InputsCount + BiasesCount + ComputationalCount;

            if (index >= InputsCount + BiasesCount + ComputationalCount && index < InputsCount + BiasesCount + ComputationalCount + OutputsCount)
            {
                return index;
            }
            throw new IndexOutOfRangeException ();
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.Append("{");

            output.Append ("\"neurons\":[");
            foreach (NeuronBase neuron in NeuronsContainer)
                output.Append(neuron + ",");
            output.Remove (output.Length - 1, 1);

            output.Append ("],\"connections\":[");
            foreach (ConnectionBase connection in ConnectionsContainer)
                output.Append (connection + ",");
            output.Remove(output.Length - 1, 1);
            output.Append ($"],\"inputs\":{InputsCount},\"biases\":{BiasesCount},\"computational\":{ComputationalCount},\"outputs\":{OutputsCount}}}");
            return output.ToString();
        }
    }
}