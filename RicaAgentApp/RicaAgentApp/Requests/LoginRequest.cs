using Newtonsoft.Json;

namespace RicaAgent.Requests
{
    internal class LoginRequest
    {
        [JsonProperty("auth")] public LoginRequestAuth Auth { get; set; }

        public static LoginRequest Create(string email, string password)
        {
            return new LoginRequest
            {
                Auth = new LoginRequestAuth
                {
                    Email = email,
                    Password = password
                }
            };
        }
    }
}