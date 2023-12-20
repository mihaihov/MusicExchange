package mono.com.triggertrap.seekarc;


public class SeekArc_OnSeekArcChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.triggertrap.seekarc.SeekArc.OnSeekArcChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onProgressChanged:(Lcom/triggertrap/seekarc/SeekArc;IZ)V:GetOnProgressChanged_Lcom_triggertrap_seekarc_SeekArc_IZHandler:Com.Triggertrap.Seekarc.SeekArc/IOnSeekArcChangeListenerInvoker, ThirdPartyLibraries\n" +
			"n_onStartTrackingTouch:(Lcom/triggertrap/seekarc/SeekArc;)V:GetOnStartTrackingTouch_Lcom_triggertrap_seekarc_SeekArc_Handler:Com.Triggertrap.Seekarc.SeekArc/IOnSeekArcChangeListenerInvoker, ThirdPartyLibraries\n" +
			"n_onStopTrackingTouch:(Lcom/triggertrap/seekarc/SeekArc;)V:GetOnStopTrackingTouch_Lcom_triggertrap_seekarc_SeekArc_Handler:Com.Triggertrap.Seekarc.SeekArc/IOnSeekArcChangeListenerInvoker, ThirdPartyLibraries\n" +
			"";
		mono.android.Runtime.register ("Com.Triggertrap.Seekarc.SeekArc+IOnSeekArcChangeListenerImplementor, ThirdPartyLibraries, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SeekArc_OnSeekArcChangeListenerImplementor.class, __md_methods);
	}


	public SeekArc_OnSeekArcChangeListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SeekArc_OnSeekArcChangeListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Triggertrap.Seekarc.SeekArc+IOnSeekArcChangeListenerImplementor, ThirdPartyLibraries, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onProgressChanged (com.triggertrap.seekarc.SeekArc p0, int p1, boolean p2)
	{
		n_onProgressChanged (p0, p1, p2);
	}

	private native void n_onProgressChanged (com.triggertrap.seekarc.SeekArc p0, int p1, boolean p2);


	public void onStartTrackingTouch (com.triggertrap.seekarc.SeekArc p0)
	{
		n_onStartTrackingTouch (p0);
	}

	private native void n_onStartTrackingTouch (com.triggertrap.seekarc.SeekArc p0);


	public void onStopTrackingTouch (com.triggertrap.seekarc.SeekArc p0)
	{
		n_onStopTrackingTouch (p0);
	}

	private native void n_onStopTrackingTouch (com.triggertrap.seekarc.SeekArc p0);

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
