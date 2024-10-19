using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayStarter : MonoBehaviour
{
	[SerializeField] string NodeName;

	private void Start()
	{
		//FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue(NodeName);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue(NodeName);
		}
	}
}
