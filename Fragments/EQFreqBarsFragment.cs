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
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using MusicExchance.CustomViews;

namespace MusicExchance
{
    public class EQFreqBarsFragment : Fragment , ViewTreeObserver.IOnGlobalLayoutListener
    {
        private LinearLayout mainContent = null;
        private FrameLayout freqBarsFrameLayout;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.EQFreqBarsFragmentLayout, null, false);

            mainContent = view.FindViewById<LinearLayout>(Resource.Id.IDLLMAIN);
            SetBackground();

            return view;
        }

        public void OnGlobalLayout()
        {
            int h = freqBarsFrameLayout.MeasuredHeight;
            int w = freqBarsFrameLayout.MeasuredWidth;
        }

        private void SetBackground()
        {
            int numberOfFreqBars = 8;
            int amplitude = 15;
            int heightOfAmplitudeLayout;

            heightOfAmplitudeLayout = 15 + (amplitude - 3) * 2 + 14 * 30;


            //amplitude linearlayout;
            LinearLayout amplitudeBarLayout = new LinearLayout(this.Context);
            LinearLayout.LayoutParams ablp = new LinearLayout.LayoutParams(0, heightOfAmplitudeLayout);
            ablp.Weight = 0.4f;
            amplitudeBarLayout.LayoutParameters = ablp;
            amplitudeBarLayout.Orientation = Orientation.Vertical;
            mainContent.AddView(amplitudeBarLayout);
            TextView tv1 = new TextView(this.Context);
            tv1.Text = amplitude.ToString()+"dB";
            LinearLayout.LayoutParams tv1lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            tv1lp.Gravity = GravityFlags.Top;
            tv1.Gravity = GravityFlags.Top;
            tv1lp.Weight = 1.3f;
            tv1.TextSize = 12;
            tv1lp.BottomMargin = 17;
            tv1.LayoutParameters = tv1lp;
            TextView tv2 = new TextView(this.Context);
            LinearLayout.LayoutParams tv2lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            tv2lp.Gravity = GravityFlags.Center;
            tv2lp.Weight = 1.3f;
            tv2.TextSize = 12;
            tv2.LayoutParameters = tv2lp;
            tv2.Text = "0dB";
            tv2.Gravity = GravityFlags.Center;
            TextView tv3 = new TextView(this.Context);
            LinearLayout.LayoutParams tv3lp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            tv3lp.Gravity = GravityFlags.Bottom;
            tv3.Gravity = GravityFlags.Bottom;
            tv3.TextSize = 12;
            tv3lp.TopMargin = 17;
            tv3.LayoutParameters = tv3lp;
            tv3lp.Weight = 1.3f;
            tv3.Text = "-" + amplitude.ToString()+"dB";
            amplitudeBarLayout.AddView(tv1);
            amplitudeBarLayout.AddView(tv2);
            amplitudeBarLayout.AddView(tv3);



            FrameLayout freqBarsFrameLayout = new FrameLayout(this.Context);
            LinearLayout.LayoutParams fbfl = new LinearLayout.LayoutParams(0, ViewGroup.LayoutParams.MatchParent,3.6f);
            freqBarsFrameLayout.LayoutParameters = fbfl;
            mainContent.AddView(freqBarsFrameLayout);


            LinearLayout gridLinesContainer = new LinearLayout(this.Context);
            gridLinesContainer.Orientation = Orientation.Vertical;
            LinearLayout.LayoutParams glclp= new LinearLayout.LayoutParams(-1, heightOfAmplitudeLayout);
            gridLinesContainer.LayoutParameters = glclp;
            freqBarsFrameLayout.AddView(gridLinesContainer);







            //draw the grid lines
            for (int i = 1; i <= amplitude; i++)
            {
                int h = 0;
                View v = new View(this.Context);
                if (i == 1 || i == 8 || i == 15) h = 5;
                else h = 2;
                LinearLayout.LayoutParams mlp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, h);
                if (i > 1) mlp.TopMargin = 30;
                v.SetBackgroundColor(Android.Graphics.Color.ParseColor("#e2e2e2"));
                v.LayoutParameters = mlp;
                gridLinesContainer.AddView(v);
            }

            LinearLayout seekBarContainer = new LinearLayout(this.Context);
            seekBarContainer.Orientation = Orientation.Horizontal;
            LinearLayout.LayoutParams sbclp = new LinearLayout.LayoutParams(-1, heightOfAmplitudeLayout + 80);
            seekBarContainer.LayoutParameters = sbclp;
            freqBarsFrameLayout.AddView(seekBarContainer);


            List<String> f = new List<String>(5);
            f.Add("63"); f.Add("125"); f.Add("1k"); f.Add("4k"); f.Add("8k");
            //add the seekbars
            for (int i=1;i<=5;i++)
            {
                LinearLayout seekBarll = new LinearLayout(this.Context);
                seekBarll.Orientation = Orientation.Vertical;
                LinearLayout.LayoutParams sblllp = new LinearLayout.LayoutParams(0, heightOfAmplitudeLayout + 80);
                sblllp.Weight = 0.8f;
                seekBarll.LayoutParameters = sblllp;
                seekBarContainer.AddView(seekBarll);

                VerticalSeekBar sb = new VerticalSeekBar(this.Context);
                sb.Max = 20;
                sb.Min = 0;
                sb.SetProgress(10, false);
                LinearLayout.LayoutParams sblp = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, heightOfAmplitudeLayout);
                sblp.Gravity = GravityFlags.Center;
                sb.LayoutParameters = sblp;
                sb.SetPadding(0, 0, 0, 0);
                seekBarll.AddView(sb);

                TextView tv = new TextView(this.Context);
                tv.Text = f[i - 1];
                tv.Gravity = GravityFlags.CenterHorizontal|GravityFlags.Bottom;
                tv.TextSize = 10;
                LinearLayout.LayoutParams tvll = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                tvll.Gravity = GravityFlags.Center;
                tv.LayoutParameters = tvll;
                seekBarll.AddView(tv);
                
            }
        }
    }
}