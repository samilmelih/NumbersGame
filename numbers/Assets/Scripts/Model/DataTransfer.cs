using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for data transfer between scenes.
/// </summary>
public static class DataTransfer
{
	// Menu
	public static int currOpenPanel = 0;
	public static Language language = Language.ENGLISH;

	// Level
	public static int levelNo;
	public static LevelMode levelMode = LevelMode.NONE;
	public static List<Level> readLevels = LevelPickerController.ReadLevels();

	// Settings
	public static float volume = 1f;
	public static float sfxVolume = 1f;
}
