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
using Network.Base;
using Network.Extentions.Interfaces;
using Network.Items;

namespace Network.Extentions.Templates
{
    public class NetworkNeuronsCollection : INetworkNeuronsCollection
    {
        public int InputNeuronsCount;

        public int OutputNeuronsCount;

        public int ComputationalNeuronsCount => (NetworkNeurons.Count - (OutputNeuronsCount + InputNeuronsCount));

        public List<NetworkLayer> Layers;

        public NetworkNeuronsCollection(int inputs, int outputs)
        {
            NetworkNeurons = new List<NeuronBase>();
            InputNeuronsCount = 0;
            OutputNeuronsCount = 0;
        }

        public NeuronBase GetInputNeuron(int index)
        {
            if (index >= InputNeuronsCount || index < 0)
                throw new IndexOutOfRangeException();

            return NetworkNeurons[index];
        }

        public NeuronBase GetOutputNeuron(int index)
        {
            if (index >= InputNeuronsCount + OutputNeuronsCount || index < InputNeuronsCount)
                throw new IndexOutOfRangeException();

            return NetworkNeurons[index];
        }

        public NeuronBase GetComputationalNeuron(int index)
        {
            if (index >= NetworkNeurons.Count || index < InputNeuronsCount + OutputNeuronsCount)
                throw new IndexOutOfRangeException();

            return NetworkNeurons[index];
        }

        public NeuronBase this[int index]
        {
            get { return NetworkNeurons[index]; }
            set { NetworkNeurons[index] = value; }
        }
    }
}
