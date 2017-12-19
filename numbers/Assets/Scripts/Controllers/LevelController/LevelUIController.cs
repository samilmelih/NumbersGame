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
		LevelMode levelMode = LevelController.Instance.currLevelMode;

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

	public void NextLevel()
	{
		ToggleSucceedScreen();
		LevelController.Instance.SetupLevel();
	}

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

	public IEnumerator FillStarImage(float _fillAmount)
	{
		LevelController levelCont = LevelController.Instance;

		float filled = 0;
		while (filled <= _fillAmount && levelCont.indexStarLines < levelCont.starPercents.Length)
		{
			if (filled >= levelCont.starPercents[levelCont.indexStarLines])
			{
				starLines[levelCont.indexStarLines].gameObject.SetActive(true);
				levelCont.indexStarLines++;
			}

			filled = filled + Time.deltaTime * levelCont.fillSpeed;
			starImage.fillAmount = filled;
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
		levelText.text = "LEVEL " + levelCont.currLevelNo;

		nextNumberText.text = levelCont.nextNumber.ToString();

		if(
			levelCont.currLevelMode == LevelMode.TIME_AND_TRY ||
			levelCont.currLevelMode == LevelMode.TIME
		){
			timePassedText.text = string.Format("{0:F2}", levelCont.timePassed);
		}

		if(
			levelCont.currLevelMode == LevelMode.TIME_AND_TRY ||
			levelCont.currLevelMode == LevelMode.TRY
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

	public void BackButton()
	{
		SceneManager.LoadScene(1);
	}
}
