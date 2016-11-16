namespace NeuralComponents.Base.Neurons
{
    public class ComputationalNeuron : Neuron
    {
        public double InputValue = 0;

        public delegate double Calculation(double value);
        public Calculation ForwardCalculation;
        public Calculation BackwardCalculation;

        public ComputationalNeuron()
        {
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
