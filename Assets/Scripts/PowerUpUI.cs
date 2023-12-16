using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    [SerializeField] private EffectBase _effectBase;

    [Space(5)]
    [SerializeField] private GameObject _effectImage;
    [SerializeField] private Image _fill;

    private float _currentDuration = 0;

	private void OnEnable()
	{
		_effectBase.OnEffectRestarted += OnEffectRestarted;
		_effectBase.OnEffectEnded += OnEffectEnded;

		_effectImage.SetActive(false);
	}

	private void OnDisable()
	{
		_effectBase.OnEffectRestarted -= OnEffectRestarted;
		_effectBase.OnEffectEnded -= OnEffectEnded;
	}

	private void OnEffectRestarted(float duration)
	{
		_currentDuration = duration;
		_effectImage.SetActive(true);
	}

	private void OnEffectEnded()
	{
		_effectImage.SetActive(false);
	}

	private void Update()
    {
		if (_currentDuration <= 0)
			return;

		_currentDuration -= Time.deltaTime;
		_fill.fillAmount = _currentDuration / _effectBase.EffectDuration;
	}
}
