using System;
using System.Collections;
using System.Collections.Generic;
using IMDb;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class FindMovieMenu : MonoBehaviour
{
	private static readonly Dictionary<string, string> requestHeaders = new Dictionary<string, string>
	{
		{"x-rapidapi-key", "9095b7c43fmshd384ccd4e7f2f39p1abe2ajsnb6820feaeb1d"},
		{"x-rapidapi-host", "imdb8.p.rapidapi.com"}
	};

	[SerializeField] private GameObject resultsContainer;
	[SerializeField] private MovieResultUI movieResultPrefab;

	private void OnEnable()
	{
		resultsContainer.SetActive(false);
	}

	private IEnumerator FindMoveResultsCoroutine(string movieName)
	{
		TitleFindQuery titleFindQuery = null;
		yield return SendIMDBRequest
		(
			"https://imdb8.p.rapidapi.com/title/find?q=" + movieName,
			request => titleFindQuery = JsonConvert.DeserializeObject<TitleFindQuery>(request.downloadHandler.text)
		);

		if (titleFindQuery == null)
		{
			yield break;
		}

		if (titleFindQuery.Results.Count > 0)
		{
			resultsContainer.SetActive(true);

			foreach (Transform oldResult in resultsContainer.transform)
			{
				Destroy(oldResult.gameObject);
			}
		}

		foreach (Result result in titleFindQuery.Results)
		{
			const string titleIdentifier = "/title/";

			// This is not a title - possibly an actor's name
			if (!result.Id.Contains(titleIdentifier))
			{
				continue;
			}
			
			// TODO: Filter out things that are not movies/tv-shows/videos

			TitleDetailsQuery titleDetailsQuery = null;
			yield return SendIMDBRequest
			(
				"https://imdb8.p.rapidapi.com/title/get-details?tconst=" + result.Id.Replace(titleIdentifier, string.Empty),
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
			foreach (KeyValuePair<string, string> requestHeader in requestHeaders)
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

	public void FindMovieResults(string movieName)
	{
		StartCoroutine(FindMoveResultsCoroutine(movieName));
	}
}