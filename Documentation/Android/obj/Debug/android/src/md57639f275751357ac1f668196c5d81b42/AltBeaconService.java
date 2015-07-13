package md57639f275751357ac1f668196c5d81b42;


public class AltBeaconService
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("AltBeaconLibrary.Sample.Droid.Services.AltBeaconService, AltBeaconLibrary.Sample.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AltBeaconService.class, __md_methods);
	}


	public AltBeaconService () throws java.lang.Throwable
	{
		super ();
		if (getClass () == AltBeaconService.class)
			mono.android.TypeManager.Activate ("AltBeaconLibrary.Sample.Droid.Services.AltBeaconService, AltBeaconLibrary.Sample.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
