using UnityEngine;

public class PortalEffect : EffectBase
{
	public bool IsEffectActive => _currentDuration > 0;

	private float _currentDuration = 0;

	private void Update()
	{
		if (_currentDuration < 0)
		{
			InvokeEnd();
			return;
		}

		if (_currentDuration > 0)
		{
			_currentDuration -= Time.deltaTime;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out PortalEffectItem _))
		{
			_currentDuration = _effectDuration;
			InvokeRestart(_effectDuration);
		}
	}
}
