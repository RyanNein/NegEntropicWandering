using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class MainMenuFunctionality : MonoBehaviour
{

	[SerializeField] Button continueButton;
	[SerializeField] Button startButton;

	[SerializeField] string startNodeString;

	private void Start()
	{
		bool canContinue = false;

		var node = PlayerPrefs.GetString("currentNode");
		Debug.Log("node : " + node);

		if (node != "Start" && node != "" && node != null)
			canContinue = true;

		continueButton.interactable = canContinue;
	}

	public void StartGame()
	{
		SceneDirector.Instance.StartDialogue(startNodeString);
		startButton.enabled = false;
		continueButton.enabled = false;
	}

	public void ContinueGame()
	{
		var playerName = PlayerPrefs.GetString("playerName");
		var node = PlayerPrefs.GetString("currentNode");

		Debug.Log("name: " + playerName);
		Debug.Log("node: " + node);

		if (playerName == null || playerName == "" || playerName == "Player")
			return;

		var variableStorage = FindObjectOfType<InMemoryVariableStorage>();
		variableStorage.SetValue("$player_name", playerName);

		SceneDirector.Instance.StartDialogue(node);
		
		startButton.enabled = false;
		continueButton.enabled = false;
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void ToggleFullScreen()
	{
		//ResolutionHandler.
		var handler = GameManager.Instance.GetComponent<ResolutionHandler>();
		handler.SetFullscreen(!Screen.fullScreen);
	}

	public void LaunchCreditsLink()
	{
		Application.OpenURL("https://linktr.ee/Ryan_Nein");
	}
}
