using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace IMDb{ 

    public class Principal
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("legacyNameText")]
        public string LegacyNameText { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("billing")]
        public int Billing { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("characters")]
        public List<string> Characters { get; } = new List<string>();

        [JsonProperty("roles")]
        public List<Role> Roles { get; } = new List<Role>();

        [JsonProperty("disambiguation")]
        public string Disambiguation { get; set; }

        [JsonProperty("as")]
        public string As { get; set; }

        [JsonProperty("attr")]
        public List<string> Attr { get; } = new List<string>();

        [JsonProperty("endYear")]
        public int? EndYear { get; set; }

        [JsonProperty("episodeCount")]
        public int? EpisodeCount { get; set; }

        [JsonProperty("startYear")]
        public int? StartYear { get; set; }
    }

}