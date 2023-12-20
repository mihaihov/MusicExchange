using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Graphics;
using ViewPager = Android.Support.V4.View.ViewPager;
using Android.Support.Design.Widget;


/// <summary>
/// (07.07.2017)
/// ATUNCI CAND APAS PE SEARCH SI APARE TASTATURA VIEWS-URILE PE CARE TASTATURA AR TREBUI SA LE ACOPERE SUNT IMPINSE IN SUS
/// TREBUIE SA GASESC O SOLUTIE A.I. ATUNCI CAND APARE TASTATURA, SA ACOPERE VIEWS-URILE NU SA LE IMPINGA IN SUS. (REZOLVARE :  [Activity(Label = "MusicExchance", MainLauncher = true, Icon = "@drawable/icon", WindowSoftInputMode = SoftInput.AdjustNothing)] )
/// 
///
/// </summary>





namespace MusicExchance
{
    [Activity(Label = "MusicExchance", MainLauncher = true, Icon = "@drawable/icon", WindowSoftInputMode = SoftInput.AdjustNothing)]
	public class MainActivity : Android.Support.V7.App.AppCompatActivity,ViewTreeObserver.IOnGlobalLayoutListener
    {
		private static MainActivity _instance;
		public static MainActivity Instance
		{
			get
			{
				if (_instance == null)
					_instance = new MainActivity();
				return _instance;
			}
		}

        public Context aplContext = null;


        private Android.Support.V4.View.ViewPager myViewPager;
        private Toolbar firstToolbar;
        private TextView firstToolbarTitle;
        private Android.Support.V4.View.PagerTabStrip pagerTabStrip;
        private BottomSheetBehavior secondToolbar;
        private View bsSupport;
        private ViewTreeObserver vto;

        //este folosit pentru a creea fragmentul care populeaza bottomsheet-ul. de asemenea este folosit si ca referinta pentru a anima 
        //obiectele din fragmentul respectiv.
        private CurrentSongFragment myCurrentSongFragment;          

		protected override void OnCreate(Bundle bundle)
		{
            
            
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);
            aplContext = this.ApplicationContext;
            myViewPager = FindViewById<ViewPager>(Resource.Id.IDVIEWPAGER);
            AppManager.Instance.GetDeviceSongs();
            InitializeViewPager();

            firstToolbar = FindViewById<Toolbar>(Resource.Id.IDFIRSTTOOLBAR);
            SetSupportActionBar(firstToolbar);
            SupportActionBar.Title = string.Empty;
            firstToolbarTitle = FindViewById<TextView>(Resource.Id.IDTVTITLEFIRSTTOOLBAR);
            firstToolbarTitle.Text = "Music Exchange";
            ChangeFont(firstToolbarTitle, "cuyabra.otf");


            //change fonts for pagertabstrip
            pagerTabStrip = FindViewById<Android.Support.V4.View.PagerTabStrip>(Resource.Id.IDPAGERTABSTRIP);
            for (int i = 0; i < pagerTabStrip.ChildCount; ++i)
            {
                View nextChild = pagerTabStrip.GetChildAt(i);
                if (nextChild.GetType() == typeof(TextView))
                {
                    TextView textViewToConvert = (TextView)nextChild;
                    ChangeFont(textViewToConvert, "cuyabra.otf");
                }
            }

            //GET THE HEIGHT OF THE BOTTOMSHEET SUPPORT(VIEW UI) AND THAT HEIGHT IS SET AS PEEKHEIGHT FOR secondToolbar;
            bsSupport = FindViewById<View>(Resource.Id.IDVBOTTOMSHEETSUPPORT);
            vto = bsSupport.ViewTreeObserver;
            vto.AddOnGlobalLayoutListener(this);


            //Initialize second toolbar
            LinearLayout view = FindViewById<LinearLayout>(Resource.Id.IDLLSECONDTOOLBARCONTAINER);
            myCurrentSongFragment = new CurrentSongFragment();
            secondToolbar = BottomSheetBehavior.From(view);
            secondToolbar.SetBottomSheetCallback(new BottomSheetEventHandler(myCurrentSongFragment));
            secondToolbar.PeekHeight = 135;
            secondToolbar.Hideable = false;
            secondToolbar.State = BottomSheetBehavior.StateCollapsed;

            
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(view.Id, myCurrentSongFragment, "MyFragment");
            trans.Commit();
        }



