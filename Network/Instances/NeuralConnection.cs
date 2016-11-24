using Network.Base.Connections;

namespace Network.Instances
{
    public class NeuralConnection : ConnectionBase
    {
        public NeuralConnection(int previous, int next) : base(previous, next)
        {
        }
    }
}
