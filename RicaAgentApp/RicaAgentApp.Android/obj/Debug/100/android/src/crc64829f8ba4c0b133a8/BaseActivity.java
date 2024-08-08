package crc64829f8ba4c0b133a8;


public class BaseActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("RicaAgentApp.Droid.Common.BaseActivity, RicaAgentApp.Android", BaseActivity.class, __md_methods);
	}


	public BaseActivity ()
	{
		super ();
		if (getClass () == BaseActivity.class) {
			mono.android.TypeManager.Activate ("RicaAgentApp.Droid.Common.BaseActivity, RicaAgentApp.Android", "", this, new java.lang.Object[] {  });
		}
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
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
