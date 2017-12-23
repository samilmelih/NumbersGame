using System.Collections.Generic;
using System;

public enum LevelMode
{
    NONE,
    TIME_AND_TRY,
    TIME,
    TRY
}

public enum LevelDifficulty
{
    NONE,
    EASY,
    MEDIUM,
    HARD
}
	
public class Level : IComparable<Level>
{
    public int levelNo;

    public LevelMode mode;

    public LevelDifficulty difficulty;

    public List<int> design;

    // We may need this.
    public float difficultyMultiplier;

    public int totalCardCount;

    public Level(LevelMode mode, LevelDifficulty difficulty, int levelNo, float difficultyMultiplier, int[] design)
    {
        this.mode = mode;
        this.difficulty = difficulty;
        this.levelNo = levelNo;
        this.difficultyMultiplier = difficultyMultiplier;
        this.design = new List<int>(design);
        this.totalCardCount = design.Length;
    }

	public int CompareTo(Level other)
	{
		int thisLevelMode   = (int) this.mode;
		int otherLevelMode  = (int) other.mode;

		int thisDifficulty  = (int) this.difficulty;
		int otherDifficulty = (int) other.difficulty;

		if(thisLevelMode != otherLevelMode)
		{
			if(thisLevelMode < otherLevelMode)
				return -1;
			else
				return 1;
		}
		else if(thisDifficulty != otherDifficulty)
		{
			if(thisDifficulty < otherDifficulty)
				return -1;
			else
				return 1;
		}
		else if(this.totalCardCount < other.totalCardCount)
			return -1;
		else
			return 1;
	}
}
