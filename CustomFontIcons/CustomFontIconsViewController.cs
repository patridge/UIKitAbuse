using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.CoreAnimation;

namespace UIKitAbuse.CustomFontIcons {
    public class CustomFontIconsViewController : UIViewController {
        static readonly UIFont IconFont = UIFont.FromName("FontAwesome", 40f);
        public override void ViewDidLoad() {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;
            View.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

            char jet = '\uf0fb';
            var label = new UILabel() {
                Font = IconFont,
                Text = jet.ToString(),
                TextColor= UIColor.Green,
                BackgroundColor = UIColor.Gray,
            };
            label.SizeToFit();
            label.Center = new PointF(View.Bounds.Width / 2f, View.Bounds.Width / 2f);
            Add(label);

            var slider = new UISlider(new RectangleF(new PointF(0f, View.Bounds.Height - 50f), new SizeF(View.Bounds.Width, 50f)));
            float maxFontSize = label.Text.GetMaxFontSize(label.Font, new SizeF(View.Bounds.Width, slider.Frame.Top));
            slider.MinValue = 0.1f; // If you go to 0, you end up losing your font for some reason.
            slider.MaxValue = maxFontSize;
            slider.Value = IconFont.PointSize;
            slider.ValueChanged += (sender, e) => {
                label.Font = label.Font.WithSize(slider.Value);
                label.SizeToFit();
                label.Center = new PointF(View.Bounds.Width / 2f, View.Bounds.Width / 2f);
            };
            Add(slider);

            char refresh = '\uf021';
            var button = new UIButton(UIButtonType.RoundedRect) {
                Font = IconFont,
            };
            button.SetTitle(refresh.ToString(), UIControlState.Normal);
            button.SizeToFit();
            button.Center = new PointF(View.Bounds.Width / 2f, slider.Frame.Top - (button.Frame.Height / 2f));
            button.TouchUpInside += (sender, e) => {
                // (Courtesy of http://www.patridgedev.com/2012/10/05/creating-an-animated-spinner-in-a-monotouch-uiimageview/)
                CABasicAnimation rotationAnimation = CABasicAnimation.FromKeyPath("transform.rotation");
                rotationAnimation.To = NSNumber.FromDouble(Math.PI * 2);
                rotationAnimation.RemovedOnCompletion = true;
                // Give the added animation a key for referencing it later (to remove, in this case).
                label.Layer.AddAnimation(rotationAnimation, "rotationAnimation");
            };
            Add(button);
        }
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations() {
            return UIInterfaceOrientationMask.Portrait;
        }
        public override bool PrefersStatusBarHidden() {
            return true;
        }
    }
}