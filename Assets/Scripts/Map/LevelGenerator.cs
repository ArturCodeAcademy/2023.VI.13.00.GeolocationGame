using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DataRequester))]
public class LevelGenerator : MonoBehaviour
{
	public List<LevelColumn> LevelColumns { get; private set; } = new List<LevelColumn>();

	[SerializeField] private LevelColumn _columnPrefab;
	[SerializeField] private GameObject _ground;
	[SerializeField] private GameObject _stop;
	[SerializeField] private SpriteRenderer _bg;

	[Space(5)]
	[SerializeField, Min(1)] private float _minInterval;
	[SerializeField, Min(1)] private float _maxInterval;
	[SerializeField, Min(1)] private float _maxHeight;
	[SerializeField, Min(1)] private float _minHeight;
	[SerializeField] private Gradient _temperatureColorGradient;

	[Space(5)]
	[SerializeField] private Item[] _itemPrefabs;
	[SerializeField, Range(0, 1)] private float _itemSpawnChance;
	[SerializeField] private float _itemSpawnHeight;

	[Space(5)]
	[SerializeField] private GameObject _player;

	private DataRequester _dataRequester;

	private void Awake()
	{
		_dataRequester = GetComponent<DataRequester>();
	}

	private IEnumerator Start()
	{
		WaitUntil waitUntil = new WaitUntil(() => _dataRequester.IsDataReady);
		yield return waitUntil;
		GenerateLevel();
	}

	private void GenerateLevel()
	{
		float minT = _dataRequester.MeteoDataList.Min(d => d.Temperature);
		float maxT = _dataRequester.MeteoDataList.Max(d => d.Temperature);
		float deltaT = maxT - minT;
		float x = 0;
		foreach (MeteoData data in _dataRequester.MeteoDataList)
		{
			var column = Instantiate(_columnPrefab, transform);
			column.transform.localPosition = new Vector3(x, 0, 0);
			x += Random.Range(_minInterval, _maxInterval);
			float tempPercent = (data.Temperature - minT) / deltaT;
			float height = Mathf.Lerp(_minHeight, _maxHeight, tempPercent);
			column.SetHeight(height);
			column.SetColor(_temperatureColorGradient.Evaluate(tempPercent));
			column.SetInfo(data);

			if (Random.value <= _itemSpawnChance)
			{
				Item item = Instantiate(_itemPrefabs[Random.Range(0, _itemPrefabs.Length)], column.transform);
				item.transform.position = new Vector3(column.transform.position.x, height + _itemSpawnHeight);
			}

			LevelColumns.Add(column);
		}

		float posY = LevelColumns[0].GetComponent<SpriteRenderer>().size.y;
		_player.transform.position = new Vector3(0, posY);

		x = LevelColumns[^1].transform.position.x + 1;
		_ground.transform.localScale = new Vector3(x, 1, 1);
		_stop.transform.position = new Vector3(x, 0);
		_bg.size = new Vector2(x, _bg.size.y);
	}
}
