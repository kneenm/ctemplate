using Android.App;
using Android.Content;
using Newtonsoft.Json;

namespace RicaAgentApp.Droid.Common
{
    internal class SharedPreferenceManager
    {
        private static readonly SharedPreferenceManager ourInstance = new SharedPreferenceManager();
        private Activity activity;

        public static SharedPreferenceManager GetInstance()
        {
            return ourInstance;
        }

        public void SetActivity(Activity activity)
        {
            this.activity = activity;
        }

        public void Save(string key, string value)
        {
            var sharedPref = activity.GetPreferences(FileCreationMode.Private);
            var editor = sharedPref.Edit();
            editor.PutString(key, value);
            editor.Commit();
        }

        public void SaveObject(string key, object value)
        {
            var sharedPref = activity.GetPreferences(FileCreationMode.Private);
            var editor = sharedPref.Edit();

            var json = JsonConvert.SerializeObject(value);

            editor.PutString(key, json);
            editor.Commit();
        }

        public string Get(string key)
        {
            return Get(key, "");
        }

        public string Get(string key, string default_value)
        {
            var sharedPref = activity.GetPreferences(FileCreationMode.Private);
            return sharedPref.GetString(key, default_value);
        }
    }
}