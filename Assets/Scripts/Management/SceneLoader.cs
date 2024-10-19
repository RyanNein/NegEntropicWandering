using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : NeinUtility.PersistentSingleton<SceneLoader>
{

	public float fadeOutTimeInSeconds = 1f;


	Animator fadeAnimator;

	private bool isFading;
	public bool IsFading
	{
		get { return isFading; }
		private set { isFading = value; }
	}

	public delegate void SceneEvents();
	public static event SceneEvents
		OnSceneStart,
		OnFadeOutStart,
		OnFadeInEnd;


	public string CurrentScene
	{
		get
		{
			var currentScene = SceneManager.GetActiveScene();
			var name = currentScene.name;
			return name;
		}
	}

	protected override void Awake()
	{
		base.Awake();

		fadeAnimator = GetComponentInChildren<Animator>();
	}


	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}



	public void LoadScene(string _sceneName)
	{
		StartCoroutine(ExitSceneTransition(_sceneName));
	}





	IEnumerator ExitSceneTransition(string _sceneName)
	{
		fadeAnimator.Play("CrossfadeExit");
		IsFading = true;
		OnFadeOutStart?.Invoke();
		yield return new WaitForSeconds(fadeOutTimeInSeconds);

		SceneManager.LoadScene(_sceneName);
	}

	IEnumerator EnterFadeWait()
	{
		fadeAnimator.Play("CrossfadeEnter");
		IsFading = true;

		yield return new WaitForSeconds(fadeOutTimeInSeconds);
		IsFading = false;
		OnFadeInEnd?.Invoke();
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		OnSceneStart?.Invoke();
		StartCoroutine(EnterFadeWait());
	}
}
