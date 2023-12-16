using System;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
	[SerializeField, Min(0)] protected float _effectDuration = 2;
	public float EffectDuration => _effectDuration;

    public event Action<float> OnEffectRestarted;
    public event Action OnEffectEnded;

	protected void InvokeRestart(float duration)
	{
		OnEffectRestarted?.Invoke(duration);
	}

    protected void InvokeEnd()
    {
		OnEffectEnded?.Invoke();
	}
}
