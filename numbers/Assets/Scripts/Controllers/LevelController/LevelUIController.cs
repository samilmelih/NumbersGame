using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUIController : MonoBehaviour
{
	[Header("Unity Stuffs")]
	public GameObject succeedScreen;
	public GameObject nextNumberArea;
	public GameObject table;
	public TextMeshProUGUI levelText;
	public TextMeshProUGUI nextNumberText;
	public TextMeshProUGUI timePassedText;
	public TextMeshProUGUI wrongTriesText;
	public Image starImage;
	public Image[] starLines;
	public Animator optionAnimator;

	public void SetupUI()
	{
		LevelMode levelMode = LevelController.Instance.levelMode;

		Transform nextButton = succeedScreen.transform.Find("Button_NextLevel");
		nextButton.GetComponent<Button>().interactable = true;
		table.SetActive(true);
		nextNumberArea.SetActive(true);


		if(levelMode == LevelMode.TRY)
		{
			timePassedText.transform.parent.gameObject.SetActive(false);
		}
		else if(levelMode == LevelMode.TIME)
		{
			wrongTriesText.transform.parent.gameObject.SetActive(false);
		}
	}

	// Next Button
	public void NextLevel()
	{
		LevelController levelCont = LevelController.Instance;

		if (levelCont.levelNo == levelCont.levels.Count)
		{
			// Mode is end. Return to level picker screen.
			BackButton();
		}
		else
		{
			// Move to the next level.
			ToggleSucceedScreen();
			LevelController.Instance.SetupLevel();
		}
	}

	public void BackButton()
	{
		// Set panel to LevelPickerPanel
		DataTransfer.currOpenPanel = 2;
		SceneManager.LoadScene(0);
	}

	// Restart Button
	public void RestartLevel()
	{
		ToggleSucceedScreen();
		LevelController.Instance.SetupLevel(true);
	}

	public void ToggleSucceedScreen()
	{
		succeedScreen.SetActive(!succeedScreen.activeSelf);
		table.gameObject.SetActive(!table.gameObject.activeSelf);
		nextNumberArea.SetActive(!nextNumberArea.activeSelf);
	}

	public void DisableNextButton()
	{
		Transform nextButton = succeedScreen.transform.Find("Button_NextLevel");
		nextButton.GetComponent<Button>().interactable = false;
	}

	public IEnumerator FillStarImage(float _fillAmount)
	{
		RestoreStarLines();

		float[] starPercents = LevelController.Instance.starPercents;
		float fillSpeed      = LevelController.Instance.fillSpeed;

		float filled = 0;
		int starLineIndex = 0;

		while (filled <= _fillAmount && starLineIndex < starPercents.Length)
		{
			filled = filled + Time.deltaTime * fillSpeed;
			starImage.fillAmount = filled;

			if (filled >= starPercents[starLineIndex])
			{
				starLines[starLineIndex].gameObject.SetActive(true);
				starLineIndex++;
			}

			yield return null;
		}
	}

	public void RestoreStarLines()
	{
		foreach (var starVar in starLines)
			starVar.gameObject.SetActive(false);
	}

	public void ShowAllCards()
	{
		LevelController.Instance.ShowAllCards();
	}

	public void UpdateInfo()
	{
		LevelController levelCont = LevelController.Instance;

		// FIXME: We don't need to update level text every frame.
		levelText.text = "LEVEL " + levelCont.levelNo;

		nextNumberText.text = levelCont.nextNumber.ToString();

		if(
			levelCont.levelMode == LevelMode.TIME_AND_TRY ||
			levelCont.levelMode == LevelMode.TIME
		){
			timePassedText.text = string.Format("{0:F2}", levelCont.timePassed);
		}

		if(
			levelCont.levelMode == LevelMode.TIME_AND_TRY ||
			levelCont.levelMode == LevelMode.TRY
		){
			wrongTriesText.text = levelCont.wrongTries.ToString();
		}
	}

	public void HowToPlay()
	{
		// TODO:
	}

	public void ShowMenuAnim()
	{
		LevelController.Instance.levelPaused = true;
		optionAnimator.SetBool("open", true);
	}

	public void CloseMenuAnim()
	{
		LevelController.Instance.levelPaused = false;
		optionAnimator.SetBool("open", false);
	}
}
