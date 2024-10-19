using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{

	Animator animator;

	[SerializeField] float minInterval;
	[SerializeField] float maxInterval;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		StartCoroutine(Blinking());
		
	}


	IEnumerator Blinking()
	{
		while (true)
		{
			var interval = Random.Range(minInterval, maxInterval);
			animator.SetBool("Blink", true);
			yield return new WaitForSeconds(2f);

			animator.SetBool("Blink", false);
			yield return new WaitForSeconds(interval);
		}
	}

}
