using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IMDb
{
	public class FilmographyItem
	{
		[JsonProperty("id")] public string Id { get; set; }

		[JsonProperty("status")] public string Status { get; set; }
		
		[JsonProperty("titleType")] public string TitleType { get; set; }
	}
}