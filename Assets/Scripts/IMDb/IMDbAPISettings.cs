using System.Collections.Generic;

namespace IMDb
{
	public static class IMDbAPISettings
	{
		public static readonly Dictionary<string, string> RequestHeaders = new Dictionary<string, string>
        {
        	{"x-rapidapi-key", "9095b7c43fmshd384ccd4e7f2f39p1abe2ajsnb6820feaeb1d"},
        	{"x-rapidapi-host", "imdb8.p.rapidapi.com"}
        };
	}
}