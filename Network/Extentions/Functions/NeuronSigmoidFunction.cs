using System;
using Network.Extentions.Templates;

namespace Network.Extentions.Functions
{
    public class NeuronSigmoidFunction : NeuronFunction
    {
        public override double ForwardFunction(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }

        public override double BackwardFunction(double value)
        {
            return value * (1 - value);
        }
    }
}
