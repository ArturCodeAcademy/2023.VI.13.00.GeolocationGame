using System;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
    [field:SerializeField]
    [field:Min(0)]
    public float EffectDuration { get; }

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
