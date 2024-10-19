using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDeactivator : MonoBehaviour
{

	[SerializeField] GameObject background;

	private void OnEnable()
	{
		SceneLoader.OnSceneStart += BackgroundDisableCheck;
	}

	private void OnDisable()
	{
		SceneLoader.OnSceneStart -= BackgroundDisableCheck;
	}

	void BackgroundDisableCheck()
	{
		var scene = SceneLoader.Instance.CurrentScene;
		Debug.Log("scene: " + scene);
		if (scene == "MainMenu")
		{
			background.SetActive(false);
		}
		else
		{
			background.SetActive(true);
		}
	}

}
