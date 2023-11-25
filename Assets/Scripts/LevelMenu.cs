using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

	private void Start()
	{
		_playerController.OnFallOutOfLevel += OnFallOutOfLevel;
		gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		_playerController.OnFallOutOfLevel -= OnFallOutOfLevel;
	}

	private void OnFallOutOfLevel()
	{
		gameObject.SetActive(true);
	}

	public void Restart()
	{
		SceneManager.LoadScene("Level");
	}

	public void Back()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
