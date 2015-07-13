package md5fe50bc6a3c709e78e4471611271cfeab;


public class MainActivity
	extends md5530bd51e982e6e7b340b73e88efe666e.FormsApplicationActivity
	implements
		mono.android.IGCUserPeer,
		org.altbeacon.beacon.BeaconConsumer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_getApplicationContext:()Landroid/content/Context;:GetGetApplicationContextHandler:AltBeaconOrg.BoundBeacon.IBeaconConsumerInvoker, AndroidAltBeaconLibrary\n" +
			"n_bindService:(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z:GetBindService_Landroid_content_Intent_Landroid_content_ServiceConnection_IHandler:AltBeaconOrg.BoundBeacon.IBeaconConsumerInvoker, AndroidAltBeaconLibrary\n" +
			"n_onBeaconServiceConnect:()V:GetOnBeaconServiceConnectHandler:AltBeaconOrg.BoundBeacon.IBeaconConsumerInvoker, AndroidAltBeaconLibrary\n" +
			"n_unbindService:(Landroid/content/ServiceConnection;)V:GetUnbindService_Landroid_content_ServiceConnection_Handler:AltBeaconOrg.BoundBeacon.IBeaconConsumerInvoker, AndroidAltBeaconLibrary\n" +
			"";
		mono.android.Runtime.register ("AltBeaconLibrary.Sample.Droid.MainActivity, AltBeaconLibrary.Sample.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity.class, __md_methods);
	}


	public MainActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("AltBeaconLibrary.Sample.Droid.MainActivity, AltBeaconLibrary.Sample.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public android.content.Context getApplicationContext ()
	{
		return n_getApplicationContext ();
	}

	private native android.content.Context n_getApplicationContext ();


	public boolean bindService (android.content.Intent p0, android.content.ServiceConnection p1, int p2)
	{
		return n_bindService (p0, p1, p2);
	}

	private native boolean n_bindService (android.content.Intent p0, android.content.ServiceConnection p1, int p2);


	public void onBeaconServiceConnect ()
	{
		n_onBeaconServiceConnect ();
	}

	private native void n_onBeaconServiceConnect ();


	public void unbindService (android.content.ServiceConnection p0)
	{
		n_unbindService (p0);
	}

	private native void n_unbindService (android.content.ServiceConnection p0);

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
