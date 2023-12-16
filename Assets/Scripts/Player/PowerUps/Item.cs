using UnityEngine;

public class Item : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.TryGetComponent(out PlayerController _))
			Destroy(gameObject);
	}
}
