﻿using System.Collections;
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

	public static void ResetProgress()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log("PlayerPref is cleaned");
	}

	
}
