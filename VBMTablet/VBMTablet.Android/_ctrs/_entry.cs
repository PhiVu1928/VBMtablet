using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VBM._ctrls;
using VuaBanhMi.Droid.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(_entry), typeof(_entry_a))]
namespace VuaBanhMi.Droid.CustomControls
{
    public class _entry_a : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                //Control.SetBackgroundResource(Resource.Layout.rounded_shape);
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(0f);
                gradientDrawable.SetStroke(5, Android.Graphics.Color.Transparent);
                gradientDrawable.SetColor(Android.Graphics.Color.Transparent);
                Control.SetBackground(gradientDrawable);
                Control.SetPadding(10, 10, 10, 0);

            }
        }

    }
}