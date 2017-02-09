using System.Collections.Generic;

namespace ObsidiaDebugger.Helpers
{
    internal class Neuron
    {
        public float InputValue;
        public float OutputValue;
        public string Id;
    }

    internal class Connection
    {
        public int FromIndex;
        public int ToIndex;
        public float Weight;
    }

    class ResponseStructure
    {
        public List<Neuron> Neurons = new List<Neuron>();
        public List<Connection> Connections = new List<Connection>();

        public int InputsCount;
        public int BiasesCount;
        public int ComputationalCount;
        public int OutputsCount;
    }
}