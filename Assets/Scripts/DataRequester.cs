using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class DataRequester : MonoBehaviour
{
    private const string BASE_URL = "https://api.open-meteo.com/v1/forecast?";
    private HttpClient _client = new HttpClient();

	private async void Start()
	{
		await GetResponseAsync(Container.SelectedLocationCoords);
	}

	private async Task GetResponseAsync(LocationCoords coords)
    {
		string finalURL = $"{BASE_URL}latitude={coords.Latitude.ToString().Replace(",", ".")}&longitude={coords.Longitude.ToString().Replace(",", ".")}&hourly=temperature_2m&hourly=windspeed_10m&hourly=winddirection_10m";
		string response = await _client.GetStringAsync(finalURL);
		Debug.Log(finalURL);
		Debug.Log(response);

		var data = JsonConvert.DeserializeObject<LocationData>(response); // add event to notify about data received and check for exceptions

	}
}
