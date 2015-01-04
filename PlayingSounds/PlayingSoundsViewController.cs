using UIKit;
using CoreGraphics;
using AVFoundation;
using Foundation;
using CoreAnimation;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PlayingSounds {
    public class PlayingSoundsViewController : UIViewController {
        UIButton button1;
        UIButton button2;
        AVAudioPlayer player;
        Random rand;
        public override void ViewDidLoad() {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;
            View.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

            // iOS is forgiving on audio files. It will handle all three of these just fine.
            string mediaType = "caf"; //"mp3"; //"wav";
            var fileUrl = new NSUrl(NSBundle.MainBundle.PathForResource("Sounds/wubwub", mediaType), false);
            player = AVAudioPlayer.FromUrl(fileUrl);
            player.PrepareToPlay();

            button1 = new UIButton(UIButtonType.RoundedRect);
            button1.SetTitle("Make Some Noise", UIControlState.Normal);
            button1.SizeToFit();
            button1.Center = new CGPoint(View.Bounds.Width / 2f, View.Bounds.Height / 2f);
            button1.TouchUpInside += (sender, e) => {
                player.Play();
            };
            // You can tie in to the event when the sound finishes as well. Here, we just spin the button afterward.
            player.FinishedPlaying += (sender, e) => {
                // (Courtesy of http://www.patridgedev.com/2012/10/05/creating-an-animated-spinner-in-a-monotouch-uiimageview/)
                CABasicAnimation rotationAnimation = CABasicAnimation.FromKeyPath("transform.rotation");
                rotationAnimation.To = NSNumber.FromDouble(Math.PI * 2);
                rotationAnimation.RemovedOnCompletion = true;
                // Give the added animation a key for referencing it later (to remove, in this case).
                button1.Layer.AddAnimation(rotationAnimation, "rotationAnimation");
            };
            Add(button1);

            rand = new Random();
            var variousPlayers = (new[] {
                "Sounds/ding-dong",
                "Sounds/pew-beep",
                "Sounds/wee-ooo",
                "Sounds/wubwub",
            }).Select(path => {
                var url = new NSUrl(NSBundle.MainBundle.PathForResource(path, "wav"), false);
                var audioPlayer = AVAudioPlayer.FromUrl(url);
                audioPlayer.PrepareToPlay();
                return audioPlayer;
            }).ToList();
            button2 = new UIButton(UIButtonType.RoundedRect);
            button2.SetTitle("Random", UIControlState.Normal);
            button2.SizeToFit();
            button2.Center = CGPoint.Add(new CGPoint(View.Bounds.Width / 2f, View.Bounds.Height / 2f), new CGSize(0f, button1.Frame.Height + 8f));
            button2.TouchUpInside += (sender, e) => {
                variousPlayers[rand.Next(variousPlayers.Count - 1)].Play();
            };
            Add(button2);
        }
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations() {
            return UIInterfaceOrientationMask.Portrait;
        }
        public override bool PrefersStatusBarHidden() {
            return true;
        }
    }
}