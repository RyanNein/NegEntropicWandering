using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using TMPro;

public class NameColorSetter : MonoBehaviour
{

	TextMeshProUGUI text;

	string previousText = "";

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{

		if (text.text != previousText)
		{

		}

		previousText = text.text;
	}

	string SetColor()
	{
		Debug.Log("set color");
		return "";
	}
}
