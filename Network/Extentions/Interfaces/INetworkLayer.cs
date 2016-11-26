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

using Network.Base;

namespace Network.Extentions.Interfaces
{
    public interface INetworkLayer
    {
        /// <summary>
        /// Perform forward propagation for this layer.
        /// </summary>
        void ForwardCalculation();

        /// <summary>
        /// Returns an array of neurons Value.
        /// </summary>
        /// <returns></returns>
        double[] GetValues();

        /// <summary>
        /// Returns an array of neurons InputValue.
        /// </summary>
        /// <returns></returns>
        double[] GetInputValues();
    }
}