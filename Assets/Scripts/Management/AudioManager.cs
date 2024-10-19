using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;


[RequireComponent(typeof(AudioSource))]
public class AudioManager : NeinUtility.PersistentSingleton<AudioManager>
{

	[SerializeField] AudioMixer myMixer;
	[SerializeField] AudioSource musicSource;
	List<AudioSource> sources = new List<AudioSource>();

	[SerializeField] float audioFadeSpeed;

	public enum AudioGroups : short
	{
		MasterGroup,
		MusicGroup,
		SfxGroup,
		VoiceGroup
	}


	#region MIXER AND GROUPS

	[SerializeField] private AudioMixerGroup _masterGroup;
	public AudioMixerGroup MasterGroup => _masterGroup;

	[SerializeField] AudioMixerGroup _musicGroup;
	public AudioMixerGroup MusicGroup => _musicGroup;

	[SerializeField] private AudioMixerGroup _sfxGroup;
	public AudioMixerGroup SfxGroup => _sfxGroup;

	[SerializeField] private AudioMixerGroup _VocalGroup;
	public AudioMixerGroup VocalGroup => _VocalGroup;

	public float CurrentMasterVolume
	{
		get
		{
			float volume;
			myMixer.GetFloat("MasterVolume", out volume);
			return volume;
		}
	}

	public float CurrentMusicVolume
	{
		get
		{
			float volume;
			myMixer.GetFloat("MusicVolume", out volume);
			return volume;
		}
	}

	public float CurrentSfxVolume
	{
		get
		{
			float volume;
			myMixer.GetFloat("SfxVolume", out volume);
			return volume;
		}
	}

	#endregion


	const int MAX_SOURCES = 10;
	const float DEFAULT_VOLUME = .9f;

	float volumeOnPause;
	float volumeDuringPause = 0.25f;



	Coroutine fadeRoutine;


	protected override void Awake()
	{
		base.Awake();
	}

	private void OnEnable()
	{
		SceneLoader.OnFadeOutStart += FadeOutMusic;
		SceneLoader.OnSceneStart += FadeInMusic;
	}

	private void OnDisable()
	{
		SceneLoader.OnFadeOutStart -= FadeOutMusic;
		SceneLoader.OnSceneStart -= FadeInMusic;
	}

	public void PlayMusic(AudioClip clip, float volume = 1f)
	{
		if (fadeRoutine != null)
			StopCoroutine(fadeRoutine);
		musicSource.clip = clip;
		musicSource.outputAudioMixerGroup = MusicGroup;
		musicSource.volume = volume;
		musicSource.loop = true;
		musicSource.Play();
	}

	public void StopMusic()
	{
		// musicSource.Stop();

		musicSource.Pause();
	}

	public void RestartMusic()
	{
		if (musicSource.clip != null)
			musicSource.Play();
	}

	public void PlaySFXOneShot(AudioClip clip, float volume = 1f)
	{
		PlayShot(clip, VocalGroup, volume);
	}

	public void PlayVoice(AudioClip clip, float volume = 1f)
	{
		PlayShot(clip, VocalGroup, volume);
	}

	void PlayShot(AudioClip clip, AudioMixerGroup _group, float volume = 1f)
	{
		bool played = false;

		// Find unused source:
		for (int i = 0; i < sources.Count; i++)
		{
			var source = sources[i];
			if (!source.isPlaying)
			{
				source.outputAudioMixerGroup = _group;
				source.PlayOneShot(clip, volume);
				played = true;
				break;
			}
		}

		// Make new source:
		if (!played && sources.Count < MAX_SOURCES)
		{
			var newSource = gameObject.AddComponent<AudioSource>();
			newSource.outputAudioMixerGroup = _group;
			newSource.PlayOneShot(clip, volume);
			sources.Add(newSource);
		}

	}

	void FadeOutMusic()
	{
		fadeRoutine = StartCoroutine(FadeOut());
	}

	void FadeInMusic()
	{
		fadeRoutine = StartCoroutine(FadeIn());
	}

	IEnumerator FadeOut()
	{
		float volume;
		myMixer.GetFloat("MusicVolume", out volume);


		while (volume > -80f)
		{
			var newvolume = volume - audioFadeSpeed;
			myMixer.SetFloat("MusicVolume", newvolume);
			volume = newvolume;
			yield return new WaitForEndOfFrame();
		}
	}


	IEnumerator FadeIn()
	{
		float volume = -80f;
		myMixer.SetFloat("MusicVolume", volume);

		while (volume <= 0f)
		{
			var newvolume = volume + audioFadeSpeed;
			myMixer.SetFloat("MusicVolume", newvolume);
			volume = newvolume;
			if (volume > 0f) myMixer.SetFloat("MusicVolume", 0f);
			yield return new WaitForEndOfFrame();
		}
	}

	public void SetGroupVolume(AudioGroups group, float volume)
	{
		string parameterName = "MasterVolume";

		switch (group)
		{
			case AudioGroups.MasterGroup:
				parameterName = "MasterVolume";
				break;
			case AudioGroups.MusicGroup:
				parameterName = "MusicVolume";
				break;
			case AudioGroups.SfxGroup:
				parameterName = "SfxVolume";
				break;
		}

		myMixer.SetFloat(parameterName, volume);
	}

	public float GetGroupVolume(AudioGroups group)
	{
		string parameterName = "MasterVolume";
		float currentVolume;

		switch (group)
		{
			case AudioGroups.MasterGroup:
				parameterName = "MasterVolume";
				break;
			case AudioGroups.MusicGroup:
				parameterName = "MusicVolume";
				break;
			case AudioGroups.SfxGroup:
				parameterName = "SfxVolume";
				break;
		}

		myMixer.GetFloat(parameterName, out currentVolume);
		return currentVolume;
	}

}
