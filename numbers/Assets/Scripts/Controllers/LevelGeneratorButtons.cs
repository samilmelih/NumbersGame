using UnityEngine;
using System;

public class LevelGeneratorButtons : MonoBehaviour
{

    [Header("Unity Stuffs")]
    public GameObject saveOptions;
    public GameObject generatorOptions;

    public void CreateNewLevel_ButtonPressed()
    {
		saveOptions.SetActive(true);

		generatorOptions.SetActive(false);

		LevelManager.Instance.saveNewLevel = true;
    }

    public void LoadLevels_ButtonPressed()
	{
		saveOptions.SetActive(true);
		generatorOptions.SetActive(false);

		LevelManager.Instance.saveNewLevel = false;
		LevelManager.Instance.LoadLevels();
    }

	public void PreviousLevel_ButtonPressed()
	{
		LevelManager.Instance.PrevLevel();
	}

	public void ResetTable_ButtonPressed()
	{
		LevelManager.Instance.ResetCards();
	}

	public void Save_ButtonPressed()
	{
		LevelManager.Instance.SaveLevel();
	}

	public void NextLevel_ButtonPressed()
	{
		LevelManager.Instance.NextLevel();
	}

	public void Back_ButtonPressed()
	{
		
	}
}
