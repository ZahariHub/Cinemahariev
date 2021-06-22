using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace IMDb{ 

    public class TitleFindQuery
    {
        [JsonProperty("@meta")]
        public Meta Meta { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; } = new List<Result>();

        [JsonProperty("types")]
        public List<string> Types { get; } = new List<string>();
    }

}