        void InitializeViewPager()
        {
            JavaList<Fragment> myList = new JavaList<Fragment>();
            myList.Add(new PlaylistsFragment());
            myList.Add(new AlbumsFragment());
            myList.Add(new SongsFragment());
            myList.Add(new ArtistsFragment());
            
            ViewPagerAdapter vpa = new ViewPagerAdapter(this.SupportFragmentManager, myList, this.ApplicationContext);
            myViewPager.Adapter = vpa;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.FirstToolbarMenu, menu);

            //Create the search bar;
            var searchItem = menu.FindItem(Resource.Id.IDMSEARCH);
            var searchView = MenuItemCompat.GetActionView(searchItem);
            Android.Support.V7.Widget.SearchView _searchView = searchView.JavaCast<Android.Support.V7.Widget.SearchView>();

            _searchView.QueryTextSubmit += (s, e) =>
            {
                Console.WriteLine(_searchView.Query);
                Intent intent = new Intent(this.ApplicationContext, typeof(SearchResultActivity));
                intent.SetAction(Intent.ActionSearch);
                intent.PutExtra("QUERY_DATA", _searchView.Query);
                StartActivity(intent);
                e.Handled = true;
            };
            

            //return base.OnCreateOptionsMenu(menu);
            return true;
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Resource.Id.IDMENUSETTINGS:
                    {
                        Android.Content.Intent i = new Android.Content.Intent(this.ApplicationContext, typeof(SettingsActivity));
                        StartActivity(i);
                        return true;
                    }

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public void ChangeFont(TextView tv, string fontName = "cuyabra.otf", TypefaceStyle tfs = TypefaceStyle.Bold)
        {
            Typeface tf = Typeface.CreateFromAsset(this.Assets, ("fonts/" + fontName).ToString());
            tv.SetTypeface(tf, tfs);
        }



        public void ChangeFont(Button btn, string fontName = "cuyabra.otf", TypefaceStyle tfs = TypefaceStyle.Bold)
        {
            Typeface tf = Typeface.CreateFromAsset(this.Assets, ("fonts/" + fontName).ToString());
            btn.SetTypeface(tf, tfs);
        }


        public void ChangeFont(EditText et, string fontName = "cuyabra.otf", TypefaceStyle tfs = TypefaceStyle.Bold)
        {
            Typeface tf = Typeface.CreateFromAsset(this.Assets, ("fonts/" + fontName).ToString());
            et.SetTypeface(tf, tfs);
        }

        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            //daca urmatoarea linie ramane atunci sunt probleme cu viewpager-ul(poate ramane intre fragmente i.e. utilizatorul poate bloca paginile a.i.
            //sa se vada jumatate dintr-un fragment si jumatate din altul). daca urmatoarea linie nu ramane atunci swipe-ul viewpager-ului este detectat
            //mult mai greu deoarece prima data se detecteaza swipe pentru recyclerview.
            myViewPager.OnTouchEvent(ev);
            return base.DispatchTouchEvent(ev);
        }

        //implement the ViewTreeObserver.IOnGlobalLayoutListener interface
        public void OnGlobalLayout()
        {
            //setam peekheight-ul pentru bottomsheet ca fiind View-ul cu id-ul IDVBOTTOMSHEETSUPPORT(se afla in fisierul Main.axml)
            secondToolbar.PeekHeight = bsSupport.Height;
        }
    }



    public class BottomSheetEventHandler : BottomSheetBehavior.BottomSheetCallback
    {
        private CurrentSongFragment myCsf;


        public BottomSheetEventHandler(CurrentSongFragment csf)
        {
            myCsf = csf;
        }

        public override void OnSlide(View bottomSheet, float slideOffset)
        {
            myCsf.AnimateCollapsedToExpanded(slideOffset);
        }

        public override void OnStateChanged(View bottomSheet, int newState)
        {
            myCsf.BottomSheetOnChangeState(newState);
        }

    }
}




