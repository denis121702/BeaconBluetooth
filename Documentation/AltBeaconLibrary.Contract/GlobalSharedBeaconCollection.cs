using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AltBeaconLibrary.ContractAndroid
{
    public class GlobalSharedBeaconCollection
    {
        public static List<SharedBeacon> currentAccessPoint = new List<SharedBeacon>();

        public static List<SharedBeacon> currentMeasurePoint = new List<SharedBeacon>();

        public static float PosX;
        public static float PosY;

        public static ObservableCollection<SharedBeacon> observableItems = new ObservableCollection<SharedBeacon>();


        public static List<SharedBeacon> accessPointListServer = new List<SharedBeacon>();

        //static GlobalSharedBeaconCollection current;
        //public GlobalSharedBeaconCollection()
        //{
        //    SharedBeaconCollection = new List<SharedBeacon>();
        //}

        //public static App Current
        //{
        //    get
        //    {
        //        return current ?? (current = new GlobalSharedBeaconCollection());
        //    }
        //}
    }
}