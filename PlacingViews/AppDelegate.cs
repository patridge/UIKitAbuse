using System;
using System.Diagnostics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace UIKitAbuse.PlacingViews {
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
        UIWindow window;
        UIViewController viewController;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            UIApplication.SharedApplication.SetStatusBarHidden(true, UIStatusBarAnimation.Slide);
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            viewController = new PlacingViewsViewController(new Random());
            window.RootViewController = viewController;
            window.MakeKeyAndVisible();

            return true;
        }
    }
}