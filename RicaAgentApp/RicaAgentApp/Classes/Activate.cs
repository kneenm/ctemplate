using Newtonsoft.Json;
using RicaAgent.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace RainMaker.Classes
{
    public class Activate : BaseResponse
    {
        [JsonProperty("order_line")]
        public String order_line = null;
        [JsonProperty("iccid")]
        public String iccid = null;
        [JsonProperty("action")]
        public String action = null;


        public Activate()
        {

        }
    }
}
