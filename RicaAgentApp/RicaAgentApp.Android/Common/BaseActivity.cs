using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using File = Java.IO.File;
using Path = System.IO.Path;

namespace RicaAgentApp.Droid.Common
{
    [Activity(Label = "BaseActivity")]
    public class BaseActivity : Activity
    {
        private AlertDialog.Builder alert;

        public Context mContext;
        public AlertDialog progressDialog;

        public static string Directorypath
        {
            get
            {
                if (Android.OS.Environment.ExternalStorageDirectory.Exists())
                {
                    var fleID = new File(Android.OS.Environment.ExternalStorageDirectory, "GotSim");
                    var mk = fleID.Mkdir();
                }

                return Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "GotSim");
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.IndeterminateProgress);
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

            base.OnCreate(savedInstanceState);

            RequestedOrientation = ScreenOrientation.Portrait;

            // Create your application here
        }

        public void ShowMessageDialog(string message, string title, string posTitle,
            EventHandler<DialogClickEventArgs> positive, string negTitle, EventHandler<DialogClickEventArgs> negative)
        {
            try
            {
                // stopProgressDialog();

                alert = new AlertDialog.Builder(this, Resource.Style.AppThemeRain_Dark_Dialog);
                alert.SetTitle(title);
                alert.SetMessage(message);
                alert.SetCancelable(false);
                alert.SetPositiveButton(posTitle, positive);
                alert.SetNegativeButton(negTitle, negative);

                Dialog dialog = alert.Create();
                dialog.Show();
            }
            catch
            {
            }
        }

        public void ShowMessageDialog(string message, string title)
        {
            try
            {
                var ok = GetString(Resource.String.ok);

                // stopProgressDialog();

                alert = new AlertDialog.Builder(this, Resource.Style.AppThemeRain_Dark_Dialog);
                alert.SetTitle(title);
                alert.SetCancelable(false);
                alert.SetMessage(message);
                alert.SetPositiveButton(ok, (senderAlert, args) => { });

                Dialog dialog = alert.Create();
                dialog.Show();
            }
            catch
            {
            }
        }

        public void ShowMessageDialogFinish(string message, string title)
        {
            try
            {
                var ok = GetString(Resource.String.ok);

                // stopProgressDialog();

                alert = new AlertDialog.Builder(this, Resource.Style.AppThemeRain_Dark_Dialog);
                alert.SetTitle(title);
                alert.SetMessage(message);
                alert.SetCancelable(false);
                alert.SetPositiveButton(ok, (senderAlert, args) => { Finish(); });

                Dialog dialog = alert.Create();
                dialog.Show();
            }
            catch
            {
            }
        }

        public void ShowMessageDialogFinishApp(string message, string title)
        {
            try
            {
                var ok = GetString(Resource.String.ok);

                // stopProgressDialog();

                alert = new AlertDialog.Builder(this, Resource.Style.AppThemeRain_Dark_Dialog);
                alert.SetTitle(title);
                alert.SetMessage(message);
                alert.SetCancelable(false);
                alert.SetPositiveButton(ok, (senderAlert, args) => { FinishAffinity(); });

                Dialog dialog = alert.Create();
                dialog.Show();
            }
            catch
            {
            }
        }

        public void ShowMessageDialog(string message, string title, string button)
        {
            try
            {
                // stopProgressDialog();

                alert = new AlertDialog.Builder(this, Resource.Style.AppThemeRain_Dark_Dialog);
                alert.SetTitle(title);
                alert.SetMessage(message);
                alert.SetCancelable(false);
                alert.SetPositiveButton(button, (senderAlert, args) => { });

                Dialog dialog = alert.Create();
                dialog.Show();
            }
            catch
            {
            }
        }

        public void ShowProgressBar(string message)
        {
            if (mContext != null)
                HideKeyboard();

            if (progressDialog != null && progressDialog.IsShowing) return;

            var li = LayoutInflater.From(this);
            var myView = li.Inflate(Resource.Layout.progress_layout, null);



            alert = new AlertDialog.Builder(this, Resource.Style.AppThemeRain_Dark_Dialog);
            alert.SetCancelable(false); // if you want user to wait for some process to finish,

            alert.SetView(myView);

            var txt = (TextView)myView.FindViewById(Resource.Id.textMessage);
            txt.Text = message; //.ToLower();
            txt.SetTextColor(Color.White);

            progressDialog = alert.Create();

            progressDialog.SetCancelable(false);

            progressDialog.Show();
        }

