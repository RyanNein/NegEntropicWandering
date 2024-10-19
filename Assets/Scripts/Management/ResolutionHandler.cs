using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionHandler : MonoBehaviour
{

	public bool IsFullscreen => Screen.fullScreen;

	Resolution[] allResolutions;
	int resolutionIndex = 0;

	int resolutionHeight;
	int resolutionWidth;

	DisplayInfo previousDisplayInfo;
	DisplayInfo currentDisplayInfo;
	bool ScreenChanged = false;

	private void Awake()
	{
		allResolutions = Screen.resolutions;
		currentDisplayInfo = Screen.mainWindowDisplayInfo;
		previousDisplayInfo = currentDisplayInfo;
	}

	private void Start()
	{
		SetResolution(960, 720);
	}

	private void Update()
	{
		// HandleDisplayChange();

		// Fullscreen
		/*
		if (Input.GetKeyDown(KeyCode.F))
			SetFullscreen(!IsFullscreen);
		*/

		/*
		#region TEMP
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Screen.SetResolution(960, 720, false);
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			Screen.SetResolution(960, 720, true);
		}
		#endregion
		*/
	}

	public void SetFullscreen(in bool _doFullscreen)
	{
		Screen.fullScreen = _doFullscreen;
		/*
		if (_doFullscreen)
			Screen.SetResolution(1280, 720, FullScreenMode.ExclusiveFullScreen);
		else
			Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
		*/
	}

	void SetResolution(in int _width, in int _height)
	{
		Screen.SetResolution(_width, _height, IsFullscreen);
		resolutionHeight = _height;
		resolutionWidth = _width;
	}


	//TODO check for changes to resolution and maintain ratio
	void HandleDisplayChange()
	{
		currentDisplayInfo = Screen.mainWindowDisplayInfo;
		if (!currentDisplayInfo.Equals(previousDisplayInfo)) ScreenChanged = true;

		if (Input.GetMouseButtonUp(0) && ScreenChanged)
		{
			Screen.MoveMainWindowTo(currentDisplayInfo, Vector2Int.zero);

			Screen.SetResolution(resolutionWidth, resolutionHeight, IsFullscreen);
			ScreenChanged = false;
		}
		
		previousDisplayInfo = currentDisplayInfo;
	}

}
