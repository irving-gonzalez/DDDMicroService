using Newtonsoft.Json;

namespace DDDMicroservice.Application.Providers.Models
{
    public class TokenResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; } = String.Empty;
    }
}