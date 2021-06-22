using Newtonsoft.Json;

namespace IMDb
{
    public class TitleDetailsQuery
    {
        [JsonProperty("@type")] public string Type { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("image")] public Image Image { get; set; }

        [JsonProperty("runningTimeInMinutes")] public int RunningTimeInMinutes { get; set; }

        [JsonProperty("nextEpisode")] public string NextEpisode { get; set; }

        [JsonProperty("numberOfEpisodes")] public int NumberOfEpisodes { get; set; }

        [JsonProperty("seriesEndYear")] public int SeriesEndYear { get; set; }

        [JsonProperty("seriesStartYear")] public int SeriesStartYear { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("titleType")] public string TitleType { get; set; }

        [JsonProperty("year")] public int Year { get; set; }
    }
}