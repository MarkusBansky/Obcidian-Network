using Network.Helpers;

namespace Network.Base.Connections
{
    public class NeuralConnection
    {
        public readonly int PreviousNeuron;
        public readonly int NextNeuron;

        public double Multiplier;

        public NeuralConnection(int previous, int next)
        {
            PreviousNeuron = previous;
            NextNeuron = next;

            Multiplier = FixedRandom.RandomDouble();
        }
    }
}
