using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPickerController : MonoBehaviour
{
	public static LevelPickerController Instance;

    public GameObject levelPickerPrefab;
    public Transform contentObject;
	public ScrollRect scrollRect;

    int countOfLevel;
	LevelMode levelMode;
	List<Level> levels;

	// Use this for initialization
	void Start ()
	{
		Instance = this;
	}

	public void LoadLevels()
	{
		DestroyOldLevelCards();

		levelMode = DataTransfer.levelMode;
		levels = GetLevels(levelMode);
		countOfLevel = levels.Count;

		//we have to change content width up to level count
		float levelPickerWidth = (levelPickerPrefab.transform as RectTransform).sizeDelta.x;
		float spacing = contentObject.gameObject.GetComponent<HorizontalLayoutGroup>().spacing;

		RectTransform contentRectTransform = (contentObject as RectTransform);
		contentRectTransform.sizeDelta = new Vector2(
			(levelPickerWidth + spacing) * (countOfLevel),
			contentRectTransform.sizeDelta.y
		);

		// This refresh the position of scrollRect.
		scrollRect.horizontalNormalizedPosition = 0.0f;

		AddLevelCards();
	}

	void AddLevelCards()
	{
		for (int levelNo = 0; levelNo < countOfLevel; levelNo++)
		{
			Level level = levels[levelNo];

			GameObject levelPicker = Instantiate(levelPickerPrefab, contentObject);
			levelPicker.name = levelNo.ToString();

			Transform table = levelPicker.transform.Find("LevelTable").Find("Table");

			int designIndex = 0;
			for (int cardNo = 0; cardNo < table.childCount; cardNo++)
			{
				// TODO: This code is for testing. When we decide about colors,
				// we'll fix this code.

				Color testColor = Color.yellow;
				testColor.a = 0.4f;

				if (designIndex < level.design.Count && level.design[designIndex] == cardNo + 1)
				{
					Sprite s = Resources.Load<Sprite>("Sprites/UISprites/CardSprite/UI_Icon_Card_Rect");
					table.GetChild(cardNo).GetComponent<Image>().sprite = s;
					table.GetChild(cardNo).GetComponent<Image>().color = Color.white;

					//table.GetChild(cardNo).GetComponent<Image>().enabled = true;
					designIndex++;
				}
				else
				{
					table.GetChild(cardNo).GetComponent<Image>().sprite = null;
					table.GetChild(cardNo).GetComponent<Image>().color = testColor;
					//table.GetChild(cardNo).GetComponent<Image>().enabled = false;
				}
			}

			PlayerProgress progress = ProgressController.GetProgress(levelMode, levelNo + 1);

			Transform levelInfo = levelPicker.transform.Find("LevelInfo");
			TextMeshProUGUI levelText = levelInfo.Find("LevelText").GetComponent<TextMeshProUGUI>();
			levelText.text = string.Format("{0} {1}", StringLiterals.levelText[(int)DataTransfer.language], (levelNo + 1));

			Transform info          = levelInfo.Find("Info");
			Transform bestTimeInfo  = info.Find("Info_BestTime");;
			Transform bestTryInfo   = info.Find("Info_BestTry");
			Transform bestCountInfo = info.Find("Info_BestCount");

			if(levelMode == LevelMode.CLASSIC)
			{
				TextMeshProUGUI bestTimeInfoText = bestTimeInfo.GetComponentInChildren<TextMeshProUGUI>();
				bestTimeInfoText.text = (progress.completed) ? string.Format("{0:F2}", progress.bestTime) : "-_-";
			}
			bestTimeInfo.gameObject.SetActive(levelMode == LevelMode.CLASSIC);

			if(levelMode == LevelMode.DO_NOT_FORGET || levelMode == LevelMode.CLASSIC)
			{
				TextMeshProUGUI bestTryInfoText = bestTryInfo.GetComponentInChildren<TextMeshProUGUI>();
				bestTryInfoText.text = (progress.completed) ? progress.bestTry.ToString() : "-_-";
			}
			bestTryInfo.gameObject.SetActive(levelMode == LevelMode.DO_NOT_FORGET || levelMode == LevelMode.CLASSIC);

			if(levelMode == LevelMode.NO_MISTAKE)
			{
				TextMeshProUGUI bestCountInfoText = bestCountInfo.GetComponentInChildren<TextMeshProUGUI>();
				bestCountInfoText.text = (progress.completed) ? progress.bestCount.ToString() :  "-_-";
			}
			bestCountInfo.gameObject.SetActive(levelMode == LevelMode.NO_MISTAKE);


			Transform stars = levelInfo.transform.Find("Stars");
			stars.GetComponentInChildren<Image>().fillAmount = progress.starPercent;

			Transform levelPickerButton = levelPicker.transform.Find("PickButton");
			Transform levelLockedIcon = levelPicker.transform.Find("LockedIcon");

			if(levelNo != 0 && progress.locked == true)
				levelLockedIcon.gameObject.SetActive(true);
			else
				AddButtonListener(levelPickerButton, levelNo);
		}
	}

	void AddButtonListener(Transform button, int levelNo)
	{
		int levelIndex = levelNo;
		button.GetComponent<Button>().onClick.AddListener(
			delegate
			{
				OnLevelPickerButton_Clicked(levelIndex);    
			}
		);
	}

	void DestroyOldLevelCards()
	{
		for(int i = 0; i < contentObject.childCount; i++)
		{
			Destroy(contentObject.GetChild(i).gameObject);
		}
	}

    void OnLevelPickerButton_Clicked(int index)
    {
		DataTransfer.levelNo = index;
        SceneManager.LoadScene(1);
    }

	public static List<Level> ReadLevels()
	{
		List<Level> readLevels = new List<Level>();
		TextAsset levelSettingsText = Resources.Load<TextAsset>("Levels/levels");
		Level level;

		var levelSettingsTextArr = levelSettingsText.text.Split('\n');
		int[] levelNumberArr = new int[Enum.GetNames(typeof(LevelMode)).Length + 1];

		for (int j = 0; j < levelSettingsTextArr.Length; j++)
		{
			if (levelSettingsTextArr[j].Length == 0)
				continue;

			var levelSettings = levelSettingsTextArr[j].Split(',');

			LevelMode levelMode             = (LevelMode)Enum.Parse(typeof(LevelMode), levelSettings[0]);
			LevelDifficulty levelDifficulty = (LevelDifficulty)Enum.Parse(typeof(LevelDifficulty), levelSettings[1]);

			level = new Level(
				levelMode,															// LevelMode
				levelDifficulty,													// Difficulty
				++levelNumberArr[(int) levelMode],									// LevelNumber
				0.0f,																// Multiplier
				Array.ConvertAll(levelSettings[2].Trim().Split('-'), int.Parse)		// Design Array
			);

			readLevels.Add(level);
		}

		return readLevels;
	}

	public static List<Level> GetLevels(LevelMode mode)
	{
		List<Level> levels = new List<Level>(DataTransfer.readLevels);
		List<Level> selectedModeLevels = new List<Level>();

		foreach(Level level in levels)
		{
			if(level.mode == mode)
				selectedModeLevels.Add(level);
		}

		return selectedModeLevels;
	}
}
