using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MusicExchance
{
    public class PlaylistSettingsDialogFragment : DialogFragment
    {
        ImageButton b1 = null; // Shfulle button (this is for playlists, artists and albums);
        ImageButton b2 = null; // Play next(this is for playlists, artists and albums);
        ImageButton b3 = null; //Delete for playlists and Play for artists and albums;

        TextView tv1 = null;
        TextView tv2 = null;
        TextView tv3 = null;
        TextView title = null;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.PlaylistSettingsDialogFragment, container, false);

            b1 = view.FindViewById<ImageButton>(Resource.Id.IDIBPLAYLISTSETTINGSB1);
            b2 = view.FindViewById<ImageButton>(Resource.Id.IDIBPLAYLISTSETTINGSB2);
            b3 = view.FindViewById<ImageButton>(Resource.Id.IDIBPLAYLISTSETTINGSB3);

            tv1 = view.FindViewById<TextView>(Resource.Id.IDTVPLAYLISTSETTINGSTV1);
            tv2 = view.FindViewById<TextView>(Resource.Id.IDTVPLAYLISTSETTINGSTV2);
            tv3 = view.FindViewById<TextView>(Resource.Id.IDTVPLAYLISTSETTINGSTV3);
            title = view.FindViewById<TextView>(Resource.Id.IDTVPLAYLISTSETTINGSTITLE);

            Utilities.Instance.ChangeFont(tv1, this.Context, "cuyabra.otf");
            Utilities.Instance.ChangeFont(tv2, this.Context, "cuyabra.otf");
            Utilities.Instance.ChangeFont(tv3, this.Context, "cuyabra.otf");
            Utilities.Instance.ChangeFont(title, this.Context, "cuyabra.otf");

            return view;
        }
    }
}