using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace IMDb{ 

    public class Result
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("runningTimeInMinutes")]
        public int RunningTimeInMinutes { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("titleType")]
        public string TitleType { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("principals")]
        public List<Principal> Principals { get; } = new List<Principal>();

        [JsonProperty("nextEpisode")]
        public string NextEpisode { get; set; }

        [JsonProperty("numberOfEpisodes")]
        public int? NumberOfEpisodes { get; set; }

        [JsonProperty("seriesStartYear")]
        public int? SeriesStartYear { get; set; }

        [JsonProperty("seriesEndYear")]
        public int? SeriesEndYear { get; set; }

        [JsonProperty("disambiguation")]
        public string Disambiguation { get; set; }

        [JsonProperty("legacyNameText")]
        public string LegacyNameText { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("knownFor")]
        public List<KnownFor> KnownFor { get; } = new List<KnownFor>();
    }

}