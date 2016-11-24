using Network.Base.Neurons;

namespace Network.Instances
{
    public class Neuron : NeuronBase
    {
        public Neuron() : base()
        {
            ForwardCalculation = __sigmoid;
            BackwardCalculation = __sigmoid_derivative;
        }

        public double Invoke()
        {
            return ForwardCalculation.Invoke(InputValue);
        }

        public double Revoke()
        {
            return BackwardCalculation.Invoke(InputValue);
        }
    }
}
