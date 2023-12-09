using UnityEngine;

public class IncreaseSize : EffectBase
{
	[SerializeField, Min(0)] private float _effectDuration = 2;
	[SerializeField, Range(0.1f, 5)] private float _sizeChangeSpeed = 1;
	[SerializeField, Min(1)] private float _maxSize = 4;

	private float _currentDuration = 0;
	private float _effectSize = 1;
	private float _currentSize = 1;

	private void Update()
	{
		if (_currentDuration < 0 && _currentSize > 1)
		{
			_currentSize -= _sizeChangeSpeed * Time.deltaTime;
			_currentSize = Mathf.Max(_currentSize, 1);
			transform.localScale = Vector3.one * _currentSize;
			InvokeEnd();
			return;
		}

		if (_currentDuration > 0)
		{
			_currentDuration -= Time.deltaTime;
			if (_currentSize < _effectSize)
			{
				_currentSize += _sizeChangeSpeed * Time.deltaTime;
				_currentSize = Mathf.Min(_currentSize, _effectSize);
				transform.localScale = Vector3.one * _currentSize;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out IncreaseSizeItem _))
		{
			_effectSize = Mathf.Min(_effectSize * 2, _maxSize);
			_currentDuration = _effectDuration;
			InvokeRestart(_effectDuration);
		}
	}
}
