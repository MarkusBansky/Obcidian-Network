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

using Network.Base.Neurons;
using Network.Enumerations;
using Network.Extentions.Templates;

namespace Network.Items
{
    /// <summary>
    /// Neuron work item.
    /// </summary>
    public class Neuron : NeuronBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Neuron() : base()
        {
            ForwardCalculation = NeuronFunction.GetFunction(NeuronFunctions.Sigmoid).ForwardFunction;
            BackwardCalculation = NeuronFunction.GetFunction(NeuronFunctions.Sigmoid).BackwardFunction;
        }

        /// <summary>
        /// Constructor with type.
        /// </summary>
        public Neuron(NeuronFunctions function) : base()
        {
            ForwardCalculation = NeuronFunction.GetFunction(function).ForwardFunction;
            BackwardCalculation = NeuronFunction.GetFunction(function).BackwardFunction;
        }

        /// <summary>
        /// Invokes forward propagation delegate function.
        /// </summary>
        /// <returns></returns>
        public double Invoke()
        {
            return ForwardCalculation.Invoke(InputValue);
        }

        /// <summary>
        /// Invokes backward propagation delegate function.
        /// </summary>
        /// <returns></returns>
        public double Revoke()
        {
            return BackwardCalculation.Invoke(InputValue);
        }
    }
}