using System;
using Newtonsoft.Json;

namespace WebApi.Models.Token
{
    public class Authorization
    {
        public string[] Scopes { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}