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
using Android.Support.V7.App;

namespace MusicExchance
{
    [Activity]
    [IntentFilter(new[] { Intent.ActionSearch })]
    [MetaData(name:"android.app.searchable",Resource ="@xml/searchable")]
    public class SearchResultActivity : Activity
    {
        public ListView myListView;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SearchResultLayout);

            myListView = FindViewById<ListView>(Resource.Id.IDLVSEARCHRESULT);

            Intent intent = Intent;
            if (Android.Content.Intent.ActionSearch.Equals(intent.Action))
            {
                string query = intent.GetStringExtra("QUERY_DATA");
                DoMySearch(query);
            }


        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.FirstToolbarMenu, menu);

            //Create the search bar;
            var searchItem = menu.FindItem(Resource.Id.IDMSEARCH);
            var searchView = Android.Support.V4.View.MenuItemCompat.GetActionView(searchItem);
            Android.Support.V7.Widget.SearchView _searchView = searchView.JavaCast<Android.Support.V7.Widget.SearchView>();

            return true;
        }


        public void DoMySearch(string query)
        {


        }
    }
}