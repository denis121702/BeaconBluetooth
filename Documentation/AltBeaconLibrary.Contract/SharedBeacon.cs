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
        public int RssiLevel { get; set; }         

        /// <summary>
        /// RssiLevel
        /// </summary>
        public uint CenterFrequency { get; set; }                 
    } 
}
