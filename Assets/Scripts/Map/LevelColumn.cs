using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class LevelColumn : MonoBehaviour
{
    private BoxCollider2D _collider;
    private SpriteRenderer _renderer;

    private void Awake()
    {
		_collider = GetComponent<BoxCollider2D>();
		_renderer = GetComponent<SpriteRenderer>();
	}

    public void SetHeight(float height)
    {
		_collider.size = new Vector2(_collider.size.x, height);
		_renderer.size = new Vector2(_renderer.size.x, height);
		transform.position = new Vector3(	transform.position.x,
											height / 2,
											transform.position.z);
	}

	public void SetColor(Color color)
	{
		_renderer.color = color;
	}
}
