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
    public float levelPickerWidth;
    public float spacing;
    
	public static LevelMode currLevelMode;
	public static List<Level> levels;

	// Use this for initialization
	void Start ()
	{
		levels = ReadLevels(currLevelMode);
        countOfLevel = levels.Count;

        //we have to change content width up to level count
        levelPickerWidth = (levelPickerPrefab.transform as RectTransform).sizeDelta.x;
        spacing = contentObject.gameObject.GetComponent<HorizontalLayoutGroup>().spacing;
        (contentObject as RectTransform).sizeDelta = new Vector2((levelPickerWidth + spacing) * (countOfLevel), (contentObject as RectTransform).sizeDelta.y);

        AddLevels();
	}

    void OnLevelPickerButton_Clicked(int index)
    {
        PlayerPrefs.SetInt("level", index);
        SceneManager.LoadScene(2);
    }

	void AddLevels()
    {
        for (int i = 0; i < countOfLevel; i++)
        {
            GameObject levelPicker = Instantiate(levelPickerPrefab, contentObject);
            levelPicker.name = i.ToString();


            Transform backround = levelPicker.transform.Find("Background");

                              //set a sprite to background of this levelPicker
                              

            int k = 0;
            Transform levelTable = levelPicker.transform.Find("LevelTable");
            Transform table = levelTable.transform.Find("Table");
            for (int j = 0; j < table.childCount; j++)
            {
               
                    if (k < levels[i].design.Count && levels[i].design[k] == j + 1)
                    {
                        table.GetChild(j).GetComponent<CardSelection>().SelectCard();
                        k++;
                    }
                    else
                    {
                        table.GetChild(j).GetComponentInChildren<Image>().color = new Color(255, 255, 255, 0.5f);
                    }
        
            }

            Transform levelInfo = levelTable.transform.Find("LevelInfo");
            levelInfo.GetComponent<TextMeshProUGUI>().text ="LEVEL " + (i+1);

            //fill the star if we ever tried to pass this level
            Transform stars = levelTable.transform.Find("Stars");
            stars.GetComponentInChildren<Image>().fillAmount = i / 10f;

            Transform info = levelTable.transform.Find("Info");//loop trough all childrens and change the txt in their children
            info.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = i + ".0 seconds";
            info.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = i + " tries";

            int levelIndex = i;
            levelPicker.GetComponentInChildren<Button>().onClick.AddListener(
                
                delegate
                {
                    OnLevelPickerButton_Clicked(levelIndex);    
                }
                );

        }
    }

	public static List<Level> ReadLevels(LevelMode mode)
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

			LevelMode levelMode             = (LevelMode)Enum.Parse(typeof(LevelMode), levelSettings[0]);
			LevelDifficulty levelDifficulty = (LevelDifficulty)Enum.Parse(typeof(LevelDifficulty), levelSettings[1]);

			if(mode != LevelMode.NONE && mode != levelMode)
				continue;

			lvl = new Level(
				levelMode,															// LevelMode
				levelDifficulty,													// Difficulty
				j + 1,																// LevelNumber
				0.0f,																// Multiplier
				Array.ConvertAll(levelSettings[2].Trim().Split('-'), int.Parse)		// Design Array
			);

			readLevels.Add(lvl);
		}

		return readLevels;
	}
}
