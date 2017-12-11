using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelMode
{
	TIME_AND_TRY,
	TIME,
	TRY
}

// Basic Level Concept
// Table is a prefab and it is standart 7x7
// We number table from 1 to 49.
// We have a List<int> in the Level class
// and it holds all openable cards.

// 01 02 03 04 05 06 07
// 08 09 10 11 12 13 14
// etc.

public class Level
{
	public int levelNo;

	public LevelMode mode;

	public string difficulty;

	public List<int> design;

	// We may need this.
	public float difficultyMultiplier;

	public float starPercent;

	public float bestTime;

	public float bestTry;

	public bool currCompleted;

	public bool completed;

	public int totalCardCount;

	public Level(LevelMode mode, string difficulty, int levelNo, float difficultyMultiplier, int[] design)
	{
		this.mode                 = mode;
		this.difficulty           = difficulty;
		this.levelNo              = levelNo;
		this.difficultyMultiplier = difficultyMultiplier;
		this.design               = new List<int>(design);
		this.totalCardCount       = design.Length;
	}
}
