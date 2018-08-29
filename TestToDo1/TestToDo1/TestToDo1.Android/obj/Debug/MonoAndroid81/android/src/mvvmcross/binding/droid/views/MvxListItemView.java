package mvvmcross.binding.droid.views;


public class MvxListItemView
	extends md5e2d47b0f76577a28cbe1af6585b33969.MvxBaseListItemView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MvvmCross.Binding.Droid.Views.MvxListItemView, MvvmCross.Binding.Droid", MvxListItemView.class, __md_methods);
	}


	public MvxListItemView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == MvxListItemView.class)
			mono.android.TypeManager.Activate ("MvvmCross.Binding.Droid.Views.MvxListItemView, MvvmCross.Binding.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public MvxListItemView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == MvxListItemView.class)
			mono.android.TypeManager.Activate ("MvvmCross.Binding.Droid.Views.MvxListItemView, MvvmCross.Binding.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public MvxListItemView (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == MvxListItemView.class)
			mono.android.TypeManager.Activate ("MvvmCross.Binding.Droid.Views.MvxListItemView, MvvmCross.Binding.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public MvxListItemView (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == MvxListItemView.class)
			mono.android.TypeManager.Activate ("MvvmCross.Binding.Droid.Views.MvxListItemView, MvvmCross.Binding.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}

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
