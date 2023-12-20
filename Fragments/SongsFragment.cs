using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using System.Threading;
using System.Threading.Tasks;
using RecyclerView = Android.Support.V7.Widget.RecyclerView;
using Android.Support.V7.Widget;
using System.IO;

namespace MusicExchance
{
    class SongsFragment : Fragment
    {
        private static SongsFragment _instance;
        public static SongsFragment Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SongsFragment();
                return _instance;
            }
        }

		private View view;
        private RecyclerView myRecyclerView;
        private RecyclerView.LayoutManager myLayoutManager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			view = inflater.Inflate(Resource.Layout.SongsFragmentLayout, null, false);

            myRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.IDRVSONGFRAGMENTLAYOUT);
            myLayoutManager = new Android.Support.V7.Widget.LinearLayoutManager(this.Context);
            myRecyclerView.SetLayoutManager(myLayoutManager);

            SongsFragmentAdapter sfa = new SongsFragmentAdapter(this.Context);
            myRecyclerView.SetAdapter(sfa);

            return view;
        }
    }

    public class Song
    {
        private string songTitle;
        private string songArtist;
        private string songLength;
        private byte[] songArtwork;
        private string songPath;
        private string songAlbum;

        public string Title { get { return songTitle; } }
        public string Artist { get { return songArtist; } }
        public string Length { get { return songLength; } }
        public byte[] Artwork { get { return songArtwork; } }
        public string Path { get { return songPath; } }
		public string Album { get { return songAlbum; } }

        public Song(string path)
        {
            songPath = path;
            songTitle = SongDetails.GetSongTitle(path);
            songArtist = SongDetails.GetSongArtist(path);
            songLength = SongDetails.GetSongDuration(path);
            songArtwork = SongDetails.GetSongArtwork(path);
            songAlbum = SongDetails.GetSongAlbum(path);
        }

    }

    public class ViewHolderSongs : RecyclerView.ViewHolder
    {
        public ImageView artWork { get; private set; }
        public TextView artistName { get; private set; }
        public TextView songName { get; private set; }
        public TextView length { get; private set; }
		public bool isSet { get; private set;}


        public ViewHolderSongs(View item) : base(item)
        {
            artWork = item.FindViewById<ImageView>(Resource.Id.IDIVSONGSFRAGMENT);
            artistName = item.FindViewById<TextView>(Resource.Id.IDTVSONGSFRAGMENTARTIST);
            songName = item.FindViewById<TextView>(Resource.Id.IDTVSONGSFRAGMENTTITLE);
            length = item.FindViewById<TextView>(Resource.Id.IDTVSONGSFRAGMENTTIME);
        }

		public void onBind(Song song, Context c)
		{


			//set artwork
			artWork.SetImageResource(Resource.Drawable.MusicExchange100x100);		//placeholder.
			if (song.Artwork != null)
				Utilities.Instance.SetImageViewAsync(song.Artwork, artWork,80,80);

			//set title and artist.
			artistName.Text = song.Artist;
			songName.Text = song.Title;
			if (string.IsNullOrEmpty(song.Artist))
			{
				artistName.Text = "<Unknown>";

			}Utilities.Instance.ChangeFont(artistName, c, "cuyabra.otf", TypefaceStyle.Normal);
			if (string.IsNullOrEmpty(song.Title))
			{
				songName.Text = System.IO.Path.GetFileNameWithoutExtension(song.Path);

			}Utilities.Instance.ChangeFont(songName, c, "cuyabra.otf", TypefaceStyle.Normal);


			//set the length of the song
			long x = Convert.ToInt64(song.Length);
			TimeSpan ts = new TimeSpan(0, (int)((x / 1000)/60), (int)(x % 60));
			length.Text = ts.ToString(@"mm\:ss");
			Utilities.Instance.ChangeFont(length,c, "cuyabra.otf", TypefaceStyle.Normal);
		}
    }


	public class SongsFragmentAdapter : RecyclerView.Adapter
	{
		private Context myContext;
		LayoutInflater inflater;

		public SongsFragmentAdapter(Context c)
		{
			myContext = c;
			inflater = LayoutInflater.From(myContext);
		}

		public override int ItemCount
		{
			get
			{
				return AppManager.Instance.songsList.Count;
			}
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			Song s = AppManager.Instance.songsList[position];
			ViewHolderSongs h = holder as ViewHolderSongs;
			h.onBind(s, myContext);
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View row = inflater.Inflate(Resource.Layout.SongsFragmentListViewRow, parent, false);

			ViewHolderSongs vh = new ViewHolderSongs(row);

			return vh;
		}


	}
}
