using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

	public void DoShake(float _duration, float _magnitude)
	{
		StartCoroutine(Shake(_duration, _magnitude));
	}

	IEnumerator Shake(float _duration, float _magnitude)
	{
		Vector3 originalPosition = transform.localPosition;
		float elapsedTime = 0f;

		// Do Shake
		while(elapsedTime < _duration)
		{
			float x = Random.Range(-1f, 1f) * _magnitude;
			float y = Random.Range(-1f, 1f) * _magnitude;
			transform.localPosition = new Vector3(x, y, originalPosition.z);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		transform.localPosition = originalPosition;
	}
}
