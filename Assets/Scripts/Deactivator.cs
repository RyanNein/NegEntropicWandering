using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
	private void Start()
	{
		gameObject.SetActive(false);
	}
}
