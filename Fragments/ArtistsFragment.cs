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
using RecyclerView = Android.Support.V7.Widget.RecyclerView;
using Android.Support.V7.Widget;

namespace MusicExchance
{
    class ArtistsFragment:Fragment
    {
        private RecyclerView myRecyclerView;
        private RecyclerView.LayoutManager myLayoutManager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ArtistsFragmentLayout, null, false);
            myRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.IDRVARTISTS);
            myLayoutManager = new Android.Support.V7.Widget.GridLayoutManager(this.Context, 2,
                Android.Support.V7.Widget.GridLayoutManager.Vertical, false);

            if(AppManager.Instance.artistList.Count>=0)
            {
                var g = AppManager.Instance.songsList.GroupBy(v => v.Artist).Select(u => u.ToList()).ToList();
                foreach(List<Song> s in g)
                {
                    Artist a = new Artist(s);
                    AppManager.Instance.artistList.Add(a);
                }
            }

            ArtistFrameAdapter afa = new ArtistFrameAdapter(this.Context);
            myRecyclerView.SetLayoutManager(myLayoutManager);
            myRecyclerView.SetAdapter(afa);

            return view;
        }
    }

    public class Artist
    {
        private string artistName;
        private byte[] artistArtwork;
        private List<Song> artistSongs;

        public string Name { get { return artistName; } }
        public byte[] Artwork { get { return artistArtwork; } }
        public List<Song> Songs { get { return artistSongs; } }

        public Artist(List<Song> l)
        {
            artistSongs = new List<Song>();
            artistSongs.AddRange(l);
            for(int i=0;i<artistSongs.Count;i++)
            {
                if(!string.IsNullOrEmpty(artistSongs[i].Artist))
                {
                    artistName = artistSongs[i].Artist;
                    break;
                }
            }

            for (int i = 0; i < artistSongs.Count; i++)
            {
                if (artistSongs[i].Artwork != null)
                {
                    artistArtwork = artistSongs[i].Artwork;
                    break;
                }
            }
        }
    }


    public class ViewHolderArtist : RecyclerView.ViewHolder
    {
        private FrameLayout artistArtwork;
        private TextView artistName;

        public ViewHolderArtist(View item): base(item)
        {
            artistArtwork = item.FindViewById<FrameLayout>(Resource.Id.IDFLALBUM1);
            artistName = item.FindViewById<TextView>(Resource.Id.IDTVALBUMTITLECOL1);
        }

        public void OnBind(Artist a, Context c)
        {
            //artistArtwork.SetBackgroundResource(Resource.Drawable.MusicExchange300x300);
            artistArtwork.SetBackgroundColor(Color.Red);
            if (a.Artwork != null)
                Utilities.Instance.SetFrameLayoutAsync(a.Artwork, artistArtwork, 165, 165);
            artistName.Text = a.Name;
            Utilities.Instance.ChangeFont(artistName, c, "cuyabra.otf", TypefaceStyle.Normal);
        }
    }

    public class ArtistFrameAdapter : RecyclerView.Adapter
    {
        private Context myContext;
        private LayoutInflater inflater;

        public ArtistFrameAdapter(Context c)
        {
            myContext = c;
            inflater = LayoutInflater.From(myContext);
        }


        public override int ItemCount
        {
            get
            {
                return AppManager.Instance.artistList.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolderArtist vh = holder as ViewHolderArtist;
            vh.OnBind(AppManager.Instance.artistList[position], myContext);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View row = inflater.Inflate(Resource.Layout.AlbumsFragmentListViewRow, parent, false);
            ViewHolderArtist vh = new ViewHolderArtist(row);
            return vh;
        }
    }
}