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
using Refractored.Controls;
using Android.Views.Animations;

namespace MusicExchance
{
    public class EQSimpleViewFragment : Fragment, ViewTreeObserver.IOnGlobalLayoutListener
    {
        private FrameLayout mainContainer = null;
        private CircleImageView civLeft;
        private CircleImageView civRight;
        private CircleImageView ledLeft1;
        private float civLeftX, civLeftY, civRightX, civRightY, civLeftPivotX, civLeftPivotY, civRightPivotX, civRightPivotY,
            civLeftCenterX, civLeftCenterY, civRightCenterX, civRightCenterY, civWidth, civHeight,
            distanceToCenter,symmetricDistanceToCenter, angleOfRotation, ledRadius;
        private int numberOfLeds;
        private bool contentCreated;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.EQSimpleViewFragmentLayout, null, false);

            mainContainer = view.FindViewById<FrameLayout>(Resource.Id.IDEQSVFL);
            civLeft = view.FindViewById<CircleImageView>(Resource.Id.IDMAINCIVLEFT);    civLeft.ViewTreeObserver.AddOnGlobalLayoutListener(this);
            civRight = view.FindViewById<CircleImageView>(Resource.Id.IDMAINCIVRIGHT);
            contentCreated = false;

            return view;
        }

        private void SetContent()
        {
            contentCreated = true;
            for (int i = 0; i < numberOfLeds; i++)
            {
                float j = i * angleOfRotation;
                j = i * angleOfRotation;
                CircleImageView leftLed = new CircleImageView(this.Context);
                leftLed.LayoutParameters = new LinearLayout.LayoutParams((int)ledRadius, (int)ledRadius);
                leftLed.SetBackgroundResource(Resource.Drawable.ME_RotatoryKnobLed);
                leftLed.SetX(civLeftCenterX - distanceToCenter);
                leftLed.SetY(civLeftCenterY);
                mainContainer.AddView(leftLed);


                CircleImageView rightLed = new CircleImageView(this.Context);
                rightLed.LayoutParameters = new LinearLayout.LayoutParams((int)ledRadius, (int)ledRadius);
                rightLed.SetBackgroundResource(Resource.Drawable.ME_RotatoryKnobLed);
                rightLed.SetX(civRightCenterX - distanceToCenter);
                rightLed.SetY(civRightCenterY);
                mainContainer.AddView(rightLed);


                RotateAnimation ra = new RotateAnimation(0f, j, civLeftCenterX, civLeftCenterY);
                ra.Duration = 0;
                ra.FillAfter = true;
                leftLed.Animation = ra;
                ra.Start();


                RotateAnimation ra2 = new RotateAnimation(0F, j, civRightCenterX, civRightCenterY);
                ra2.Duration = 0;
                ra2.FillAfter = true;
                rightLed.Animation = ra2;
                ra2.Start();
            }

        }

        public void OnGlobalLayout()
        {
            civLeftX = civLeft.GetX();  civLeftY = civLeft.GetY();  civLeftPivotX = civLeft.PivotX; civLeftPivotY = civLeft.PivotY; civWidth = civLeft.Width; civHeight = civLeft.Height;
            civRightX = civRight.GetX(); civRightY = civRight.GetY();   civRightPivotX = civRight.PivotX;   civRightPivotY = civRight.PivotY;
            civLeftCenterX = (civLeftX + civWidth / 2) - 13;
            civLeftCenterY = civLeftY + civHeight / 2;
            civRightCenterX = (civRightX + civWidth/ 2) - 13;
            civRightCenterY = civRightY + civHeight / 2;

            ledRadius = civWidth / 10f;
            distanceToCenter = civWidth / 2 + 70;
            symmetricDistanceToCenter = distanceToCenter - 50;
            numberOfLeds = 9;
            angleOfRotation = (float)180 / (float)(numberOfLeds - 1);
            angleOfRotation += 1;

            if (contentCreated == false) SetContent();
         
        }
    }
}