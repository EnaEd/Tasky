package mono.com.xenione.libs.swipemaker;


public class SwipeLayout_OnTranslateChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.xenione.libs.swipemaker.SwipeLayout.OnTranslateChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTranslateChange:(FIF)V:GetOnTranslateChange_FIFHandler:Com.Xenione.Libs.Swipemaker.SwipeLayout/IOnTranslateChangeListenerInvoker, SwipeMaker\n" +
			"";
		mono.android.Runtime.register ("Com.Xenione.Libs.Swipemaker.SwipeLayout+IOnTranslateChangeListenerImplementor, SwipeMaker", SwipeLayout_OnTranslateChangeListenerImplementor.class, __md_methods);
	}


	public SwipeLayout_OnTranslateChangeListenerImplementor ()
	{
		super ();
		if (getClass () == SwipeLayout_OnTranslateChangeListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Xenione.Libs.Swipemaker.SwipeLayout+IOnTranslateChangeListenerImplementor, SwipeMaker", "", this, new java.lang.Object[] {  });
	}


	public void onTranslateChange (float p0, int p1, float p2)
	{
		n_onTranslateChange (p0, p1, p2);
	}

	private native void n_onTranslateChange (float p0, int p1, float p2);

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
