using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltBeaconLibrary.Measure1.DataContract
{
    public class MeasurePoint
    {
        /// <summary>
        /// MAC address of the access point
        /// </summary>      
        public string Bssid { get; set; }

        /// <summary>
        /// Level bzw. Signal strength during the measurement
        /// </summary>       
        public double RssiLevel { get; set; }

        /// <summary>
        /// Wi-Fi Name of the access point
        /// </summary>      
        public string Ssid { get; set; }

        /// <summary>
        /// Signal strength
        /// </summary>
        public uint Quality { get; set; }

        /// <summary>
        /// Frequency
        /// </summary>
        public uint CenterFrequency { get; set; }

        /// <summary>
        /// Constant
        /// </summary>
        public double Constant { get; set; }
    }
}
