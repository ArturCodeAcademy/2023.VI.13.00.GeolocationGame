using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _latitudeText;
    [SerializeField] private TMP_InputField _longitudeText;

    [Space(3)]
    [SerializeField] private GameObject _scrollViewContent;
    [SerializeField] private GameObject _prefabButton;

    [Space(3)]
    [SerializeField]
    private LocationCoords[] _cities =
    {
        new LocationCoords() { City = "Moscow", Latitude = 55.7558f, Longitude = 37.6173f },
        new LocationCoords() { City = "London", Latitude = 51.5074f, Longitude = -0.1278f },
    	new LocationCoords() { City = "New York", Latitude = 40.7128f, Longitude = -74.0060f },
        new LocationCoords() { City = "Tokyo", Latitude = 35.6762f, Longitude = 139.6503f },
        new LocationCoords() { City = "Sydney", Latitude = -33.8688f, Longitude = 151.2093f },
        new LocationCoords() { City = "Rio de Janeiro", Latitude = -22.9068f, Longitude = -43.1729f },
        new LocationCoords() { City = "Cape Town", Latitude = -33.9249f, Longitude = 18.4241f },
        new LocationCoords() { City = "Buenos Aires", Latitude = -34.6037f, Longitude = -58.3816f },
        new LocationCoords() { City = "Reykjavik", Latitude = 64.1466f, Longitude = -21.9426f },
        new LocationCoords() { City = "Anchorage", Latitude = 61.2181f, Longitude = -149.9003f },
        new LocationCoords() { City = "Nuuk", Latitude = 64.1750f, Longitude = -51.7380f },
        new LocationCoords() { City = "Longyearbyen", Latitude = 78.2232f, Longitude = 15.6267f },
    };

    private LocationCoords _coords = new LocationCoords();

	private void Awake()
	{
        _prefabButton.SetActive(false);
		foreach (var city in _cities)
        {
			var button = Instantiate(_prefabButton, _scrollViewContent.transform);
            button.SetActive(true);
			button.GetComponentInChildren<TMP_Text>().text = city.City;
			button.GetComponent<Button>().onClick.AddListener(() => OnCityButtonClicked(city));
		}
	}

	private void OnCityButtonClicked(LocationCoords city)
	{
		_coords = city;
        _latitudeText.text = _coords.Latitude.ToString();
        _longitudeText.text = _coords.Longitude.ToString();
	}

	public void OnStartButtonClicked()
	{
		SceneManager.LoadScene("Level");
	}

    public void OnExitButtonClicked()
    {
		Application.Quit();
    }

	public void OnLatitudeEndEdit(string value)
    {
        float.TryParse(value, out float result);
        _coords.Latitude = result;
	}

    public void OnLongitudeEndEdit(string value)
    {
        float.TryParse(value, out float result);
        _coords.Longitude = result;
	}
}
