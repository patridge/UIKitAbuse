using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;

namespace UIKitAbuse.CustomFontIcons {
    public class CustomFontIconsViewController : UIViewController {
        static readonly UIFont IconFont = UIFont.FromName("FontAwesome", 40f);
        public override void ViewDidLoad() {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;
            View.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

            char jet = '\uf0fb';
            var label = new UILabel(RectangleF.Empty) {
                Font = IconFont,
                Text = jet.ToString(),
                TextColor= UIColor.Orange,
                BackgroundColor = UIColor.Blue,
            };
            label.SizeToFit();
            Add(label);

            var slider = new UISlider(new RectangleF(new PointF(0f, View.Bounds.Height - 50f), new SizeF(View.Bounds.Width, 50f)));
            float maxFontSize = label.Text.GetMaxFontSize(label.Font, new SizeF(View.Bounds.Width, slider.Frame.Top));
            slider.MinValue = 0.1f; // If you go to 0, you end up losing your font for some reason.
            slider.MaxValue = maxFontSize;
            slider.Value = IconFont.PointSize;
            slider.ValueChanged += (sender, e) => {
                label.Font = label.Font.WithSize(slider.Value);
                label.SizeToFit();
            };
            Add(slider);
        }
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations() {
            return UIInterfaceOrientationMask.Portrait;
        }
        public override bool PrefersStatusBarHidden() {
            return true;
        }
    }
}