using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace RicaAgentApp
{
    internal static class ExtensionMethods
    {
        public static Uri Append(this Uri @this, params string[] paths)
        {
            return new Uri(paths.Aggregate(@this.AbsoluteUri, (current, path) =>
                $"{current.TrimEnd('/')}/{path.TrimStart('/')}"));
        }

        public static bool TryJObjectParse(this string @this, out JObject result)
        {
            if (string.IsNullOrEmpty(@this))
            {
                result = null;

                return false;
            }

            try
            {
                result = JObject.Parse(@this);

                return true;
            }
            catch
            {
                result = null;

                return false;
            }
        }
    }
}
