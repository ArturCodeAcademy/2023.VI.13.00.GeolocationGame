using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DataRequester))]
public class LevelGenerator : MonoBehaviour
{
	public List<LevelColumn> LevelColumns { get; private set; } = new List<LevelColumn>();

	[SerializeField] private LevelColumn _columnPrefab;
	
	[Space(5)]
	[SerializeField, Min(1)] private float _minInterval;
	[SerializeField, Min(1)] private float _maxInterval;
	[SerializeField, Min(1)] private float _maxHeight;
	[SerializeField, Min(1)] private float _minHeight;
	[SerializeField] private Gradient _temperatureColorGradient;

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

			LevelColumns.Add(column);
		}

		float posY = LevelColumns[0].GetComponent<SpriteRenderer>().size.y;
		_player.transform.position = new Vector3(0, posY);
	}
}
