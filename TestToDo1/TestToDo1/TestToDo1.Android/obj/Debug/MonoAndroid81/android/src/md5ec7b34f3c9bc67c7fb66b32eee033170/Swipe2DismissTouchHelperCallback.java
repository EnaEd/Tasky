package md5ec7b34f3c9bc67c7fb66b32eee033170;


public class Swipe2DismissTouchHelperCallback
	extends android.support.v7.widget.helper.ItemTouchHelper.SimpleCallback
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMove:(Landroid/support/v7/widget/RecyclerView;Landroid/support/v7/widget/RecyclerView$ViewHolder;Landroid/support/v7/widget/RecyclerView$ViewHolder;)Z:GetOnMove_Landroid_support_v7_widget_RecyclerView_Landroid_support_v7_widget_RecyclerView_ViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_Handler\n" +
			"n_onSwiped:(Landroid/support/v7/widget/RecyclerView$ViewHolder;I)V:GetOnSwiped_Landroid_support_v7_widget_RecyclerView_ViewHolder_IHandler\n" +
			"";
		mono.android.Runtime.register ("TestToDo1.Droid.Helper.Swipe2DismissTouchHelperCallback, TestToDo1.Android", Swipe2DismissTouchHelperCallback.class, __md_methods);
	}


	public Swipe2DismissTouchHelperCallback (int p0, int p1)
	{
		super (p0, p1);
		if (getClass () == Swipe2DismissTouchHelperCallback.class)
			mono.android.TypeManager.Activate ("TestToDo1.Droid.Helper.Swipe2DismissTouchHelperCallback, TestToDo1.Android", "System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1 });
	}


	public boolean onMove (android.support.v7.widget.RecyclerView p0, android.support.v7.widget.RecyclerView.ViewHolder p1, android.support.v7.widget.RecyclerView.ViewHolder p2)
	{
		return n_onMove (p0, p1, p2);
	}

	private native boolean n_onMove (android.support.v7.widget.RecyclerView p0, android.support.v7.widget.RecyclerView.ViewHolder p1, android.support.v7.widget.RecyclerView.ViewHolder p2);


	public void onSwiped (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1)
	{
		n_onSwiped (p0, p1);
	}

	private native void n_onSwiped (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1);

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
