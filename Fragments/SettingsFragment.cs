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
using Android.Preferences;

namespace MusicExchance
{
    public class SettingsFragment : PreferenceFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AddPreferencesFromResource(Resource.Xml.preferences);

            Preference equlizer = FindPreference("preference_equlizer");
            Preference sleepTime = FindPreference("preference_sleeptime");
            Preference refresh = FindPreference("preference_refresh");


            equlizer.PreferenceClick += Pref_EqulizerClick;
            sleepTime.PreferenceClick += Pref_SleepTimeClick;
            refresh.PreferenceClick += Pref_RefreshClick;
        }

        private void Pref_EqulizerClick(object sender, Preference.PreferenceClickEventArgs e)
        {
            //Console.WriteLine("You clicked on equlizer");
            Intent i = new Intent(Application.Context, typeof(EqualizerActivity));
            StartActivity(i);
        }


        private void Pref_SleepTimeClick(object sender, Preference.PreferenceClickEventArgs e)
        {
            //Console.WriteLine("You clicked on sleep timer");
            SleepTimer st = new SleepTimer();
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            st.Show(ft,"SleepTimerFragmentDialog");
        }


        private void Pref_RefreshClick(object sender, Preference.PreferenceClickEventArgs e)
        {
            Console.WriteLine("You clicked on refresh!");
        }
    }
}