using System;

namespace ObcidiaNetwork.Base
{
    public class Sigmoid
    {
        public static double Output (double value)
        {
            //return 1.0f / (float)(1.0f + Math.Exp (-value));
            return value < -45.0 ? 0.0 : value > 45.0 ? 1.0 : 1.0 / (1.0 + Math.Exp (-value));
        }

        public static double Derivative (double value)
        {
            return value * (1 - value);
        }
    }
}