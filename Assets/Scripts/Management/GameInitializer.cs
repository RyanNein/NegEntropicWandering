using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : NeinUtility.PersistentSingleton<GameInitializer>
{

	[SerializeField] string[] addativeSceneNames;
	[SerializeField] string nameOfFirstRoomScene;

	static bool hasInitialized = false;

	bool RunningABuild => SceneManager.GetActiveScene().buildIndex == 0;

	protected override void Awake()
	{
		if (hasInitialized)
		{
			Destroy(gameObject);
			return;
		}

		base.Awake();

		InitializeGame();

		// Create addative (Management) Scnees. dont destroy on load
		foreach (var name in addativeSceneNames)
			SceneManager.LoadScene(name, LoadSceneMode.Additive);

		// Check for Init Room and Transition to game
		if (RunningABuild)
			SceneManager.LoadScene(nameOfFirstRoomScene);

		hasInitialized = true;
	}

	void InitializeGame()
	{
		Screen.SetResolution(1280, 720, false);

		Application.targetFrameRate = -1;
	}
}
