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
using System.Collections.Generic;
using Network.Extentions.Templates;
using Network.Items;

namespace Network.Base.Network
{
    /// <summary>
    /// Base network class.
    /// </summary>
    public class NetworkBase
    {
        /// <summary>
        /// Custom neurons collection.
        /// </summary>
        protected NetworkNeuronsCollection NeuronsCollection;

        /// <summary>
        /// Neurons connections mappings.
        /// </summary>
        protected List<NeuralConnection> NeuralConnections;

        /// <summary>
        /// [] overloaded method.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Neuron this[int index]
        {
            get { return NeuronsCollection[index] as Neuron; }
            set { NeuronsCollection[index] = value; }
        }
    }
}
