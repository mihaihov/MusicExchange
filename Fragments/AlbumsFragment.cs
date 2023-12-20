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
    class AlbumsFragment:Fragment
    {
        private RecyclerView myRecyclerView;
        private RecyclerView.LayoutManager myLayoutManager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AlbumsFragmentLayout, null, false);

            myRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.IDRVALBUMS);
            myLayoutManager = new Android.Support.V7.Widget.GridLayoutManager(this.Context, 2,
                Android.Support.V7.Widget.GridLayoutManager.Vertical, false);

            if (AppManager.Instance.albumList.Count<=0)
            {
                var g = AppManager.Instance.songsList.GroupBy(u => u.Album).Select(grp => grp.ToList()).ToList();
                foreach (List<Song> s in g)
                {
                    Album a = new Album(s);
                    AppManager.Instance.albumList.Add(a);
                } 
            }

            AlbumsFragmentAdapter afa = new AlbumsFragmentAdapter(this.Context);
            myRecyclerView.SetLayoutManager(myLayoutManager);
            myRecyclerView.SetAdapter(afa);

            return view;
        }
    }


    public class Album
    {
        private string albumTitle;
        private string albumArtist;
        private byte[] albumArtwork;
        private List<Song> albumSongs;

        public string Title { get { return albumTitle; } }
        public string Artist { get { return albumArtist; } }
        public byte[] Artwork { get { return albumArtwork; } }
        public List<Song> Songs { get { return albumSongs; } }

        public Album(List<Song> l)
        {
            albumSongs = new List<Song>(); 
            albumSongs.AddRange(l);
            for(int i = 0;i<albumSongs.Count;i++)
            {
                if(albumSongs[i].Artwork != null)
                {
                    albumArtwork = albumSongs[i].Artwork;
                    break;
                }
            }

            for (int i = 0; i < albumSongs.Count; i++)
            {
                if (!string.IsNullOrEmpty(albumSongs[i].Album))
                {
                    albumTitle = albumSongs[i].Album;
                    break;
                }
            }

            for (int i = 0; i < albumSongs.Count; i++)
            {
                if (!string.IsNullOrEmpty(albumSongs[i].Artist))
                {
                    albumArtist = albumSongs[i].Artist;
                    break;
                }
            }
        }
    }



    public class ViewHolderAlbums : RecyclerView.ViewHolder
    {
        private FrameLayout albumArtwork;
        private TextView albumArtist;
        private TextView albumTitle;

        public ViewHolderAlbums(View item) : base(item)
        {
            albumArtwork = item.FindViewById<FrameLayout>(Resource.Id.IDFLALBUM1);
            albumArtist = item.FindViewById<TextView>(Resource.Id.IDTVALBUMARTISTCOL1);
            albumTitle = item.FindViewById<TextView>(Resource.Id.IDTVALBUMTITLECOL1);
        }

        public void OnBind(Album a, Context c)
        {
            //albumArtwork.SetBackgroundResource(Resource.Drawable.MusicExchange300x300);
            albumArtwork.SetBackgroundColor(Color.Red);
            if (a.Artwork != null)
                Utilities.Instance.SetFrameLayoutAsync(a.Artwork, albumArtwork, 165, 165);
            albumArtist.Text = a.Artist;
            albumTitle.Text = a.Title;
            Utilities.Instance.ChangeFont(albumArtist, c, "cuyabra.otf", TypefaceStyle.Normal);
            Utilities.Instance.ChangeFont(albumTitle, c, "cuyabra.otf", TypefaceStyle.Normal);
        }
    }


    public class AlbumsFragmentAdapter : RecyclerView.Adapter
    {

        private Context myContext;
        private LayoutInflater inflater;

        public AlbumsFragmentAdapter(Context c)
        {
            myContext = c;
            inflater = LayoutInflater.From(myContext);
        }

        public override int ItemCount
        {
            get
            {
                return AppManager.Instance.albumList.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolderAlbums vh = holder as ViewHolderAlbums;
            vh.OnBind(AppManager.Instance.albumList[position], myContext);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View row = inflater.Inflate(Resource.Layout.AlbumsFragmentListViewRow, parent, false);
            ViewHolderAlbums vh = new ViewHolderAlbums(row);
            return vh;
        }
    }
}