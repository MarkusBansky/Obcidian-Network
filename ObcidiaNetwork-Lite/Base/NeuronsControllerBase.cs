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

        public NeuronsControllerBase (int inputs, int computational, int outputs)
        {
            NeuronsContainer = new NeuronBase[inputs + computational + computational + outputs];
            ConnectionsContainer = new List<ConnectionBase>();

            InputsCount = inputs;
            BiasesCount = computational;
            ComputationalCount = computational;
            OutputsCount = outputs;
            
            for (int i = 0; i < NeuronsContainer.Length; i++)
            {
                NeuronsContainer[i] = new NeuronBase ();
            }
        }

        protected NeuronBase GetInputNeuron(int index)
        {
            if (index >= 0 && index < InputsCount)
            {
                return NeuronsContainer[index];
            }
            throw new IndexOutOfRangeException("Index is out of 'Inputs' bounds. Consider using a value > 0 and < InputsCount.");
        }

        protected NeuronBase GetBiasNeuron (int index)
        {
            index -= InputsCount;

            if (index >= 0 && index < BiasesCount)
            {
                return NeuronsContainer[index];
            }
            throw new IndexOutOfRangeException ("Index is out of 'Biases' bounds. Consider using a value > 0 and < BiasesCount.");
        }

        protected NeuronBase GetComputationalNeuron (int index)
        {
            index -= InputsCount + BiasesCount;

            if (index >= 0 && index < ComputationalCount)
            {
                return NeuronsContainer[index];
            }
            throw new IndexOutOfRangeException ("Index is out of 'Computationals' bounds. Consider using a value > 0 and < ComputationalCount.");
        }

        protected NeuronBase GetOutputNeuron (int index)
        {
            index -= InputsCount + BiasesCount + ComputationalCount;

            if (index >= 0 && index < OutputsCount)
            {
                return NeuronsContainer[index];
            }
            throw new IndexOutOfRangeException ("Index is out of 'Outputs' bounds. Consider using a value > 0 and < OutputsCount.");
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
            output.Append("{\n");

            output.Append ("\t\"neurons\":[\n");
            foreach (NeuronBase neuron in NeuronsContainer)
                output.Append("\t\t" + neuron + ",\n");
            output.Remove (output.Length - 2, 2);

            output.Append ("\n\t],\n\t\"connections\":[\n");
            foreach (ConnectionBase connection in ConnectionsContainer)
                output.Append ("\t\t" + connection + ",\n");
            output.Remove(output.Length - 2, 2);
            output.Append ("\n\t]\n}");
            return output.ToString();
        }
    }
}