using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DataRequester))]
public class LevelGenerator : MonoBehaviour
{
	[SerializeField] private LevelColumn _columnPrefab;

	private DataRequester _dataRequester;

	private void Awake()
	{
		_dataRequester = GetComponent<DataRequester>();
	}

	private IEnumerable Start()
	{
		WaitUntil waitUntil = new WaitUntil(() => _dataRequester.IsDataReady);
		yield return waitUntil;

	}
}
