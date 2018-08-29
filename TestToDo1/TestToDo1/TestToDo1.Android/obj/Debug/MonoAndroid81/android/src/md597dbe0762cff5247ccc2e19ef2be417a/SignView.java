package md597dbe0762cff5247ccc2e19ef2be417a;


public class SignView
	extends md5574c1700f54c1af9f21719cde6491b73.MvxActivity_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("TestToDo1.Droid.Views.SignView, TestToDo1.Android", SignView.class, __md_methods);
	}


	public SignView ()
	{
		super ();
		if (getClass () == SignView.class)
			mono.android.TypeManager.Activate ("TestToDo1.Droid.Views.SignView, TestToDo1.Android", "", this, new java.lang.Object[] {  });
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
