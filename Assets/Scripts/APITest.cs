using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using IMDb;
using Newtonsoft.Json;
using UnityEngine;

public class APITest : MonoBehaviour
{
 private async void Awake()
 {
  TitleFindQuery titleFindQuery = null;
  
  string titleToFind = "Shrek 2";
  
  var client = new HttpClient();
  var request = new HttpRequestMessage
  {
   Method = HttpMethod.Get,
   RequestUri = new Uri("https://imdb8.p.rapidapi.com/title/find?q=" + titleToFind),
   Headers =
   {
    { "x-rapidapi-key", "9095b7c43fmshd384ccd4e7f2f39p1abe2ajsnb6820feaeb1d" },
    { "x-rapidapi-host", "imdb8.p.rapidapi.com" },
   },
  };
  using (var response = await client.SendAsync(request))
  {
   response.EnsureSuccessStatusCode();
   var body = await response.Content.ReadAsStringAsync();
   titleFindQuery = JsonConvert.DeserializeObject<TitleFindQuery>(body);
  }

  if (titleFindQuery == null)
  {
   Debug.LogWarning("Failed to find any titles!");
   return;
  }

  TitleDetailsQuery titleDetailsQuery = null;
  
  request = new HttpRequestMessage
  {
   Method = HttpMethod.Get,
   RequestUri = new Uri("https://imdb8.p.rapidapi.com/title/get-details?tconst=" + titleFindQuery.Results[0].Id.Replace("/title/", string.Empty)),
   Headers =
   {
    { "x-rapidapi-key", "9095b7c43fmshd384ccd4e7f2f39p1abe2ajsnb6820feaeb1d" },
    { "x-rapidapi-host", "imdb8.p.rapidapi.com" },
   },
  };
  using (var response = await client.SendAsync(request))
  {
   response.EnsureSuccessStatusCode();
   var body = await response.Content.ReadAsStringAsync();
   titleDetailsQuery = JsonConvert.DeserializeObject<TitleDetailsQuery>(body);
  }

  if (titleDetailsQuery == null)
  {
   Debug.LogWarning("Failed to retrieve title details!");
   return;
  }
  
  Debug.Log($"Found details for the {titleDetailsQuery.TitleType} {titleDetailsQuery.Title} from the year {titleDetailsQuery.Year}");
 }
}
