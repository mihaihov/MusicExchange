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
using Android.Preferences;

namespace MusicExchance
{
    [Activity(Label = "", ParentActivity = typeof(MainActivity))]
    public class SettingsActivity : Android.Support.V7.App.AppCompatActivity
    {
        LinearLayout fragmentContainer = null;
        Toolbar myToolbar = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SettingsActivityLayout);

            //handle the SettingsActivity's toolbar to have a back button
            //the behaviour for the back button is handled in the OnOptionsItemSelected function
            myToolbar = FindViewById<Toolbar>(Resource.Id.IDTSETTINGSACTIVITY);
            SetSupportActionBar(myToolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);


            fragmentContainer = FindViewById<LinearLayout>(Resource.Id.IDLLSETTINGSFRAGMENTCONTAINER);
            FragmentManager.BeginTransaction().Replace(fragmentContainer.Id, new SettingsFragment()).Commit();


        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch(item.ItemId)
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