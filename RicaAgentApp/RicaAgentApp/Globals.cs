using System;

namespace RicaAgent
{
    public class Globals
    {
        public static String KEY = "6eef45fa-4fe4-4fb8-b80b-540d6e5b2b44";

        public static String KEY_ORDERS = "81e88785-2a1f-412b-9df2-45ff4f9ed6bb";

        public static String API_KEY = "xZMkdbTSuvPRw-gwLfmlNrZm8Rs-jha0lMn58keAI";

        public const string KEY_ICCID = "xEr4slh6wdngI-imBb9t8sm4JLY-e1yQz7Tmo7S2A";


        public const int ResponseCodeError = 99;
        public const int ResponseCodeSuccess = 0;
        public static TimeSpan Timeout = new TimeSpan(0, 5, 0);


        public static String API_URL = "https://api-sit.rain.co.za/";
        //        public static String API_URL = "https://api-ppd.rain.co.za/";
        // public static String API_URL = "https://api.rain.co.za/";

        public static Uri RestApiUri = new Uri(API_URL);

        public static Uri IccidUri = new Uri(RestApiUri, "/iccid");

    }
}
