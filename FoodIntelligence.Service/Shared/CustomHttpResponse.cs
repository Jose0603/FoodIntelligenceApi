using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace FoodIntelligence.Service.Shared
{
    public class CustomHttpResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public dynamic Data { get; set; }

        [JsonProperty("errors")]
        public List<string> Errors { get; set; }

        [JsonProperty("status_code")]
        public HttpStatusCode StatusCode { get; set; }

        public CustomHttpResponse()
        {
            this.Success = false;
            this.Message = null;
            this.Data = null;
            this.Errors = new List<string>();
            this.StatusCode = HttpStatusCode.Conflict;
        }

    }
}
