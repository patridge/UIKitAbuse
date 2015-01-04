using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;

namespace PlayingSounds {
    public class Application {
        // This is the main entry point of the application.
        static void Main (string[] args) {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate {
        UIWindow window;
        UIViewController viewController;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            UIApplication.SharedApplication.SetStatusBarHidden(true, UIStatusBarAnimation.Slide);
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            viewController = new PlayingSoundsViewController();
            window.RootViewController = viewController;
            window.MakeKeyAndVisible();

            return true;
        }
    }
}