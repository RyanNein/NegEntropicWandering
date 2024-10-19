using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class SceneDirector : NeinUtility.PersistentSingleton<SceneDirector>
{

	DialogueRunner dialogueRunner;
	GameObject dialogueSystemObject;

	public delegate void DialogueEvents();
	public static event DialogueEvents
		OnDialogueStart,
		OnDialogueEnd;

	public bool IsDoingVoice { get; private set; }

	protected override void Awake()
	{
		base.Awake();
	}

	private void OnEnable()
	{
		dialogueRunner = FindObjectOfType<DialogueRunner>();
		dialogueSystemObject = dialogueRunner.gameObject;

		dialogueRunner.AddCommandHandler<string>("scene", ChangeScene);
		dialogueRunner.AddCommandHandler<bool>("voice", Voice);
		dialogueRunner.AddCommandHandler<string>("sound", PlaySound);
		dialogueRunner.AddCommandHandler<string>("music", PlayMusic);
		dialogueRunner.AddCommandHandler("stop_music", StopMusic);
		dialogueRunner.AddCommandHandler<float, float>("camera_shake", ShakeCamera);
		dialogueRunner.AddCommandHandler<string, bool>("activate", Activate);
		dialogueRunner.AddCommandHandler<string>("fade_out", FadeOut);
		dialogueRunner.AddCommandHandler<string>("save", Save);
		dialogueRunner.AddCommandHandler("clear_save", ClearSave);
	}

	public void StartDialogue(string _nodeName)
	{
		dialogueSystemObject.SetActive(true);
		dialogueRunner.StartDialogue(_nodeName);
	}

	public void EndDialogue()
	{
		dialogueRunner.Stop();
		//dialogueSystemObject.SetActive(false);
	}

	#region COMMANDS

	void ChangeScene(string _newSceneName)
	{
		SceneLoader.Instance.LoadScene(_newSceneName);
	}

	void Voice(bool _doVoice)
	{
		IsDoingVoice = _doVoice;
	}

	void PlaySound(string _soundName)
	{
		AudioClip clip = Resources.Load<AudioClip>("AudioClips/" + _soundName);
		AudioManager.Instance.PlaySFXOneShot(clip);
	}

	void PlayMusic(string _musicName)
	{
		AudioClip clip = Resources.Load<AudioClip>("Music/" + _musicName);
		AudioManager.Instance.PlayMusic(clip);
	}

	void StopMusic()
	{
		AudioManager.Instance.StopMusic();
	}

	void ShakeCamera(float _duration, float _magnitude)
	{
		Camera.main.gameObject.GetComponent<CameraShake>().DoShake(_duration, _magnitude);
	}

	void Activate(string _objectName, bool _doActive)
	{
		var activateObject = GameObject.Find(_objectName);
		if (activateObject == null)
		{
			Debug.LogWarning("Object not found");
			return;
		}

		activateObject.transform.GetChild(0).gameObject.SetActive(_doActive);
	}

	void FadeOut(string _objectName)
	{
		var ParentObject = GameObject.Find(_objectName);
		if (ParentObject == null)
		{
			Debug.LogWarning("Object not found");
			return;
		}

		var fadeObject = ParentObject.transform.GetChild(0);

		var fader = fadeObject.GetComponent<Fader>();
		if (fader == null)
		{
			Debug.LogWarning("no fader on object");
			return;
		}

		fader.DoFadeOut();
	}

	void Save(string _nodeName)
	{
		var variableStorage = FindObjectOfType<InMemoryVariableStorage>();
		string playerName;
		variableStorage.TryGetValue<string>("$player_name", out playerName);

		PlayerPrefs.SetString("playerName", playerName);
		PlayerPrefs.SetString("currentNode", _nodeName);
	}

	void ClearSave()
	{
		PlayerPrefs.SetString("playerName", null);
		PlayerPrefs.SetString("currentNode", null);
	}


	#endregion
}