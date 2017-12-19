using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LevGenUIController : MonoBehaviour
{

    [Header("Unity Stuffs")]
    public GameObject saveOptions;
    public GameObject generatorOptions;
	public GameObject prevButton;
	public GameObject nextButton;

    public void CreateNewLevel_ButtonPressed()
    {
		saveOptions.SetActive(true);
		prevButton.SetActive(false);
		nextButton.SetActive(false);
		generatorOptions.SetActive(false);

		LevelGenerator.Instance.saveNewLevel = true;
		LevelGenerator.Instance.ResetCards();
	}

    public void LoadLevels_ButtonPressed()
	{
		saveOptions.SetActive(true);
		prevButton.SetActive(true);
		nextButton.SetActive(true);
		generatorOptions.SetActive(false);

		LevelGenerator.Instance.saveNewLevel = false;
		LevelGenerator.Instance.LoadLevels();
    }

	public void PreviousLevel_ButtonPressed()
	{
		LevelGenerator.Instance.PrevLevel();
	}

	public void ResetTable_ButtonPressed()
	{
		LevelGenerator.Instance.ResetCards();
	}

	public void Save_ButtonPressed()
	{
		LevelGenerator.Instance.SaveLevel();
	}

	public void NextLevel_ButtonPressed()
	{
		LevelGenerator.Instance.NextLevel();
	}

	public void Back_ButtonPressed()
	{
		if(generatorOptions.activeSelf == true)
			SceneManager.LoadScene(0);
		else
		{
			saveOptions.SetActive(false);
			generatorOptions.SetActive(true);
		}
	}
}
