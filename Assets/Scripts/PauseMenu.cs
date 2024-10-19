using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : NeinUtility.PersistentSingleton<PauseMenu>
{

	[SerializeField] GameObject pauseObject;
	public bool isPaused;

	private void OnEnable()
	{
		SceneLoader.OnSceneStart += ExitPauseMenu;
	}

	private void OnDisable()
	{
		SceneLoader.OnSceneStart -= ExitPauseMenu;
	}

	private void Update()
	{
		if (SceneLoader.Instance.IsFading)
			return;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isPaused)
			{
				var pauseMenu = FindObjectOfType<PauseMenu>();
				pauseMenu.ExitPauseMenu();
				
			}
			else
			{
				if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu")
					return;

				pauseObject.SetActive(true);
				Time.timeScale = 0f;
				isPaused = true;
			}
		}
	}

	public void ToggleFullScreen()
	{
		//ResolutionHandler.
		var handler = GameManager.Instance.GetComponent<ResolutionHandler>();
		handler.SetFullscreen(!Screen.fullScreen);
	}

	public void ExitGame()
	{
		ExitPauseMenu();

		SceneDirector.Instance.EndDialogue();
		SceneDirector.Instance.StartDialogue("Return");

		// SceneLoader.Instance.LoadScene("MainMenu");
	}

	public void ExitPauseMenu()
	{
		Time.timeScale = 1f;
		isPaused = false;
		pauseObject.SetActive(false);
	}

}
