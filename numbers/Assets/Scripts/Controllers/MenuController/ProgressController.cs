using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProgressController
{
	// 			KEY										VALUE
	// LEVEL_MODE~LEVEL_NUMBER   STAR_PERCENT~BEST_TIME~BEST_TRY~BEST_COUNT~CLEARED-LOCKED
	//      TIME_AND_TRY~3                 	99.12~30.23~8~5~True~False

	public static void SaveProgress(PlayerProgress progress)
	{
		PlayerProgress lastProgress = GetProgress(progress.levelMode, progress.levelNumber);

		float starPercent = Mathf.Max(lastProgress.starPercent, progress.starPercent);
		float bestTime    = Mathf.Min(lastProgress.bestTime, progress.bestTime);
		int   bestTry     = Mathf.Min(lastProgress.bestTry, progress.bestTry);
		int   bestCount   = Mathf.Max(lastProgress.bestCount, progress.bestCount);
		bool  completed   = progress.completed;
		bool  locked      = progress.locked;

		string progressKey = string.Format(
			"{0}~{1}",
			progress.levelMode,
			progress.levelNumber
		);

		string progressValue = string.Format(
			"{0}~{1}~{2}~{3}~{4}~{5}",
			starPercent,
			bestTime,
			bestTry,
			bestCount,
			completed,
			locked
		);

		PlayerPrefs.SetString(progressKey, progressValue);
	}

	public static PlayerProgress GetProgress(LevelMode levelMode, int levelNumber)
	{
		string progressKey = string.Format("{0}~{1}", levelMode, levelNumber);

		float starPercent = 0.0f;
		float bestTime    = float.MaxValue;
		int   bestTry     = int.MaxValue;
		int   bestCount   = int.MinValue;
		bool  completed   = false;
		bool  locked      = true;

		bool keyExist = PlayerPrefs.HasKey(progressKey);
		if(keyExist == true)
		{
			string progressValue = PlayerPrefs.GetString(progressKey);
			string[] progressArr = progressValue.Split('~');

			starPercent = float.Parse(progressArr[0]);
			bestTime    = float.Parse(progressArr[1]);
			bestTry     = int.Parse(progressArr[2]);
			bestCount   = int.Parse(progressArr[3]);
			completed   = bool.Parse(progressArr[4]);
			locked      = bool.Parse(progressArr[5]);
		}

		PlayerProgress progress = new PlayerProgress(
			levelMode,
			levelNumber,
			starPercent,
			bestTime,
			bestTry,
			bestCount,
			completed,
			locked
		);

		return progress;
	}

	public static bool IsRecordExist(LevelMode levelMode, int levelNumber)
	{
		string progressKey = string.Format("{0}~{1}", levelMode, levelNumber);
		bool completed = false;

		if(PlayerPrefs.HasKey(progressKey) == false)
			return completed;

		string progressValue = PlayerPrefs.GetString(progressKey);
		string[] progressArr = progressValue.Split('~');

		completed = bool.Parse(progressArr[4]);

		return completed;
	}

	public static void SetVolume(float volume)
	{
		PlayerPrefs.SetFloat("Volume", volume);
	}

	public static float GetVolume()
	{
		if(PlayerPrefs.HasKey("Volume") == false)
			return SettingsController.defaultVolume;
		
		return PlayerPrefs.GetFloat("Volume");
	}

	public static void SetSfxVolume(float volume)
	{
		PlayerPrefs.SetFloat("Sfx", volume);
	}

	public static float GetSfxVolume()
	{
		if(PlayerPrefs.HasKey("Sfx") == false)
			return SettingsController.defaultVolume;
		
		return PlayerPrefs.GetFloat("Sfx");
	}

	public static void SetRemainingTime(float remainingTime)
	{
        
		PlayerPrefs.SetFloat("RemainingTime", remainingTime);
	}

	public static float GetRemainingTime()
	{
		if(PlayerPrefs.HasKey("RemainingTime") == false)
			return 10f;

		return PlayerPrefs.GetFloat("RemainingTime");
	}

	public static void ResetProgress()
	{
		PlayerPrefs.DeleteAll();
		DataTransfer.remainingTime = GetRemainingTime();
	}
}
