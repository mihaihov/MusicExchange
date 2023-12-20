using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Graphics;
using Android.Views.Animations;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Fragment = Android.Support.V4.App.Fragment;
using RecyclerView = Android.Support.V7.Widget.RecyclerView;
using FragmentActivity = Android.Support.V4.App.FragmentActivity;
using Android.Support.V7.Widget;
using Android.Support.V4.App;

namespace MusicExchance
{
    class PlaylistsFragment : Fragment
	{
        private static PlaylistsFragment _instance = null;
        public static PlaylistsFragment Instance
        {
            get
            {
                if (_instance == null) _instance = new PlaylistsFragment();
                return _instance;
            }
        }



		private RecyclerView myRecyclerView;
		private RecyclerView.LayoutManager myLayoutManager;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.PlaylistFragmentLayout, null, false);
            if (AppManager.Instance.playlistList.Count <= 0)
            {
                Playlist p = new Playlist("Default", AppManager.Instance.songsList);
                AppManager.Instance.playlistList.Add(p);

                Playlist q = new Playlist("Nigga", 10);
                AppManager.Instance.playlistList.Add(q);

                Playlist q1 = new Playlist("Relax", 23);
                AppManager.Instance.playlistList.Add(q1);

                Playlist q2 = new Playlist("Night", 45);
                AppManager.Instance.playlistList.Add(q2);

                Playlist q3 = new Playlist("In the car", 13);
                AppManager.Instance.playlistList.Add(q3);

                Playlist q4 = new Playlist("Before sleep", 22);
                AppManager.Instance.playlistList.Add(q4);
            }

            myRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.IDRVPLAYLISTS);
            myLayoutManager = new Android.Support.V7.Widget.GridLayoutManager(this.Context, 2,
                Android.Support.V7.Widget.GridLayoutManager.Vertical, false);

            
            myRecyclerView.SetLayoutManager(myLayoutManager);
            PlaylistFragmentAdapter pfa = new PlaylistFragmentAdapter(this.Context,this.Activity);
            myRecyclerView.SetAdapter(pfa);

            return view;
        }

    }

    public class Playlist
    {
        string playlistTitle;
        byte[] playlistArtwork;
        int numberOfSongs;
		List<int> playlistSongs;

        public int Count { get { return numberOfSongs; } }
        public string Title { get { return playlistTitle; } }
        public byte[] Artwork { get { return playlistArtwork; } }
		public List<int> PlaylistSongs { get { return playlistSongs; } }

        public Playlist(string t)
        {
            playlistArtwork = null;
            playlistTitle = t;
            numberOfSongs = 0;
			playlistSongs = new List<int>();
        }

		public Playlist(string t, int s)
		{
			playlistTitle = t;
			numberOfSongs = 1;
			playlistSongs = new List<int>(); playlistSongs.Add(s);
			playlistArtwork = AppManager.Instance.songsList[s].Artwork;
		}

        public Playlist(string t, List<Song> s)
        {
            playlistTitle = t;
            numberOfSongs = s.Count;
            playlistArtwork = s[0].Artwork;
            playlistSongs = new List<int>();
            for (int i = 0; i < numberOfSongs; i++)
                playlistSongs.Add(i);
        }
    }

    public class ViewHolderPlaylists : RecyclerView.ViewHolder
    {
        private FrameLayout playlistArtwork;
        private TextView playlistTitle;
        private TextView playlistData;
        private ImageButton settingsButton;

        private FragmentActivity myFragmentActivity;

        public ViewHolderPlaylists(View item, FragmentActivity fa) : base(item)
        {
            myFragmentActivity = fa;

            playlistArtwork = item.FindViewById<FrameLayout>(Resource.Id.IDFLPLAYLIST1);
            playlistTitle = item.FindViewById<TextView>(Resource.Id.IDTVPLAYLISTCOL1);
            playlistData = item.FindViewById<TextView>(Resource.Id.IDTVDATASPLAYLIST);
            settingsButton = item.FindViewById<ImageButton>(Resource.Id.IDIBEXPANDPLAYLIST1);
        }

        public void OnBind(Playlist p, Context c)
        {
            playlistArtwork.SetBackgroundResource(Resource.Drawable.MusicExchange300x300);
            //playlistArtwork.SetBackgroundColor(Color.Red);
            if (p.Artwork != null)
                Utilities.Instance.SetFrameLayoutAsync(p.Artwork, playlistArtwork, 165, 165);
            playlistTitle.Text = p.Title;

            long s = 0;
            for(int i=0;i<p.PlaylistSongs.Count;i++)
            {
                s += Convert.ToInt64(AppManager.Instance.songsList[p.PlaylistSongs[i]].Length);
            }
            playlistData.Text = p.PlaylistSongs.Count.ToString() + " Song(s) - " + (s/1000/60).ToString()+  " Minutes"; 

            Utilities.Instance.ChangeFont(playlistTitle, c, "cuyabra.otf");
            Utilities.Instance.ChangeFont(playlistData, c, "cuyabra.otf", TypefaceStyle.Normal);
            settingsButton.Click += SettingsButton_Click;
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Android.App.FragmentTransaction ft = myFragmentActivity.FragmentManager.BeginTransaction();
            PlaylistSettingsDialogFragment psdf = new PlaylistSettingsDialogFragment();
            psdf.Show(ft, "PlaylistSettings");
        }
    }


    public class PlaylistFragmentAdapter : RecyclerView.Adapter
    {
        private Context myContext;
        private LayoutInflater inflater;
        private FragmentActivity myFragmentActivity;

        public PlaylistFragmentAdapter(Context c, FragmentActivity fa)
        {
            myContext = c;
            myFragmentActivity = fa;
            inflater = LayoutInflater.From(myContext);
        }

        public override int ItemCount
        {
            get
            {
                return AppManager.Instance.playlistList.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolderPlaylists vh = holder as ViewHolderPlaylists;
            vh.OnBind(AppManager.Instance.playlistList[position], myContext);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View row = inflater.Inflate(Resource.Layout.PlaylistsFragmentListViewRow2, parent, false);

            ViewHolderPlaylists vh = new ViewHolderPlaylists(row, myFragmentActivity);

            return vh;
        }
    }
}