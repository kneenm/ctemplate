
using Foundation;
using UIKit;

namespace RicaAgentApp.iOS
{

    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
        }

        public override void DidEnterBackground(UIApplication application)
        {
        }

        public override void WillEnterForeground(UIApplication application)
        {
        }

        public override void OnActivated(UIApplication application)
        {
        }

        public override void WillTerminate(UIApplication application)
        {
        }

        //        [Export("application:openURL:sourceApplication:annotation:")]
        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            //            if (sourceApplication == "com.apple.SafariViewService")
            //            {
            NSNotificationCenter.DefaultCenter.PostNotificationName("SafariCallback", url);
            //            };
            return true;
        }

        public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
        {
            if (userActivity.ActivityType == NSUserActivityType.BrowsingWeb)
                //I found I needed to delay the handling a bit to make sure the UI was up to speed.
                System.Threading.Tasks.Task.Delay(500).ContinueWith(_ =>
                {
                    this.InvokeOnMainThread(() =>
                    {
                        //                        OutputStringToConsole("Calling OpenAppFromUniversalLink");
                        //                        OpenAppFromUniversalLink(userActivity.WebPageUrl);
                    });
                });

            return true;
        }

        public override bool WillContinueUserActivity(UIKit.UIApplication application, string userActivityType)
        {

            return true;
        }
    }
}
