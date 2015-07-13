using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltBeaconLibrary.ContractAndroid
{
    public class SharedBeacon
    {
        public string Id { get; set; }
        public string Distance { get; set; }

        public float PosX { get; set; }
        public float PosY { get; set; }

        /// <summary>
        /// BlutoothAdress
        /// </summary>
        public string Bssid { get; set; }
        
        /// <summary>
        /// RssiLevel
        /// </summary>
        public string RssiLevel { get; set; }         

        /// <summary>
        /// RssiLevel
        /// </summary>
        public string CenterFrequency { get; set; }                 
    } 
}
