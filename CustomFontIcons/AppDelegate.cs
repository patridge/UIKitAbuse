using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace UIKitAbuse.CustomFontIcons {
    public class Application {
        static void Main(string[] args) {
            try {
                UIApplication.Main(args, null, "AppDelegate");
            }
            catch (Exception ex) {
                Debug.WriteLine("Top-level exception: {0}", ex);
            }
        }
    }
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate {
        const int MaxViews = 15;
        readonly Dictionary<string, string> availableCharsAndDescriptions = new Dictionary<string, string>() {
            { "\uf001", "musical notes" },
            { "\uf004", "heart" },
            { "\uf005", "star" },
            { "\uf013", "gear" },
            { "\uf06b", "present" },
            { "\uf06c", "leaf" },
            { "\uf072", "airplane" },
            { "\uf0ac", "earth" },
            { "\uf0ad", "wrench" },
            { "\uf0c2", "cloud" },
            { "\uf0d1", "truck" },
            { "\uf0e7", "lightning bolt" },
            { "\uf0fb", "jet" },
            { "\uf111", "circle" },
        };


        UIWindow window;
        UIViewController viewController;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            UIApplication.SharedApplication.SetStatusBarHidden(true, UIStatusBarAnimation.Slide);
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            viewController = new CustomFontIconsViewController(new Random(), MaxViews, availableCharsAndDescriptions.Keys.ToList());
            window.RootViewController = viewController;
            window.MakeKeyAndVisible();

            return true;
        }
    }
}