        public void StopProgressBar()
        {
            if (progressDialog != null && progressDialog.IsShowing)
            {
                progressDialog.Dismiss();
                progressDialog.Cancel();
            }

            progressDialog = null;
        }

        public bool HasNetworkConnection()
        {
            try
            {
                ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);

                NetworkInfo activeNetwork = connectivityManager.ActiveNetworkInfo;

                if (activeNetwork?.IsConnected ?? false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void ExportBitmapAsPNG(Bitmap bitmap, string path, string name)
        {
            var filePath = Path.Combine(path, name);
            var stream = new FileStream(filePath, FileMode.Create);
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
            stream.Close();
        }

        public static bool validateIDNum(string idNumber)
        {
            if (Debugger.IsAttached)
                return true;

            if (idNumber == null || idNumber.Length != 13)
                return false;

            var id = idNumber.ToCharArray();
            long total_odds = 0;
            long total_even = 0;

            for (int i = 0, j = 100000; i < id.Length - 1; i += 2, j /= 10)
            {
                total_odds += (long)id[i] - 48;
                total_even += ((long)id[i + 1] - 48) * j;
            }

            total_even *= 2;
            var total_all = total_odds;
            while (total_even != 0)
            {
                total_all += total_even % 10;
                total_even /= 10;
            }

            var check_number = (int)(10 - total_all % 10);
            if (check_number % 10 == (long)id[12] - 48)
                return true;
            return false;
        }

        public static string CapitalizeFirst(string s)
        {
            s = s.ToLower();

            var IsNewSentense = true;
            var result = new StringBuilder(s.Length);
            for (var i = 0; i < s.Length; i++)
            {
                if (IsNewSentense && char.IsLetter(s[i]))
                {
                    result.Append(char.ToUpper(s[i]));
                    IsNewSentense = false;
                }
                else
                {
                    result.Append(s[i]);
                }

                if (s[i] == '!' || s[i] == '?' || s[i] == '.') IsNewSentense = true;
            }

            return result.ToString();
        }

        public void HideKeyboard()
        {
            var context = mContext;
            var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null && context is Activity)
            {
                var activity = context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                activity.Window.DecorView.ClearFocus();
            }
        }

        public void SetupMenu(int resName, int resButton)
        {
            //try
            //{
            //    TextView txtName = (TextView)FindViewById(resName);
            //    String str = Commons.getDeviceName();
            //    str = "RICA Application";
            //    txtName.Text = str;
            //}
            //catch
            //{

            //}

            //try
            //{
            //    ImageButton buttonMenu = (ImageButton)FindViewById(resButton);
            //    buttonMenu.Click += delegate
            //    {
            //        PopupMenu menu = new PopupMenu(this, buttonMenu);

            //    // Call inflate directly on the menu:
            //    menu.Inflate(Resource.Menu.menu1);

            //    // A menu item was clicked:
            //    menu.MenuItemClick += (s1, arg1) =>
            //        {

            //            switch (arg1.Item.ItemId)
            //            {
            //                case Resource.Id.item1:

            //                    new AlertDialog.Builder(mContext, Resource.Style.AppThemeRain_Dark_Dialog)
            //                        .SetMessage("Are you sure you want to sign out?")
            //                        .SetPositiveButton("Yes", delegate
            //                        {
            //                            SharedPreferenceManager.GetInstance().Save("AUTH_TOKEN", "");
            //                            SharedPreferenceManager.GetInstance().Save("NORM_USER", "");
            //                            SharedPreferenceManager.GetInstance().Save("NORM_PASSWORD", "");

            //                            Intent intent = new Intent(mContext, typeof(MainActivity));
            //                            intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            //                            StartActivity(intent);

            //                            Finish();
            //                        })
            //                        .SetNegativeButton("No", delegate
            //                        {
            //                        })
            //                        .SetCancelable(false)
            //                        .Show();

            //                    break;

            //                case Resource.Id.itemHome:
            //                    Intent intent = new Intent(mContext, typeof(MenuActivity));
            //                    intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            //                    StartActivity(intent);

            //                    Finish();
            //                    break;

            //                case Resource.Id.itemAbout:
            //                    Intent intent1 = new Intent(mContext, typeof(AboutActivity));
            //                    StartActivity(intent1);

            //                    break;
            //            }
            //        };

            //    // Menu was dismissed:
            //    menu.DismissEvent += (s2, arg2) =>
            //        {
            //            Console.WriteLine("menu dismissed");
            //        };

            //        menu.Show();
            //    };
            //}
            //catch
            //{

            //}


        }
    }
}