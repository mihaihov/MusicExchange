using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Graphics;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using System.Threading.Tasks;
using Android.Util;
using Java.Lang;
using Android.Graphics.Drawables;

namespace MusicExchance
{
	public class Utilities : Android.Support.V7.App.AppCompatActivity
	{
		private static Utilities _instance;
		public static Utilities Instance
		{
			get
			{
				if (_instance == null)
					_instance = new Utilities();
				return _instance;
			}
		}


        public float PixelsToDP(Context c, float dimension)
        {
            float density = c.Resources.DisplayMetrics.Density;
            float dp = dimension / density;
            return dp;
        }


        public float DPToPixels(Context c, float dimension)
        {
            float density = c.Resources.DisplayMetrics.Density;
            float px = dimension * density;
            return px;
        }

		public void ChangeFont(TextView tv, Context c, string fontName = "cuyabra.otf", TypefaceStyle tfs = TypefaceStyle.Bold)
		{
			Typeface tf = Typeface.CreateFromAsset(c.Assets, ("fonts/" + fontName).ToString());
			tv.SetTypeface(tf, tfs);
		}


        public void ChangeFont(EditText et, Context c, string fontName = "cuyabra.otf", TypefaceStyle tfs = TypefaceStyle.Bold)
        {
            Typeface tf = Typeface.CreateFromAsset(c.Assets, ("fonts/" + fontName).ToString());
            et.SetTypeface(tf, tfs);
        }


        public void ChangeFont(Button btn, Context c, string fontName = "cuyabra.otf", TypefaceStyle tfs = TypefaceStyle.Bold)
        {
            Typeface tf = Typeface.CreateFromAsset(c.Assets, ("fonts/" + fontName).ToString());
            btn.SetTypeface(tf, tfs);
        }

        public async void SetImageViewAsync(byte[] image, ImageView iv, int reqWidth, int reqHeight)
		{
			if (image == null) return;
			BitmapFactory.Options options = await GetBitmapOptionsOfImageAsync(image);
			Bitmap bitmap = await LoadScaledDownBitmapForDisplayAsync(image, options, reqWidth, reqHeight);
			iv.SetImageBitmap(bitmap);
		}

        public async void SetFrameLayoutAsync(byte[] image, FrameLayout iv, int reqWidth, int reqHeight)
        {
            if (image == null) return;
            BitmapFactory.Options options = await GetBitmapOptionsOfImageAsync(image);
            Bitmap bitmap = await LoadScaledDownBitmapForDisplayAsync(image, options, reqWidth, reqHeight);
            Drawable d = new BitmapDrawable(bitmap);
            iv.SetBackgroundDrawable(d);
        }

        public Bitmap SetSongImageView(byte[] image, ImageView iv = null)
		{
			if (image != null)
			{
				BitmapFactory.Options options = GetBitmapOptionsOfImage(image);
				Bitmap bitmap = LoadScaledDownBitmap(image, options, 50, 50);
				return bitmap;
			}
			return null;
		}


		public async Task<BitmapFactory.Options> GetBitmapOptionsOfImageAsync(byte[] image)
		{
			BitmapFactory.Options options = new BitmapFactory.Options
			{
				InJustDecodeBounds = true
			};
			Bitmap result = await BitmapFactory.DecodeByteArrayAsync(image, 0, image.Length, options);
			return options;
		}

		public int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
		{
			float height = options.OutHeight;
			float width = options.OutWidth;
			double inSampleSize = 1D;

			if (height > reqHeight || width > reqWidth)
			{
				int halfHeight = (int)(height / 2);
				int halfWidth = (int)(width / 2);

				while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth)
				{
					inSampleSize *= 2;
				}
			}

			return (int)inSampleSize;
		}


		public async Task<Bitmap> LoadScaledDownBitmapForDisplayAsync(byte[] image, BitmapFactory.Options options, int reqWidth, int reqHeight)
		{
			// Calculate inSampleSize
			options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

			// Decode bitmap with inSampleSize set
			options.InJustDecodeBounds = false;

			return await BitmapFactory.DecodeByteArrayAsync(image, 0, image.Length, options);
		}




		public Bitmap LoadScaledDownBitmap(byte[] image, BitmapFactory.Options options, int reqWidth, int reqHeight)
		{
			options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);
			options.InJustDecodeBounds = false;
			return BitmapFactory.DecodeByteArray(image, 0, image.Length, options);
		}


		public BitmapFactory.Options GetBitmapOptionsOfImage(byte[] image)
		{
			BitmapFactory.Options options = new BitmapFactory.Options
			{
				InJustDecodeBounds = true
			};
			Bitmap result = BitmapFactory.DecodeByteArray(image, 0, image.Length, options);
			return options;
		}
	}
}