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
using Fragment = Android.Support.V4.App.Fragment;
using Android.Support.Design.Widget;

namespace MusicExchance
{
    public class CurrentSongFragment : Fragment
    {
        private View myView1;           //layout in collapsed mode. Which displays a play button, a textview for artist and another one for song name
        private View myView2;           //layout in expanded mode. Which display two buttons. The one shuffles the songs and the other one is for repeat/repeat one/etc
        private int bottomSheetState;           //it can be collapsed sau expanded. at start is collapsed
        private TextView artistName;
        private TextView songName;
        private TextView songLength;
        private TextView artistNameExpanded;
        private TextView songNameExpanded;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.CurrentSongLayout, container, false);
            myView1 = view.FindViewById<LinearLayout>(Resource.Id.IDLLSECONDTOOLBARBOTTOMSHEETCOLAPSED);
            myView2 = view.FindViewById<LinearLayout>(Resource.Id.IDLLSECONDTOOLBARBOTTOMSHEETEXPANDED);
            myView2.Alpha = 0f;
            bottomSheetState = BottomSheetBehavior.StateCollapsed;

            artistName = view.FindViewById<TextView>(Resource.Id.IDCURRENTSONGARTIST);  
            songName = view.FindViewById<TextView>(Resource.Id.IDCURRENTSONGTITLE);
            songLength = view.FindViewById<TextView>(Resource.Id.IDTVCURRENTSONGLENTH);
            artistNameExpanded = view.FindViewById<TextView>(Resource.Id.IDTVCURRENTSONGARTIST);
            songNameExpanded = view.FindViewById<TextView>(Resource.Id.IDTVCURRENTSONGTITLE);
            Utilities.Instance.ChangeFont(artistName, this.Context,"cuyabra.otf" , Android.Graphics.TypefaceStyle.Normal);
            Utilities.Instance.ChangeFont(songName, this.Context, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);
            Utilities.Instance.ChangeFont(songLength, this.Context, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);
            Utilities.Instance.ChangeFont(artistNameExpanded, this.Context, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);
            Utilities.Instance.ChangeFont(songNameExpanded, this.Context, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);
            return view;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public void AnimateCollapsedToExpanded(float amount)
        {
            amount = amount * 2;
            if(bottomSheetState == BottomSheetBehavior.StateCollapsed)
            {
                myView1.Alpha = 1f - amount;
                if (amount >= 0.5f)
                    myView2.Alpha = Math.Abs(1f - 2f * amount);
            }
            else if(bottomSheetState == BottomSheetBehavior.StateExpanded)
            {
                myView2.Alpha = amount;
                if (amount <= 0.5f)
                    myView1.Alpha = Math.Abs(1f-2f*amount);
            }

        }

        public void BottomSheetOnChangeState(int state)
        {
            if (state == BottomSheetBehavior.StateDragging)
            {
                myView1.Visibility = ViewStates.Visible;
                myView2.Visibility = ViewStates.Visible;
            }

            else if (state == BottomSheetBehavior.StateCollapsed)
            {
                myView1.Visibility = ViewStates.Visible;
                myView2.Visibility = ViewStates.Invisible;
                bottomSheetState = state;
            }

            else if (state == BottomSheetBehavior.StateExpanded)
            {
                myView1.Visibility = ViewStates.Invisible;
                myView2.Visibility = ViewStates.Visible;
                bottomSheetState = state;
            }
        }
    }
}