using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject levelPickerPrefab;
    public Transform contentObject;

    public int countOfLevel;
    
	public static int currLevelNo;
	public static LevelMode currLevelMode;
	public static List<Level> levels;

	// Use this for initialization
	void Start ()
	{
		levels = ReadLevels(currLevelMode);
        countOfLevel = levels.Count;

        //we have to change content width up to level count
		float levelPickerWidth = (levelPickerPrefab.transform as RectTransform).sizeDelta.x;
		float spacing = contentObject.gameObject.GetComponent<HorizontalLayoutGroup>().spacing;

		RectTransform contentRectTransform = (contentObject as RectTransform);
		contentRectTransform.sizeDelta = new Vector2(
			(levelPickerWidth + spacing) * (countOfLevel),
			contentRectTransform.sizeDelta.y
		);

        AddLevels();
	}

    void OnLevelPickerButton_Clicked(int index)
    {
		currLevelNo = index;
        SceneManager.LoadScene(2);
    }

	void AddLevels()
    {
		for (int levelNo = 0; levelNo < countOfLevel; levelNo++)
        {
            GameObject levelPicker = Instantiate(levelPickerPrefab, contentObject);
            levelPicker.name = levelNo.ToString();


				

			Transform table = levelPicker.transform.Find("LevelTable").Find("Table");
			Level level = levels[levelNo];

			int designIndex = 0;
			for (int cardNo = 0; cardNo < table.childCount; cardNo++)
            {
            	if (designIndex < level.design.Count && level.design[designIndex] == cardNo + 1)
				{
					table.GetChild(cardNo).GetComponent<Image>().enabled = true;
					designIndex++;
                }
                else
				{
					table.GetChild(cardNo).GetComponent<Image>().enabled = false;
				}
            }

			PlayerProgress progress = ProgressController.GetProgress(currLevelMode, levelNo + 1);

			Transform levelInfo = levelPicker.transform.Find("LevelInfo");

			Transform levelText = levelInfo.Find("LevelText");
			levelText.GetComponent<TextMeshProUGUI>().text = "LEVEL " + (levelNo + 1);

			Transform bestTimeInfo = levelInfo.Find("BestTimeInfo");
			TextMeshProUGUI bestTimeInfoText = bestTimeInfo.GetComponentInChildren<TextMeshProUGUI>();
			bestTimeInfoText.text = (progress.cleared) ? string.Format("{0:F2}", progress.bestTime) : "0.0";

			Transform bestTryInfo = levelInfo.Find("BestTryInfo");
			TextMeshProUGUI bestTryInfoText = bestTryInfo.GetComponentInChildren<TextMeshProUGUI>();
			bestTryInfoText.text = (progress.cleared) ? progress.bestTry.ToString() : "0";

			Transform stars = levelInfo.transform.Find("Stars");
			stars.GetComponentInChildren<Image>().fillAmount = progress.starPercent;
				
			Transform levelPickerButton = levelPicker.transform.Find("PickButton");

			int levelIndex = levelNo;
			levelPickerButton.GetComponent<Button>().onClick.AddListener(
                delegate
                {
					OnLevelPickerButton_Clicked(levelIndex);    
                }
			);

			if(currLevelMode == LevelMode.TRY)
				bestTimeInfo.gameObject.SetActive(false);
			else if(currLevelMode == LevelMode.TIME)
				bestTryInfo.gameObject.SetActive(false);
        }
    }

	public static List<Level> ReadLevels(LevelMode mode)
	{
		List<Level> readLevels = new List<Level>();
		TextAsset levelSettingsText = Resources.Load<TextAsset>("Levels/levels");
		Level lvl;

		var levelSettingsTextArr = levelSettingsText.text.Split('\n');

		int levelNo = 1;
		for (int j = 0; j < levelSettingsTextArr.Length; j++)
		{
			if (levelSettingsTextArr[j].Length == 0)
				continue;

			var levelSettings = levelSettingsTextArr[j].Split(',');

			LevelMode levelMode             = (LevelMode)Enum.Parse(typeof(LevelMode), levelSettings[0]);
			LevelDifficulty levelDifficulty = (LevelDifficulty)Enum.Parse(typeof(LevelDifficulty), levelSettings[1]);

			if(mode != LevelMode.NONE && mode != levelMode)
				continue;

			lvl = new Level(
				levelMode,															// LevelMode
				levelDifficulty,													// Difficulty
				levelNo++,															// LevelNumber
				0.0f,																// Multiplier
				Array.ConvertAll(levelSettings[2].Trim().Split('-'), int.Parse)		// Design Array
			);

			readLevels.Add(lvl);
		}

		return readLevels;
	}

	public void BackButton_OnClick()
	{
		GameController.mainMenuOpen = false;
		SceneManager.LoadScene(0);
	}
}
