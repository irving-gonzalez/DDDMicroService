using Newtonsoft.Json;

namespace DDDMicroservice.Application.Providers.Models
{
    public class TokenRequest
    {
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; } = String.Empty;
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; } = String.Empty;
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; } = String.Empty;
        [JsonProperty(PropertyName = "grant_type")]
        public string GrantType { get; set; } = String.Empty;
    }
}