using System;
using System.Collections.Generic;
using System.IO;
//using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;

    public bool generateMapActive = true;
	List<Level> readLevels;
    public List<CardSelection> selectedCardList;
	public List<CardSelection> cardTableList;

    public DropdownController dropdownController;
    public GameObject cardPrefab;
    public Transform cardTabletransform;

	int currentLevelIndex;
	public bool saveNewLevel;

	// 7x7
    private int maxMapSize = 7;

    // Use this for initialization
    void Start()
    {
        if (Instance != null)
            return;

        Instance = this;

		readLevels = LevelManager.ReadLevels(LevelMode.NONE);
		selectedCardList = new List<CardSelection>();

        if(generateMapActive)
		GenerateMap();
    }

	public void AddCardToList(CardSelection card)
	{
		selectedCardList.Add(card);
	}

    public void RemoveCardFromList(CardSelection card)
    {
        selectedCardList.Remove(card);
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

	public void LoadLevels()
	{
		ResetCards();

		Level currLevel = readLevels[currentLevelIndex];

		dropdownController.levelMode = currLevel.mode;
		dropdownController.levelDifficulty = currLevel.difficulty;
		dropdownController.LevelmodeDropdown.value = (int) currLevel.mode;
		dropdownController.DifficultyDropdown.value = (int) currLevel.difficulty;

		foreach(CardSelection cardSelection in cardTableList)
		{
			if(currLevel.design.Contains(cardSelection.index) == true)
			{
				cardSelection.OnCardClikced();
			}
		}
	}

	public void PrevLevel()
	{
		currentLevelIndex--;
		if(currentLevelIndex == -1)
			currentLevelIndex = 0;
		else
			LoadLevels();
	}

	public void NextLevel()
	{
		currentLevelIndex++;
		if(currentLevelIndex == readLevels.Count)
			currentLevelIndex = readLevels.Count - 1;
		else
			LoadLevels();
	}


    public void SaveLevel()
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

		// This is a change so remove old one and add new design
		if(saveNewLevel == false)
			readLevels.Remove(readLevels[currentLevelIndex]);
			
		readLevels.Add(newLevel);
		readLevels.Sort();

		string path = "Assets/Resources/Levels/levels.txt";
		StreamWriter streamWriter = new StreamWriter(path, false);

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

		// If we don't import text asset, it does not
		// update when game is running.
		//AssetDatabase.ImportAsset(path);
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

				// We need this code because OnCardClicked() method may be called
				// before Start() method. This causes exception.
				go.GetComponent<CardSelection>().levelGenerator = this;

				cardTableList.Add(go.GetComponent<CardSelection>());
            }

        }
    }



}
