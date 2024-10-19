using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public class DialogueController : MonoBehaviour
{

	[SerializeField] LineView lineView;

	[SerializeField] List<AudioClip> voiceClips;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (!NeinUtility.Utility.IsMouseOverUI) return;
			NextPage();
		}
	}


	// Assigned in Menu to Line View
	public void DoVoice()
	{
		/*
		if (!SceneDirector.Instance.IsDoingVoice) return;

		var clip = voiceClips[Random.Range(0, voiceClips.Count)];
		AudioManager.Instance.PlaySFXOneShot(clip);
		*/
	}

	void NextPage()
	{
		lineView.OnContinueClicked();
	}


}
