using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace UIKitAbuse.CustomFontIcons {
    public class CustomFontIconsViewController : UIViewController {
        public Random Rand;
        public int MaxItemsShown;
        public List<string> IconChars;
        readonly Queue<UIView> AddedViews = new Queue<UIView>();
        public CustomFontIconsViewController(Random rand, int maxItemsShown, List<string> iconChars) {
            Rand = rand;
            MaxItemsShown = maxItemsShown;
            IconChars = iconChars;
        }

        static readonly float CharacterSize = 40f;
        static readonly UIFont IconFont = UIFont.FromName("FontAwesome", CharacterSize);
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
        UILabel GetCharacterLabel() {
            string icon = IconChars[Rand.Next(IconChars.Count)];
            UILabel label = new UILabel() {
                Font = IconFont,
                Text = icon,
                TextColor = GetRandomColor(),
                BackgroundColor = UIColor.Clear,
            };
            label.SizeToFit();
            return label;
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
                PointF location = tapRecognizer.LocationOfTouch(0, View);
                UIView newTapView = GetCharacterLabel();
                newTapView.Center = location;
                View.Add(newTapView);
                AddedViews.Enqueue(newTapView);
                if (AddedViews.Count > MaxItemsShown) {
                    UIView oldView = AddedViews.Dequeue();
                    oldView.RemoveFromSuperview();
                    oldView.Dispose();
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