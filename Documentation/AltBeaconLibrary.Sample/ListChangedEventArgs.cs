using System;

namespace AltBeaconLibrary.Sample
{
    using AltBeaconLibrary.ContractAndroid;

    public class ListChangedEventArgs : EventArgs
	{
		public System.Collections.Generic.List<SharedBeacon> Data { get; protected set; }

		public ListChangedEventArgs(System.Collections.Generic.List<SharedBeacon> data)
		{
			Data = data;
		}
	}
}
