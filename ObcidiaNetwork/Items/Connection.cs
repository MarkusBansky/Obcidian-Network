using ObcidiaNetwork.Base;
using ObcidiaNetwork.Helpers;

namespace ObcidiaNetwork.Items
{
    internal class Connection : ConnectionBase
    {
        public Connection(int from, int to)
        {
            NeuronFromId = from;
            NeuronToId = to;

            WeightValue = (float)FixedRandom.RandomDouble();
        }

        public Connection(int from, int to, float weight)
        {
            NeuronFromId = from;
            NeuronToId = to;

            WeightValue = weight;
        }
    }
}