

using Android.Bluetooth;
using Android.OS;
using Android.Text;
using Java.Lang;

namespace RicaAgentApp.Droid.Common
{
    public class Commons
    {
        public static bool DEV_MODE = false;

        public static string base64ID = "";
        public static string base64Addr = "";

        public static string ID_NUM = "";

        public static int CANCEL_SIM = 100;
        public static int SPEND_LIMIT_SIM = 101;
        public static int LIST_ORDERS = 102;
        public static int RICA = 103;
        public static string getDeviceName()
        {

            try
            {
                BluetoothAdapter myDevice = BluetoothAdapter.DefaultAdapter;
                string deviceName = capitalize(myDevice.Name);
                return deviceName.ToLower();

            }
            catch (Java.Lang.Exception e)
            {

            }

            string manufacturer = Build.Manufacturer;
            string model = Build.Model;
            if (model.StartsWith(manufacturer))
            {
                return capitalize(model);
            }
            return capitalize(manufacturer) + " " + model;
        }

        private static string capitalize(string str)
        {
            if (TextUtils.IsEmpty(str))
            {
                return str;
            }
            char[] arr = str.ToLower().ToCharArray();
            bool capitalizeNext = true;
            string phrase = "";
            foreach (char c in arr)
            {
                if (capitalizeNext && Character.IsLetter(c))
                {
                    phrase += Character.ToUpperCase(c);
                    capitalizeNext = false;
                    continue;
                }
                else if (Character.IsWhitespace(c))
                {
                    capitalizeNext = true;
                }
                phrase += c;
            }
            return phrase;
        }


    }
}