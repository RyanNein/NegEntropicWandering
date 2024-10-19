using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
	[SerializeField] float fadeSpeed;

	float alpha = 1f;

	SpriteRenderer spriteRenderer;


	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnEnable()
	{
		StartCoroutine(FadeIn());
	}

	IEnumerator FadeIn()
	{
		var color = spriteRenderer.color;
		alpha = 0f;
		color.a = alpha;
		spriteRenderer.color = color;

		while(alpha <= 1f)
		{
			alpha += Time.deltaTime * fadeSpeed;
			color.a = alpha;
			spriteRenderer.color = color;
			yield return new WaitForEndOfFrame();
		}
		
		yield return null;
	}

	
	public void DoFadeOut()
	{
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut()
	{
		var color = spriteRenderer.color;
		alpha = 1f;
		color.a = alpha;
		spriteRenderer.color = color;

		while (alpha >= 0f)
		{
			alpha -= Time.deltaTime * fadeSpeed;
			color.a = alpha;
			spriteRenderer.color = color;
			yield return new WaitForEndOfFrame();
		}

		yield return null;
	}
}
