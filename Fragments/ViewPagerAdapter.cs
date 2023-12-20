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
using Fragments = Android.Support.V4.App.Fragment;
using Android.Support.V4.App;
using Java.Lang;

namespace MusicExchance
{
	class ViewPagerAdapter : FragmentPagerAdapter
    {
        private JavaList<Fragments> myFragments;
        private Context myContext;

        public ViewPagerAdapter(Android.Support.V4.App.FragmentManager fm, JavaList<Fragments> f, Context c) : base(fm)
        {
            if (myFragments == null) myFragments = new JavaList<Fragments>();
            myFragments = f;
            myContext = c;
        }

        public override int Count
        {
            get
            {
                return myFragments.Count;
            }
        }

        public override Fragments GetItem(int position)
        {
            return myFragments[position];
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            string title;
            switch(position)
            {
                case 0:
                    title = "Playlists";
                    break;
                case 1:
                    title = "Albums";
                    break;
                case 2:
                    title = "Songs";
                    break;
                case 3:
                    title = "Arists";
                    break;

                default:
                    title = "Error";
                    break;
            }

            return new Java.Lang.String(title);
        }
    }
}