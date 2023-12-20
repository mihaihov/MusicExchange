package md5514e7e95671c76e0182d14b3d7b46b4e;


public class BottomSheetEventHandler
	extends android.support.design.widget.BottomSheetBehavior.BottomSheetCallback
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSlide:(Landroid/view/View;F)V:GetOnSlide_Landroid_view_View_FHandler\n" +
			"n_onStateChanged:(Landroid/view/View;I)V:GetOnStateChanged_Landroid_view_View_IHandler\n" +
			"";
		mono.android.Runtime.register ("MusicExchance.BottomSheetEventHandler, MusicExchance, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BottomSheetEventHandler.class, __md_methods);
	}


	public BottomSheetEventHandler () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BottomSheetEventHandler.class)
			mono.android.TypeManager.Activate ("MusicExchance.BottomSheetEventHandler, MusicExchance, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public BottomSheetEventHandler (md5514e7e95671c76e0182d14b3d7b46b4e.CurrentSongFragment p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == BottomSheetEventHandler.class)
			mono.android.TypeManager.Activate ("MusicExchance.BottomSheetEventHandler, MusicExchance, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "MusicExchance.CurrentSongFragment, MusicExchance, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void onSlide (android.view.View p0, float p1)
	{
		n_onSlide (p0, p1);
	}

	private native void n_onSlide (android.view.View p0, float p1);


	public void onStateChanged (android.view.View p0, int p1)
	{
		n_onStateChanged (p0, p1);
	}

	private native void n_onStateChanged (android.view.View p0, int p1);

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
