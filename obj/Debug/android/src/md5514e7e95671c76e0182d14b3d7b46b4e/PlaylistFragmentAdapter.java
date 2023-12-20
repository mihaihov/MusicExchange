package md5514e7e95671c76e0182d14b3d7b46b4e;


public class PlaylistFragmentAdapter
	extends android.support.v7.widget.RecyclerView.Adapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getItemCount:()I:GetGetItemCountHandler\n" +
			"n_onBindViewHolder:(Landroid/support/v7/widget/RecyclerView$ViewHolder;I)V:GetOnBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_IHandler\n" +
			"n_onCreateViewHolder:(Landroid/view/ViewGroup;I)Landroid/support/v7/widget/RecyclerView$ViewHolder;:GetOnCreateViewHolder_Landroid_view_ViewGroup_IHandler\n" +
			"";
		mono.android.Runtime.register ("MusicExchance.PlaylistFragmentAdapter, MusicExchance, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PlaylistFragmentAdapter.class, __md_methods);
	}


	public PlaylistFragmentAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PlaylistFragmentAdapter.class)
			mono.android.TypeManager.Activate ("MusicExchance.PlaylistFragmentAdapter, MusicExchance, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public PlaylistFragmentAdapter (android.content.Context p0, android.support.v4.app.FragmentActivity p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == PlaylistFragmentAdapter.class)
			mono.android.TypeManager.Activate ("MusicExchance.PlaylistFragmentAdapter, MusicExchance, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Support.V4.App.FragmentActivity, Xamarin.Android.Support.Fragment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0, p1 });
	}


	public int getItemCount ()
	{
		return n_getItemCount ();
	}

	private native int n_getItemCount ();


	public void onBindViewHolder (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1)
	{
		n_onBindViewHolder (p0, p1);
	}

	private native void n_onBindViewHolder (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1);


	public android.support.v7.widget.RecyclerView.ViewHolder onCreateViewHolder (android.view.ViewGroup p0, int p1)
	{
		return n_onCreateViewHolder (p0, p1);
	}

	private native android.support.v7.widget.RecyclerView.ViewHolder n_onCreateViewHolder (android.view.ViewGroup p0, int p1);

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
