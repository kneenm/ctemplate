using Newtonsoft.Json;

namespace RicaAgent.Requests
{
    internal class LoginRequestAuth
    {
        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("password")] public string Password { get; set; }
    }
}