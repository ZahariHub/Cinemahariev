using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IMDb
{
	public class TomHanksMovies_Recommendations
	{
		[JsonProperty("filmography")] public List<FilmographyItem> Filmography { get; set; }
	}
}