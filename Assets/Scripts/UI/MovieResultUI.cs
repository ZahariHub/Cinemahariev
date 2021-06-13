using System.Collections;
using System.Collections.Generic;
using IMDb;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Image = UnityEngine.UI.Image;

public class MovieResultUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI title;
	[SerializeField] private TextMeshProUGUI year;
	[SerializeField] private TextMeshProUGUI titleType;
	[SerializeField] private TextMeshProUGUI duration;
	[SerializeField] private Image moviePoster;
	public void SetMovieData(TitleDetailsQuery movieData)
	{
		title.text = movieData.Title;
		year.text = movieData.Year.ToString();
		titleType.text = movieData.TitleType;
		duration.text = movieData.RunningTimeInMinutes.ToString()+" min.";

		if (movieData.Image != null)
		{
			StartCoroutine(DownloadImageCoroutine(movieData.Image.Url));
		}
		else
		{
			moviePoster.gameObject.SetActive(false);
		}
	}

	private IEnumerator DownloadImageCoroutine(string imageURL)
	{
		using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL))
		{
			yield return request.SendWebRequest();
			Texture2D texture = DownloadHandlerTexture.GetContent(request);
			moviePoster.sprite = Sprite.Create(texture, new Rect(0,0,texture.width,texture.height),new Vector2(0.5f,0.5f));
		}
	}
}
