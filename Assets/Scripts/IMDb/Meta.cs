using Newtonsoft.Json; 
namespace IMDb{ 

    public class Meta
    {
        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("serviceTimeMs")]
        public double ServiceTimeMs { get; set; }
    }

}