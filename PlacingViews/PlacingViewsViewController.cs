using MonoTouch.UIKit;
using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace UIKitAbuse.PlacingViews {
    public class PlacingViewsViewController : UIViewController {
        public Random Rand;
        public PlacingViewsViewController(Random rand) {
            Rand = rand;
        }

        static readonly SizeF ItemSize = new SizeF(30f, 30f);
        UIColor GetRandomColor() {
            int red = Rand.Next(255);
            int green = Rand.Next(255);
            int blue = Rand.Next(255);
            UIColor color = UIColor.FromRGBA(
                (red / 255.0f),
                (green / 255.0f),
                (blue / 255.0f),
                1f);
            return color;
        }
        public override void ViewDidLoad() {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;
            View.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

            // Adjust taps/touches required to fit your needs.
            UITapGestureRecognizer tapRecognizer = new UITapGestureRecognizer() {
                NumberOfTapsRequired = 1,
                NumberOfTouchesRequired = 1,
            };
            tapRecognizer.AddTarget((sender) => {
                // The foreach is only necessary if you have more than one touch for your recognizer.
                // For all else just roll with zero, `PointF location = tapRecognizer.LocationOfTouch(0, View);`
                foreach (int locationIndex in Enumerable.Range(0, tapRecognizer.NumberOfTouches)) {
                    PointF location = tapRecognizer.LocationOfTouch(locationIndex, View);
                    UIView newTapView = new UIView(new RectangleF(PointF.Empty, ItemSize)) {
                        BackgroundColor = GetRandomColor(),
                    };
                    newTapView.Center = location;
                    View.Add(newTapView);
                }
            });
            View.AddGestureRecognizer(tapRecognizer);
        }
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations() {
            return UIInterfaceOrientationMask.Portrait;
        }
        public override bool PrefersStatusBarHidden() {
            return true;
        }
    }
}