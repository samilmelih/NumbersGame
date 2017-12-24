using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress
{
	public LevelMode levelMode;
	public int       levelNumber;
	public float	 starPercent;
	public float     bestTime;
	public int       bestTry;
	public int       bestCount;
	public bool      completed;
	public bool      locked;

	public PlayerProgress(LevelMode levelMode, int levelNumber, float starPercent, 
		float bestTime, int bestTry, int bestCount, bool cleared, bool locked)
	{
		this.levelMode   = levelMode;
		this.levelNumber = levelNumber;
		this.starPercent = starPercent;
		this.bestTime    = bestTime;
		this.bestTry     = bestTry;
		this.bestCount   = bestCount;
		this.completed   = cleared;
		this.locked      = locked;
	}
}
