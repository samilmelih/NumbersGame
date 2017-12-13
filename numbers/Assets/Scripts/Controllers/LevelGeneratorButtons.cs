using UnityEngine;
using System;

public class LevelGeneratorButtons : MonoBehaviour
{

    [Header("Unity Stuffs")]
    public GameObject levelInfoScreen;
    public GameObject buttons;
	public GameObject tableScreen;

    public void LevelInfoCreate()
    {
        // hide screen
        LevelInfoScreenShowHide();
    }

    public void LoadLevels()
    {
		LevelInfoScreenShowHide();
		tableScreen.SetActive(!tableScreen.activeSelf);
		LevelManager.Instance.LoadLevels();
    }

	public void NextLevelButton()
	{
		// TODO
	}

	public void PreviousLevelButton()
	{
		// TODO
	}

    public void ResetButton()
    {
        LevelManager.Instance.ResetCards();
    }

    public void LevelInfoScreenShowHide()
    {
        levelInfoScreen.SetActive(!levelInfoScreen.activeSelf);
        buttons.SetActive(!buttons.activeSelf);
    }

    public void CreateLevel()
    {
        LevelManager.Instance.CreateLevel();
    }
}
