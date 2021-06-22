using System;
using System.Collections;
using System.Collections.Generic;
using IMDb;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
	public class RecommendationsMenu : MonoBehaviour
	{
		[SerializeField] private Button getRandomRecommendationButton;
		[SerializeField] private TextMeshProUGUI loadingLabel;
		[SerializeField] private GameObject resultsContainer;
		[SerializeField] private MovieResultUI movieResultPrefab;

		private TomHanksMovies_Recommendations recommendations = null;
		
		private void OnEnable()
		{
			resultsContainer.SetActive(false);

			if (recommendations == null)
			{
				getRandomRecommendationButton.interactable = false;
				loadingLabel.gameObject.SetActive(true);
				StartCoroutine(GetRecommendationsCoroutine());
			}
		}
		
		private IEnumerator GetRecommendationsCoroutine()
		{
			yield return SendIMDBRequest
			(
				"https://imdb8.p.rapidapi.com/actors/get-all-filmography?nconst=nm0000158",
				request => recommendations = JsonConvert.DeserializeObject<TomHanksMovies_Recommendations>(request.downloadHandler.text)
			);
			
			if (recommendations == null)
			{
				yield break;
			}

			for (int i = recommendations.Filmography.Count - 1; i >= 0; --i)
			{
				FilmographyItem filmographyItem = recommendations.Filmography[i];
				if ((filmographyItem.Status != null && filmographyItem.Status.ToLower() != "released") || (filmographyItem.TitleType != null && filmographyItem.TitleType.ToLower() != "movie"))
				{
					recommendations.Filmography.RemoveAt(i);
				}
			}

			getRandomRecommendationButton.interactable = true;
			loadingLabel.gameObject.SetActive(false);
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

		private IEnumerator GetRandomRecommendationCoroutine(string titleID)
		{
			const string titleIdentifier = "/title/";

			// This is not a title - possibly an actor's name
			if (!titleID.Contains(titleIdentifier))
			{
				yield break;
			}
		
			// TODO: Filter out things that are not movies/tv-shows/videos

			TitleDetailsQuery titleDetailsQuery = null;
			yield return SendIMDBRequest
			(
				"https://imdb8.p.rapidapi.com/title/get-details?tconst=" + titleID.Replace(titleIdentifier, string.Empty),
				request => titleDetailsQuery = JsonConvert.DeserializeObject<TitleDetailsQuery>(request.downloadHandler.text)
			);

			if (titleDetailsQuery == null)
			{
				yield break;
			}
			
			resultsContainer.SetActive(true);

			foreach (Transform oldResult in resultsContainer.transform)
			{
				Destroy(oldResult.gameObject);
			}
		
			MovieResultUI movieResultUI = Instantiate(movieResultPrefab, resultsContainer.transform);
			movieResultUI.SetMovieData(titleDetailsQuery);
		}
		
		public void GetRandomRecommendation()
		{
			if (recommendations == null)
			{
				return;
			}

			if (recommendations.Filmography.Count == 0)
			{
				return;
			}
			
			int randomMovieIndex = Random.Range(0, recommendations.Filmography.Count);
			FilmographyItem randomMovie = recommendations.Filmography[randomMovieIndex];

			StartCoroutine(GetRandomRecommendationCoroutine(randomMovie.Id));
		}
	}
}