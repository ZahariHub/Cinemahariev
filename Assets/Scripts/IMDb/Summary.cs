using Newtonsoft.Json; 
namespace IMDb{ 

    public class Summary
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("displayYear")]
        public string DisplayYear { get; set; }
    }

}