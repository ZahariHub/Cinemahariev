using System;
using System.Collections;
using System.Collections.Generic;
using IMDb;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TopMoviesMenu : MonoBehaviour
{
	[SerializeField] private int maxTitles = 10;
	[SerializeField] private TMP_Dropdown genreDropdown;
	[SerializeField] private GameObject resultsContainer;
	[SerializeField] private MovieResultUI movieResultPrefab;

	private IEnumerator FindTopMoviesCoroutine(string movieGenre)
	{
		List<string> titleIDs = new List<string>();
		yield return SendIMDBRequest
		(
			"https://imdb8.p.rapidapi.com/title/get-popular-movies-by-genre?genre=/chart/popular/genre/" + movieGenre.ToLower(),
			request => titleIDs = JsonConvert.DeserializeObject<List<string>>(request.downloadHandler.text)
		);
		
		if (titleIDs.Count > 0)
		{
			resultsContainer.SetActive(true);

			foreach (Transform oldResult in resultsContainer.transform)
			{
				Destroy(oldResult.gameObject);
			}
		}
		
		int moviesCount = Math.Min(maxTitles, titleIDs.Count);
		
		for (int i = 0; i < moviesCount; ++i)
		{
			TitleDetailsQuery titleDetailsQuery = null;
			const string titleIdentifier = "/title/";
			yield return SendIMDBRequest
			(
				"https://imdb8.p.rapidapi.com/title/get-details?tconst=" + titleIDs[i].Replace(titleIdentifier, string.Empty),
				request => titleDetailsQuery = JsonConvert.DeserializeObject<TitleDetailsQuery>(request.downloadHandler.text)
			);

			if (titleDetailsQuery == null)
			{
				continue;
			}
			
			MovieResultUI movieResultUI = Instantiate(movieResultPrefab, resultsContainer.transform);
			movieResultUI.SetMovieData(titleDetailsQuery);
			
		}


	}

	private IEnumerator SendIMDBRequest(string URI, Action<UnityWebRequest> callback)
	{
		using (UnityWebRequest request = UnityWebRequest.Get(URI))
		{
			foreach (KeyValuePair<string, string> requestHeader in IMDbAPISettings.RequestHeaders)
			{
				request.SetRequestHeader(requestHeader.Key, requestHeader.Value);
			}

			yield return request.SendWebRequest();

			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogWarning($"Request {URI} failed: ({request.result})!");
				yield break;
			}

			callback(request);
		}
	}

	public void FindTopMovies(int genreIndex)
	{
		if (genreIndex == 0) // Placeholder "Genres" text
		{
			return;
		}
		
		StartCoroutine(FindTopMoviesCoroutine(genreDropdown.options[genreIndex].text));
	}
}
