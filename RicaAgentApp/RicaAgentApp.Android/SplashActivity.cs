﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;

using Android.Util;
using System.Threading.Tasks;

namespace RicaAgentApp.Droid
{
    [Activity(Label = "RicaAgentApp", Icon = "@drawable/ic_launcher", Theme = "@style/MyTheme.Splash", MainLauncher = true,
        NoHistory = true)]
    public class SplashActivity : Activity
    {
        private static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            var startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed()
        {
        }

        // Simulates background work that happens behind the splash screen
        private void SimulateStartup()
        {
            Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            //            await Task.Delay(2000); // Simulate a bit of startup work.
            Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            //            OverridePendingTransition(0, 0);
        }
    }
}