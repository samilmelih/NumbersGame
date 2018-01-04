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
	public GameObject howToPlayScreen;
	public GameObject adsLoadingGO;
	public GameObject showRewardScreen;
	public GameObject succeedScreen;
	public GameObject nextNumberArea;
	public GameObject table;
	public TextMeshProUGUI levelText;
	public TextMeshProUGUI nextNumberText;
	public TextMeshProUGUI timePassedText;
	public TextMeshProUGUI wrongTriesText;
	public TextMeshProUGUI bestCountText;
	public TextMeshProUGUI remainingTimeText;
	public Image starImage;
	public Image[] starLines;
	public Animator optionAnimator;
	public Button optionsButton;
    public Button showCardsButton;

	public RectTransform pupil;
	public GameObject lastOpenedCard;
	Vector2 lastPupilPos;
	Vector2 currPupilPos;
	Vector2 destPupilPos;
	float movementOfPupil;
	bool pupilIsMoving;

	bool succeedScreenOpen;
	bool menuAnimOpen;

    int langIndex;

    private void Start()
    {
        langIndex = PlayerPrefs.HasKey("lang") ? PlayerPrefs.GetInt("lang") : 0;
    }

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
			// Move to the next level.
			ToggleSucceedScreen();
			LevelController.Instance.SetupLevel();
		}
	}

	// Restart Button
	public void RestartLevel()
	{
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

	public void ToggleRewardScreen(bool _active)
	{
		LevelController levelCont = LevelController.Instance;

		showRewardScreen.SetActive(_active);
		optionsButton.enabled = !_active;
		levelCont.levelPaused = _active;
	}

	public void ShowAds(string adsType)
	{
		if(Application.internetReachability == NetworkReachability.NotReachable)
			return;

		DataTransfer.remainingTime += (adsType == "video") ? 1f : 5f;
		adsLoadingGO.SetActive(true);

		Advertisement.Show(
			adsType, 
			new ShowOptions(){
				resultCallback = delegate(ShowResult res) {
					adsLoadingGO.SetActive(false);
					ToggleRewardScreen(false);
				}
			}
		);
	}

	public void ToggleSucceedScreen()
	{
		succeedScreenOpen = !succeedScreenOpen;
		succeedScreen.SetActive(!succeedScreen.activeSelf);
		table.gameObject.SetActive(!table.gameObject.activeSelf);
		nextNumberArea.SetActive(!nextNumberArea.activeSelf);

        succeedScreen.transform.Find("LevelCompletedText").GetComponent<TextMeshProUGUI>().text = StringLiterals.levelCompletedText[langIndex];
	}
		
	public void ShowAllCards()
	{
		LevelController levelCont = LevelController.Instance;

		if(levelCont.showingAllCards == false)
			levelCont.ShowAllCards();
		else
			levelCont.RestoreCards();
	}

	public void UpdateEyeAnimation(float deltaTime)
	{

	}

	void Update()
	{
		UpdateEyeAnimation(Time.deltaTime);
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

	public void DisableNextButton()
	{
		Transform nextButton = succeedScreen.transform.Find("Button_NextLevel");
		nextButton.GetComponent<Button>().interactable = false;
	}

    public void UpdateInfo()
	{
		LevelController levelCont = LevelController.Instance;

		int nextNumber = Mathf.Clamp(levelCont.nextNumber, 1, levelCont.currLevel.totalCardCount);
		nextNumberText.text = nextNumber.ToString();

		remainingTimeText.text = string.Format("{0:F2}", DataTransfer.remainingTime);

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

	public void ToggleMenuAnim()
	{
		menuAnimOpen = !menuAnimOpen;

		LevelController.Instance.levelPaused = menuAnimOpen;
		optionAnimator.SetBool("open", menuAnimOpen);
	}  
}
