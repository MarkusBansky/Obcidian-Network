using System;
using System.Collections;

namespace NeuralComponents.Base
{
    public class Neuron
    {
        public readonly string Guid;
        public double Value;

        public Neuron()
        {
            Guid = System.Guid.NewGuid().ToString();
        }
    }
}
