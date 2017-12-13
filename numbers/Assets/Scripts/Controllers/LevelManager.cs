using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public List<CardSelection> selectedCardList;

    public DropdownController dropdownController;
    public GameObject cardPrefab;
    public Transform cardTabletransform;

	// 7x7
    private int maxMapSize = 7;

    // Use this for initialization
    void Start()
    {
        if (Instance != null)
            return;

        Instance = this;

		selectedCardList = new List<CardSelection>();
		GenerateMap();
    }

    public void RemoveCardFromList(CardSelection card)
    {
        selectedCardList.Remove(card);
    }

    public void AddCardToList(CardSelection card)
    {
        selectedCardList.Add(card);
    }

    public void ResetCards()
    {
		// Why we don't work on original list?
        List<CardSelection> cards = new List<CardSelection>(selectedCardList);
        foreach (CardSelection card in cards)
        {
            card.OnCardClikced();
        }
    }

	public static List<Level> ReadLevels()
	{
		List<Level> readLevels = new List<Level>();
		TextAsset levelSettingsText = Resources.Load<TextAsset>("Levels/levels");
		Level lvl;

		var levelSettingsTextArr = levelSettingsText.text.Split('\n');
		for (int j = 0; j < levelSettingsTextArr.Length; j++)
		{
			if (levelSettingsTextArr[j].Length == 0)
				continue;

			var levelSettings = levelSettingsTextArr[j].Split(',');

			lvl = new Level(
				(LevelMode)Enum.Parse(typeof(LevelMode), levelSettings[0]),					// LevelMode
				(LevelDifficulty)Enum.Parse(typeof(LevelDifficulty), levelSettings[1]),		// Difficulty
				j + 1,																		// LevelNumber
				0.0f,																		// Multiplier
				Array.ConvertAll(levelSettings[2].Trim().Split('-'), int.Parse)				// Design Array
			);

			readLevels.Add(lvl);
		}

		return readLevels;
	}

    public void CreateLevel()
    {
        // check if we have all data that we need
        // create level if you need(we will in 2. update)
        // save to the file

		// we dont have to sort but it will look better and clean
		selectedCardList.Sort();

		int[] selectedCardArr = Array.ConvertAll(selectedCardList.ToArray(), card => card.index);

		Level newLevel = new Level(
			dropdownController.levelMode,
			dropdownController.levelDifficulty,
			0,
			0.0f,
			selectedCardArr
		);

		List<Level> readLevels = ReadLevels();
		readLevels.Add(newLevel);
		readLevels.Sort();


		StreamWriter streamWriter = new StreamWriter("Assets/Resources/Levels/levels.txt", false);

		foreach(Level level in readLevels)
		{
			string levelContext = string.Format("{0},{1},", level.mode, level.difficulty);

			foreach(int cardNumber in level.design)
			{
				levelContext += cardNumber + "-";
			}

			levelContext = levelContext.Remove(levelContext.Length - 1);

			streamWriter.WriteLine(levelContext);
		}

        streamWriter.Close();
    }

	public void LoadLevels()
	{
		List<Level> readLevels = ReadLevels();

		// TODO: Load levels into grid
	}

    public void GenerateMap()
    {
        for (int i = 0; i < maxMapSize; i++)
        {
            for (int j = 0; j < maxMapSize; j++)
            {
                GameObject go = Instantiate(cardPrefab, cardTabletransform);
                go.GetComponent<CardSelection>().index = maxMapSize * (i) + (j + 1);
                go.name = maxMapSize * (i) + (j + 1) + "";
            }

        }
    }



}
