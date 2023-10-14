using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class DataRequester : MonoBehaviour
{
    private const string BASE_URL = "https://api.open-meteo.com/v1/forecast?";
    private string _finalURL = string.Empty;
    private string _response = string.Empty;
    private HttpClient _client = new HttpClient();

    private async Task GetResponseAsync(LocationCoords coords)
    {
        _finalURL = $"{BASE_URL}latitude={coords.Latitude}&longitude={coords.Longitude}&hourly=temperature_2m&hourly=windspeed_10m&hourly=winddirection_10m";
		_response = await _client.GetStringAsync(_finalURL);		
	}
}
