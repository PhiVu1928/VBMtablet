using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using VBM._ctrls;
using VuaBanhMi.iOS.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(_entry), typeof(_entry_ios))]
namespace VuaBanhMi.iOS.CustomControls
{
    public class _entry_ios : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                // do whatever you want to the UITextField here!
                Control.BackgroundColor = UIColor.FromWhiteAlpha(1, 0);
                Control.BorderStyle = UITextBorderStyle.None;
                Control.LeftView = new UIView(new CGRect(0, 0, 10, 0));
                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.RightView = new UIView(new CGRect(0, 0, 10, 0));
                Control.RightViewMode = UITextFieldViewMode.Always;
            }
        }
    }
}