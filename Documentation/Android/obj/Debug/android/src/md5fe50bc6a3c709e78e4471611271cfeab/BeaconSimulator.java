package md5fe50bc6a3c709e78e4471611271cfeab;


public class BeaconSimulator
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		org.altbeacon.beacon.simulator.BeaconSimulator
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_getBeacons:()Ljava/util/List;:GetGetBeaconsHandler:AltBeaconOrg.BoundBeacon.Simulator.IBeaconSimulatorInvoker, AndroidAltBeaconLibrary\n" +
			"";
		mono.android.Runtime.register ("AltBeaconLibrary.Sample.Droid.BeaconSimulator, AltBeaconLibrary.Sample.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BeaconSimulator.class, __md_methods);
	}


	public BeaconSimulator () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BeaconSimulator.class)
			mono.android.TypeManager.Activate ("AltBeaconLibrary.Sample.Droid.BeaconSimulator, AltBeaconLibrary.Sample.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public java.util.List getBeacons ()
	{
		return n_getBeacons ();
	}

	private native java.util.List n_getBeacons ();

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
