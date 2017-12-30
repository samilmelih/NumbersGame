using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;
using System;

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
	public TextMeshProUGUI bestCountText;
	public Image starImage;
	public Image[] starLines;
	public Animator optionAnimator;
    public GameObject howToPlayScreen;
    public GameObject adsLoadingGO;

	bool succeedScreenOpen;

    public void SetupUI()
	{
		LevelMode levelMode = LevelController.Instance.levelMode;

		Transform nextButton = succeedScreen.transform.Find("Button_NextLevel");
		nextButton.GetComponent<Button>().interactable = true;
		table.SetActive(true);
		nextNumberArea.SetActive(true);

		timePassedText.transform.parent.gameObject.SetActive(levelMode == LevelMode.CLASSIC);
		wrongTriesText.transform.parent.gameObject.SetActive(levelMode == LevelMode.CLASSIC || levelMode == LevelMode.DO_NOT_FORGET);
		bestCountText.transform.parent.gameObject.SetActive(levelMode == LevelMode.NO_MISTAKE);
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
			ShowAds();

			// Move to the next level.
			ToggleSucceedScreen();
			LevelController.Instance.SetupLevel();
		}
	}

	// Restart Button
	public void RestartLevel()
	{
		ShowAds();

		if(succeedScreenOpen == true)
			ToggleSucceedScreen();
		
		LevelController.Instance.SetupLevel(true);
	}

	public void BackButton()
	{
		// Set panel to LevelPickerPanel
		DataTransfer.currOpenPanel = 2;
		SceneManager.LoadScene(0);
	}

	void ShowAds()
	{
		if(DataTransfer.playedLevelCount % 3 == 0)
		{
			adsLoadingGO.SetActive(true);

			// We don't need to know whether ads is showed or failed etc.
			// Maybe we can randomize placement id of ads.
			Advertisement.Show(
				"video", 
				new ShowOptions(){
					resultCallback = delegate(ShowResult res) {
						adsLoadingGO.SetActive(false);
					}
				}
			);
		}
	}

	public void ToggleSucceedScreen()
	{
		succeedScreenOpen = !succeedScreenOpen;
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
		LevelController levelCont = LevelController.Instance;

		// This button will start time begining of the game.
		if(levelCont.levelStarted == false)
			levelCont.levelStarted = true;
		
		if (levelCont.ShowAllCards() && levelCont.levelPaused == false)
        {
			if(Application.internetReachability == NetworkReachability.NotReachable)
				return;

            if (Advertisement.IsReady())
            {
				adsLoadingGO.SetActive(true);
                levelCont.ResetCardStates();
                levelCont.levelPaused = true;
                Advertisement.Show("video", new ShowOptions() { resultCallback = AdResultHandler });
            }
        }
	}

    private void AdResultHandler(ShowResult res)
    {
		LevelController levelCont = LevelController.Instance;
		float timeOfShowing = -1f;

        switch (res)
        {
            case ShowResult.Failed:

                break;
            case ShowResult.Skipped:

				timeOfShowing = 3.0f;
                break;
            case ShowResult.Finished:
				
				timeOfShowing = 6.0f;
                break;
            default:
                break;
        }
        
		levelCont.levelPaused = false;
		adsLoadingGO.SetActive(false);

		if(timeOfShowing > 0.0f)
			levelCont.ShowAllCards(timeOfShowing);   
    }

    public void UpdateInfo()
	{
		LevelController levelCont = LevelController.Instance;

		int nextNumber = Mathf.Clamp(levelCont.nextNumber, 1, levelCont.currLevel.totalCardCount);
		nextNumberText.text = nextNumber.ToString();

		if(levelCont.levelMode == LevelMode.CLASSIC)
		{
			timePassedText.text = string.Format("{0:F2}", levelCont.timePassed);
		}

		if(
			levelCont.levelMode == LevelMode.CLASSIC ||
			levelCont.levelMode == LevelMode.DO_NOT_FORGET
		){
			wrongTriesText.text = levelCont.wrongTries.ToString();
		}

		// This code is not readable, I know...
		// FIXME: We'll improve it.
		if(levelCont.levelMode == LevelMode.NO_MISTAKE)
			bestCountText.text = ((nextNumber - 1) + ((levelCont.levelCompleted) ? 1 : 0)).ToString();
	}

	public void SetLevelText(int levelNo)
	{
		levelText.text = string.Format("{0} {1}", StringLiterals.levelText[(int)DataTransfer.language], levelNo);
	}

	public void HowToPlay()
	{
        howToPlayScreen.SetActive(true);
        optionAnimator.SetBool("open", false);
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
