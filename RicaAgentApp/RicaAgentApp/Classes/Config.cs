using Newtonsoft.Json;
using System;

namespace RicaAgent.Classes
{
    public class Config
    {
        [JsonProperty("name")]
        public String name;
        [JsonProperty("value")]
        public String value;

        public Config()
        {
        }
    }
}