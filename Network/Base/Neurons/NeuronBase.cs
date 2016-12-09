#region Licence
//  /*************************************************************************
//  * 
//  * AFGOR CONFIDENTIAL
//  * __________________
//  * 
//  *  [2015] - [2016] Markus Benovsky, Afgor Entertainment
//  *  All Rights Reserved.
//  * 
//  * NOTICE:  All information contained herein is, and remains
//  * the property of Afgor Entertainment and its suppliers,
//  * if any.  The intellectual and technical concepts contained
//  * herein are proprietary to Afgor Entertainment
//  * and its suppliers and may be covered by UA and Foreign Patents,
//  * patents in process, and are protected by trade secret or copyright law.
//  * Dissemination of this information or reproduction of this material
//  * is strictly forbidden unless prior written permission is obtained
//  * from Afgor Entertainment.
//  * 
//  * Code written by Markus Benovsky for ObsidiaNetwork project in NauralNetworks
//  * 2016 11 25
//  */
#endregion
using System;
using Network.Items;

namespace Network.Base.Neurons
{
    /// <summary>
    /// Basic neuron class.
    /// </summary>
    public class NeuronBase : IEquatable<NeuronBase>, ICloneable
    {
        /// <summary>
        /// Unique Identificator.
        /// </summary>
        public readonly string Guid;

        /// <summary>
        /// Neuron value after sigmoid calculations.
        /// </summary>
        public double Value;

        /// <summary>
        /// Input value calculated as sum of connections * multipliers.
        /// </summary>
        public double InputValue = 0;

        /// <summary>
        /// Calculation function delegate.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public delegate double Calculation(double value);

        /// <summary>
        /// Forward calculation function.
        /// </summary>
        public Calculation ForwardCalculation;

        /// <summary>
        /// Backward calculation function.
        /// </summary>
        public Calculation BackwardCalculation;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NeuronBase()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Implemented IEquatable interface for IEnumerable collections.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NeuronBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || string.Equals(Guid, other.Guid);
        }

        /// <summary>
        /// Overloaded equals method.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((NeuronBase) obj);
        }

        /// <summary>
        /// Overloaded hash code method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Guid?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// Overloaded ICloneable interface.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            NeuronBase neuron = new Neuron();
            neuron.BackwardCalculation = BackwardCalculation;
            neuron.ForwardCalculation = ForwardCalculation;
            neuron.InputValue = InputValue;
            neuron.Value = Value;

            return neuron;
        }

        /// <summary>
        /// Overloaded == operator.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(NeuronBase left, NeuronBase right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Overloaded != operator.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(NeuronBase left, NeuronBase right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Overloaded string conversion.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Guid + ":" + InputValue + ":" + Value;
        }
    }
}