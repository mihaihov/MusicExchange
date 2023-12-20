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
    public class EQSpinnerPresetsAdapter : DialogFragment
    {
        private ListView myListView = null;
        private List<String> items;

        public EQSpinnerPresetsAdapter(List<String> i)
        {
            if(items == null)
            {
                items = new List<String>();
                items.AddRange(i);
            }
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.EQSpinnerLayout, container, false);

            myListView = view.FindViewById<ListView>(Resource.Id.IDLVEQSPINNER);
            myListView.Adapter = new EQSpinnerAdapter(this.Dialog.Context, items);
            return view;
        }



        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }

    }


    public class EQSpinnerAdapter : BaseAdapter<String>
    {
        private Context myContext;
        private List<String> items;

        public EQSpinnerAdapter(Context c, List<String> i)
        {
            myContext = c;
            if(items == null)
            {
                items = new List<String>();
                items.AddRange(i);
            }
        }


        public override string this[int position]
        {
            get
            {
                return items[position];
            }
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if(row == null)
            {
                row = LayoutInflater.From(myContext).Inflate(Resource.Layout.EQSpinnerRowAdapter, null, false);
            }

            TextView tv = row.FindViewById<TextView>(Resource.Id.IDTVEQSPINNERROWADAPTER);
            tv.Text = items[position];
            Utilities.Instance.ChangeFont(tv, myContext, "cuyabra.otf", Android.Graphics.TypefaceStyle.Normal);
            tv.Click += Tv_Click;
            return row;
        }

        private void Tv_Click(object sender, EventArgs e)
        {
            TextView tv = (TextView)sender;
            EqualizerActivity.ChangeSpinnerSelectedItem(tv.Text);
        }
    }
}