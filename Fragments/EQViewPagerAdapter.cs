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
using Android.Support.V4.App;
using Fragment = Android.Support.V4.App;

namespace MusicExchance
{
    public class EQViewPagerAdapter : FragmentPagerAdapter
    {
        private JavaList<Android.Support.V4.App.Fragment> items;
        private Context myContext;


        public EQViewPagerAdapter(Android.Support.V4.App.FragmentManager fm, JavaList<Android.Support.V4.App.Fragment> i, Context c)
            :base(fm)
        {
            myContext = c;
            if (items == null) items = new JavaList<Android.Support.V4.App.Fragment>();
            items = i;
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return items[position];
        }


    }
}