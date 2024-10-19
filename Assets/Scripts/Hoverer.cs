using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverer : MonoBehaviour
{
	Vector2 startPosition;
	[SerializeField] Vector2 travelDistance;
	[SerializeField] [Range(-1, 1)] float movementFactor;

	[SerializeField] float intervalTime;

	const float TAU = Mathf.PI * 2;


	private void Awake()
	{
		startPosition = transform.position;
	}

	private void Update()
	{
		if (Mathf.Approximately(intervalTime, 0))
			return;

		float cycles = Time.time / intervalTime;
		float sin = Mathf.Sin(TAU * cycles);

		// movementFactor = (sin + 1) / 2;
		movementFactor = sin;

		Vector2 offset =  travelDistance * movementFactor;
		transform.position = startPosition + offset;
	}

}
