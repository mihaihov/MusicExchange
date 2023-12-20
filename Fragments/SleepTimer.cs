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
using Android.Graphics;

namespace MusicExchance
{
    public class SleepTimer : DialogFragment
    {
        private EditText hoursET, minutesET, secondsET;
        private TextView controlBarTV, hoursTV, minutesTV, secondsTV;
        private Button clearTimeB, startB, closeB;

        public bool isOn = false;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.SleepTimerLayout, container, false);

            //getting references to controls
            hoursET = view.FindViewById<EditText>(Resource.Id.IDETSLEEPTIMERHOURS);
            minutesET = view.FindViewById<EditText>(Resource.Id.IDETSLEEPTIMERMINUTES);
            secondsET = view.FindViewById<EditText>(Resource.Id.IDETSLEEPTIMERSECONDS);
            controlBarTV = view.FindViewById<TextView>(Resource.Id.IDTVCONTROLBAR);
            hoursTV = view.FindViewById<TextView>(Resource.Id.IDTVSLEEPTIMERHOURS);
            minutesTV = view.FindViewById<TextView>(Resource.Id.IDTVSLEEPTIMERMINUTES);
            secondsTV = view.FindViewById<TextView>(Resource.Id.IDTVSLEEPTIMERSECONDS);
            clearTimeB = view.FindViewById<Button>(Resource.Id.IDBSLEEPTIMERCLEARTIME);
            startB = view.FindViewById<Button>(Resource.Id.IDBSLEEPTIMERSTART);
            closeB = view.FindViewById<Button>(Resource.Id.IDBSLEEPTIMERSTOP);


            ////change fonts for textviews
            Utilities.Instance.ChangeFont(controlBarTV,this.Dialog.Context,"cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);
            Utilities.Instance.ChangeFont(hoursTV, this.Dialog.Context, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);
            Utilities.Instance.ChangeFont(minutesTV, this.Dialog.Context, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);
            Utilities.Instance.ChangeFont(secondsTV, this.Dialog.Context, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);

            //change fonts for edittexts.
            Utilities.Instance.ChangeFont(hoursET, this.Dialog.Context, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);
            Utilities.Instance.ChangeFont(minutesET, this.Dialog.Context, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);
            Utilities.Instance.ChangeFont(secondsET, this.Dialog.Context, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);

            //change fonts for buttons
            Utilities.Instance.ChangeFont(clearTimeB, this.Dialog.Context, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);
            Utilities.Instance.ChangeFont(startB, this.Dialog.Context, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);
            Utilities.Instance.ChangeFont(closeB, this.Dialog.Context, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);

            //set the initial value of the timer to be 1 min.
            hoursET.Text = (00).ToString();
            minutesET.Text = (01).ToString();
            secondsET.Text = (00).ToString();
            if(!isOn)
            {
                clearTimeB.Enabled = false;
                clearTimeB.Visibility = ViewStates.Invisible;
            }

            closeB.Click += CloseB_Click;


            return view;
        }

        private void CloseB_Click(object sender, EventArgs e)
        {
            //close button for dialogfragment
            Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }
    }
}