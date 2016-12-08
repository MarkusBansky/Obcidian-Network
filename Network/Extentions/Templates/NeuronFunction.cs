using System;
using Network.Enumerations;
using Network.Extentions.Functions;
using Network.Extentions.Interfaces;

namespace Network.Extentions.Templates
{
    public class NeuronFunction : INeuronFunction
    {
        public virtual double ForwardFunction(double value)
        {
            throw new NotImplementedException();
        }

        public virtual double BackwardFunction(double value)
        {
            throw new NotImplementedException();
        }

        public static NeuronFunction GetFunction(NeuronFunctions function)
        {
            if (function == NeuronFunctions.Sigmoid)
                return new NeuronSigmoidFunction();

            return null;
        }
    }
}
