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
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Media;
using Android.Media.Audiofx;
using MusicExchance.CustomViews;
using Orientation = Android.Widget.Orientation;
using Refractored.Controls;
using Android.Views.Animations;

namespace MusicExchance
{
    [Activity]
    public class EqualizerActivity : Android.Support.V7.App.AppCompatActivity
    {
        private static EqualizerActivity _instance;
        public static EqualizerActivity Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EqualizerActivity();
                return _instance;
            }
        }


        private Toolbar myToolbar;
        private static TextView eqPresets;
        private Equalizer myEqualizer;
        private short numberOfBands;
        private short minAmplitude;
        private short maxAmplitude;
        private short numberOfEQPresets;
        private List<String> EQPresetNames;
        private BaseAdapter<String> EQPresetSpinnerAdapter;
        private Android.Support.V4.View.ViewPager eqViewPager;
        private Button bassBoosterButton;


        private LinearLayout freqBarsLinearLayout = null;           //container for equalizer freqeuncy seekbar
        private FrameLayout eqSimpleView = null;                    //container for equalizer bass booster and virualizer


        //equalizer simple view variables
        private CircleImageView civLeft;
        private CircleImageView civRight;
        private CircleImageView eqLeftVolumeIndicator;
        private CircleImageView eqRightVolumeIndicator;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EqualizerLayout);

            myToolbar = FindViewById<Toolbar>(Resource.Id.IDEQTOOLBAR);
            eqPresets = FindViewById<TextView>(Resource.Id.IDTVEQLAYOUT); Utilities.Instance.ChangeFont(eqPresets, this.ApplicationContext, "cuyabra.otf", Android.Graphics.TypefaceStyle.Bold);
            freqBarsLinearLayout = FindViewById<LinearLayout>(Resource.Id.IDLLEQFREQBARS);
            eqSimpleView = FindViewById<FrameLayout>(Resource.Id.IDFLEQSIMPLEVIEW);
            civLeft = FindViewById<CircleImageView>(Resource.Id.IDMAINCIVLEFT);
            civRight = FindViewById<CircleImageView>(Resource.Id.IDMAINCIVRIGHT);
            eqLeftVolumeIndicator = FindViewById<CircleImageView>(Resource.Id.IDMAINCIVLEFTVOLUMEINDICATOR);
            eqRightVolumeIndicator = FindViewById<CircleImageView>(Resource.Id.IDMAINCIVRIGHTVOLUMEINDICATOR);

            EQPresetNames = new List<String>();
            for (int i = 1; i <= 10; i++)
                EQPresetNames.Add("Item " + i.ToString());

            SetSupportActionBar(myToolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            InitializeFreqBarsView();

            eqPresets.Click += EqPresets_Click;
        }


        private void InitializeFreqBarsView()
        {
            int numberOfFreqBars = 8;
            int amplitude = 15;
            int heightOfAmplitudeLayout, heightOfAmplitudeLayoutDP;

            heightOfAmplitudeLayout = 700 + (amplitude - 3) * 2 + 14 * 30;
            heightOfAmplitudeLayoutDP = (int)Utilities.Instance.PixelsToDP(this.ApplicationContext, heightOfAmplitudeLayout);


            //amplitude linearlayout is the vertical-linearlayout which contains the amplitude textviews (-15dB, 0dB, 15dB);
            LinearLayout amplitudeBarLayout = new LinearLayout(this.ApplicationContext);
            amplitudeBarLayout.Orientation = Orientation.Vertical;
            LinearLayout.LayoutParams ablp = new LinearLayout.LayoutParams(0, heightOfAmplitudeLayoutDP);
            ablp.Weight = 0.4f;
            amplitudeBarLayout.LayoutParameters = ablp;
            freqBarsLinearLayout.AddView(amplitudeBarLayout);

            ///textview 1
            TextView tv1 = new TextView(this.ApplicationContext);
            tv1.Text = amplitude.ToString() + "dB";
            tv1.TextSize = Utilities.Instance.PixelsToDP(this.ApplicationContext,27);
            tv1.SetTextColor(Android.Graphics.Color.ParseColor("#000000"));
            LinearLayout.LayoutParams tv1lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            tv1lp.Gravity = GravityFlags.Top;
            tv1.Gravity = GravityFlags.Top;
            tv1lp.Weight = 1.3f;
            tv1lp.BottomMargin = (int)Utilities.Instance.PixelsToDP(this.ApplicationContext, 22); 
            tv1.LayoutParameters = tv1lp;

            ///textview 2
            TextView tv2 = new TextView(this.ApplicationContext);
            tv2.Text = "0dB";
            tv2.TextSize = Utilities.Instance.PixelsToDP(this.ApplicationContext, 27); 
            tv2.SetTextColor(Android.Graphics.Color.ParseColor("#000000"));
            tv2.Gravity = GravityFlags.Center | GravityFlags.Left;
            LinearLayout.LayoutParams tv2lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            tv2lp.Gravity = GravityFlags.Center|GravityFlags.Left;
            tv2lp.Weight = 1.3f;
            tv2.LayoutParameters = tv2lp;

            //textview 3
            TextView tv3 = new TextView(this.ApplicationContext);
            tv3.TextSize = Utilities.Instance.PixelsToDP(this.ApplicationContext, 27); 
            tv3.Gravity = GravityFlags.Bottom;
            tv3.Text = "-" + amplitude.ToString() + "dB";
            tv3.SetTextColor(Android.Graphics.Color.ParseColor("#000000"));
            LinearLayout.LayoutParams tv3lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            tv3lp.Gravity = GravityFlags.Bottom;
            tv3lp.TopMargin = (int)Utilities.Instance.PixelsToDP(this.ApplicationContext, 22);
            tv3.LayoutParameters = tv3lp;
            tv3lp.Weight = 1.3f;

            //adding textview1, textview2, textview3 to amplitudeBarLayout(which is a linear layout)
            amplitudeBarLayout.AddView(tv1);
            amplitudeBarLayout.AddView(tv2);
            amplitudeBarLayout.AddView(tv3);


            //this frame layout will contain the grid lines(background) and the vertical seekbars)
            FrameLayout freqBarsFrameLayout = new FrameLayout(this.ApplicationContext);
            LinearLayout.LayoutParams fbfl = new LinearLayout.LayoutParams(0, ViewGroup.LayoutParams.MatchParent, 3.6f);
            freqBarsFrameLayout.LayoutParameters = fbfl;
            freqBarsLinearLayout.AddView(freqBarsFrameLayout);

            //this vertica linear layout has the same dimension as its parent( the frame layout defined above - freqBarsFrameLayout) 
            //and will contain horizontal lines drawn 
            //on top of each other.
            LinearLayout gridLinesContainer = new LinearLayout(this.ApplicationContext);
            gridLinesContainer.Orientation = Orientation.Vertical;
            LinearLayout.LayoutParams glclp = new LinearLayout.LayoutParams(-1, heightOfAmplitudeLayoutDP);
            gridLinesContainer.LayoutParameters = glclp;
            freqBarsFrameLayout.AddView(gridLinesContainer);



            //draw the grid lines which will be placed in the linearlayout defined above(gridLinesContainer)
            for (int i = 1; i <= amplitude; i++)
            {
                int h = 0;
                View v = new View(this.ApplicationContext);
                if (i == 1 || i == 8 || i == 15) h = (int)Utilities.Instance.PixelsToDP(this.ApplicationContext, 20); 
                else h =(int) Utilities.Instance.PixelsToDP(this.ApplicationContext, 10);
                LinearLayout.LayoutParams mlp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, h);
                int g = (int)Utilities.Instance.PixelsToDP(this.ApplicationContext, 73);
                if (i > 1) mlp.TopMargin = (int)Utilities.Instance.PixelsToDP(this.ApplicationContext, 73);
                v.SetBackgroundColor(Android.Graphics.Color.ParseColor("#e2e2e2"));
                v.LayoutParameters = mlp;
                gridLinesContainer.AddView(v);
            }


            //this horizontal linearlayout will contain the vertical seekbars. Its parent is the framelayout freqBarsFrameLayout.
            LinearLayout seekBarContainer = new LinearLayout(this.ApplicationContext);
            seekBarContainer.Orientation = Orientation.Horizontal;
            LinearLayout.LayoutParams sbclp = new LinearLayout.LayoutParams(-1, (int)Utilities.Instance.PixelsToDP(this.ApplicationContext, heightOfAmplitudeLayout + 200));
            seekBarContainer.LayoutParameters = sbclp;
            freqBarsFrameLayout.AddView(seekBarContainer);



            //it will be erased, is just for testing!!!!
            List<String> f = new List<String>(5);
            f.Add("63"); f.Add("125"); f.Add("1k"); f.Add("4k"); f.Add("8k");


            //add the seekbars
            for (int i = 1; i <= 5; i++)
            {
                LinearLayout seekBarll = new LinearLayout(this.ApplicationContext);
                seekBarll.Orientation = Orientation.Vertical;
                LinearLayout.LayoutParams sblllp = new LinearLayout.LayoutParams(0,
                  (int)Utilities.Instance.PixelsToDP(this.ApplicationContext, heightOfAmplitudeLayout + 200));
                sblllp.Weight = 0.8f;
                seekBarll.LayoutParameters = sblllp;
               
                seekBarContainer.AddView(seekBarll);

                VerticalSeekBar sb = new VerticalSeekBar(this.ApplicationContext);
                sb.Max = 20;
                sb.Min = 0;
                sb.SetProgress(10, false);
                LinearLayout.LayoutParams sblp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, heightOfAmplitudeLayoutDP);
                sblp.Gravity = GravityFlags.Center;
                sb.LayoutParameters = sblp;
                sb.SetPadding(0, 0, 0, 0);
                seekBarll.AddView(sb);

                TextView tv = new TextView(this.ApplicationContext);
                tv.Text = f[i - 1];
                tv.SetTextColor(Android.Graphics.Color.ParseColor("#000000"));
                tv.Gravity = GravityFlags.Bottom| GravityFlags.CenterHorizontal;
                tv.TextSize = Utilities.Instance.PixelsToDP(this.ApplicationContext, 24); 
                LinearLayout.LayoutParams tvll = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                tvll.Gravity =  GravityFlags.Bottom|GravityFlags.CenterHorizontal;
                tv.LayoutParameters = tvll;
                seekBarll.AddView(tv);

            }
        }


        private void EqPresets_Click(object sender, EventArgs e)
        {
            EQSpinnerPresetsAdapter espa = new EQSpinnerPresetsAdapter(EQPresetNames);
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            espa.Show(ft, "SleepTimerFragmentDialog");
        }



        public static void ChangeSpinnerSelectedItem(String x)
        {
            eqPresets.Text = x;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }


        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }

}