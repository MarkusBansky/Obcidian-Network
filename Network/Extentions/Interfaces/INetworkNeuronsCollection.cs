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

namespace Network.Extentions.Interfaces
{
    // ReSharper disable once InconsistentNaming
    public interface INetworkNeuronsCollection
    {
        /// <summary>
        /// Gets input neuron object.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        NeuronBase GetInputNeuron(int index);

        /// <summary>
        /// Gets output neuron object.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        NeuronBase GetOutputNeuron(int index);

        /// <summary>
        /// Gets computational neuron object.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        NeuronBase GetComputationalNeuron(int index);

        #region Add methods for input neurons
        /// <summary>
        /// Add default input neuron.
        /// </summary>
        /// <returns></returns>
        NeuronBase AddInputNeuron();

        /// <summary>
        /// Add custom input neuron.
        /// </summary>
        /// <param name="neuron"></param>
        /// <returns></returns>
        NeuronBase AddInputNeuron(NeuronBase neuron);
        #endregion

        #region Add methods for output neurons
        /// <summary>
        /// Add default output neuron.
        /// </summary>
        /// <returns></returns>
        NeuronBase AddOutputNeuron();

        /// <summary>
        /// Add custom output neuron.
        /// </summary>
        /// <param name="neuron"></param>
        /// <returns></returns>
        NeuronBase AddOutputNeuron(NeuronBase neuron);
        #endregion

        NeuronBase AddComputationalNeuron();

        NeuronBase AddComputationalNeuron(NeuronBase neuron);
    }
}
