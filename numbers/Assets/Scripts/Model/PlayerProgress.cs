﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress
{
	public LevelMode levelMode;
	public int       levelNumber;
	public float	 starPercent;
	public float     bestTime;
	public int       bestTry;
	public bool      cleared;

	public PlayerProgress(LevelMode levelMode, int levelNumber, float starPercent, 
					float bestTime, int bestTry, bool cleared)
	{
		this.levelMode   = levelMode;
		this.levelNumber = levelNumber;
		this.starPercent = starPercent;
		this.bestTime    = bestTime;
		this.bestTry     = bestTry;
		this.cleared     = cleared;
	}
}
