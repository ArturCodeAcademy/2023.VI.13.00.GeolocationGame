using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	public bool Active { get; private set; } = true;
	[field:SerializeField, Min(0)] public float MaxVelocity { get; set; }
	[field:SerializeField, Min(0)] public float MaxDistance { get; set; }

	public event Action OnStartDrag;
	public event Action OnDrag;
	public event Action OnRelease;
	public event Action OnLand;

    private Rigidbody2D _rb;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void OnMouseDown()
	{
		if (!Active)
			return;

		OnStartDrag?.Invoke();
	}

	private void OnMouseDrag()
	{
		if (!Active)
			return;

		OnDrag?.Invoke();
	}

	private void OnMouseUp()
	{
		if (!Active)
			return;

		_rb.AddForce(GetDirection() * CalculateForce(), ForceMode2D.Impulse);
		Active = false;
		OnRelease?.Invoke();
	}

	public Vector2 GetDirection()
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return ((Vector2)transform.position - mousePos).normalized;
	}

	public float CalculateForce()
	{
		float distance = Vector2.Distance(transform.position,
									Camera.main.ScreenToWorldPoint(Input.mousePosition));
		distance = Mathf.Clamp(distance, 0, MaxDistance);
		float force = Mathf.Lerp(0, MaxVelocity, distance / MaxDistance);
		return force;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.contacts[0].point.y < transform.position.y)
		{
			Active = true;
			OnLand?.Invoke();
		}
	}
}
