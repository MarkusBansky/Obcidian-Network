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
using System.Collections.Generic;
using Network.Base.Neurons;
using Network.Extentions.Interfaces;

namespace Network.Extentions.Templates
{
    public class NetworkNeuronsCollection : INetworkNeuronsCollection
    {
        protected int InputNeurons;

        protected int OutputNeurons;

        protected List<NeuronBase> NetworkNeurons;

        public NeuronBase GetInputNeuron(int index)
        {
            if (index >= InputNeurons || index < 0)
                throw new IndexOutOfRangeException();

            return NetworkNeurons[index];
        }

        public NeuronBase GetOutputNeuron(int index)
        {
            if (index >= InputNeurons + OutputNeurons || index < InputNeurons)
                throw new IndexOutOfRangeException();

            return NetworkNeurons[index];
        }

        public NeuronBase GetComputationalNeuron(int index)
        {
            if (index >= NetworkNeurons.Count || index < InputNeurons + OutputNeurons)
                throw new IndexOutOfRangeException();

            return NetworkNeurons[index];
        }

        public NeuronBase AddInputNeuron()
        {
            throw new NotImplementedException();
        }

        public NeuronBase AddInputNeuron(NeuronBase neuron)
        {
            throw new NotImplementedException();
        }

        public NeuronBase AddOutputNeuron()
        {
            throw new NotImplementedException();
        }

        public NeuronBase AddOutputNeuron(NeuronBase neuron)
        {
            throw new NotImplementedException();
        }
    }
}
