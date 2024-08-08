using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RicaAgentApp.Droid.Common;
using Java.Lang;
using System.Collections.Generic;
using Android.Content;
using System.Linq;

namespace RicaAgentApp.Droid
{
    [Activity(Label = "RicaAgentApp", Icon = "@mipmap/icon", Theme = "@style/AppThemeRain", 
        LaunchMode = LaunchMode.SingleTask,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            mContext = this;


            SharedPreferenceManager.GetInstance().SetActivity(this);

            try
            {
                if (NeedsPermission())
                {
                    var perms = RetrievePermissions(this);
                    RequestPermissions(perms.ToArray(), 10);
                }
                else
                {
                    continueStart();
                }
            }
            catch
            {
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == 10)
            {
                if (HasAllPermissionsGranted(grantResults))
                    continueStart();
                else
                    ShowMessageDialogFinish("Not all required permissions have been granted!", "Error");
            }
        }

        public static IList<string> RetrievePermissions(Context context)
        {
            try

            {
                return context.PackageManager.GetPackageInfo(context.PackageName, PackageInfoFlags.Permissions)
                    .RequestedPermissions;
            }
            catch (PackageManager.NameNotFoundException e)
            {
                throw new RuntimeException("This should have never happened.", e);
            }
        }

        public bool NeedsPermission()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                return true;
            return false;
        }

        public bool HasAllPermissionsGranted(Permission[] grantResults)
        {
            for (var i = 0; i < grantResults.Length; i++)
            {
                var grantResult = grantResults[i];

                if (grantResult != Permission.Granted) return false;
            }

            return true;
        }


        private async void continueStart()
        {
            SetContentView(Resource.Layout.login);

            SetupMenu(Resource.Id.txtName, Resource.Id.buttonMenu);
        }
    }
}