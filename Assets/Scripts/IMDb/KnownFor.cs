using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace IMDb{ 

    public class KnownFor
    {
        [JsonProperty("crew")]
        public List<Crew> Crew { get; } = new List<Crew>();

        [JsonProperty("summary")]
        public Summary Summary { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("titleType")]
        public string TitleType { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }
    }

}