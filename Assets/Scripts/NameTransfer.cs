using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;

public class NameTransfer : MonoBehaviour
{

	public string playerName;
	public TMP_InputField inputText;

	[YarnCommand("select_field")]
	public void SelectTextField()
	{
		inputText.Select();
	}

	public void StoreName()
	{	
		playerName = inputText.text;
		print(playerName);

		if (playerName == "")
			return;

		var variableStorage = FindObjectOfType<InMemoryVariableStorage>();
		variableStorage.SetValue("$player_name", playerName);


		var activateObject = GameObject.Find("Continue Button");
		if (activateObject == null)
		{
			Debug.LogWarning("Object not found");
			return;
		}
		activateObject.transform.GetChild(0).gameObject.SetActive(true);
	}


}
