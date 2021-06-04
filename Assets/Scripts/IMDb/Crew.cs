using Newtonsoft.Json; 
namespace IMDb{ 

    public class Crew
    {
        [JsonProperty("category")]
        public string Category { get; set; }
    }

}