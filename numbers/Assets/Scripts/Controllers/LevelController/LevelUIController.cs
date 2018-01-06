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
    public GameObject RewardScreen;
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
	public Button restartButton;

	public RectTransform pupil;
	public GameObject lastOpenedCard;
	Vector2 centerOfEye;
	const float speedOfPupil = 150f;
	const float eyeWidth     = 50f;
	const float eyeHeight    = 20f;

	bool succeedScreenOpen;
	bool menuAnimOpen;

    int langIndex;

    private void Start()
    {
        langIndex = PlayerPrefs.HasKey("lang") ? PlayerPrefs.GetInt("lang") : 0;
		centerOfEye = new Vector2(pupil.position.x, pupil.position.y);

        RewardScreen.transform.GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.rewardScreenText[(int)DataTransfer.language];
        Button[] btns=RewardScreen.transform.GetComponentsInChildren<Button>();

        btns[0].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.rewardScreen4Sec[(int)DataTransfer.language];
        btns[1].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.rewardScreen8Sec[(int)DataTransfer.language];
		btns[2].GetComponentInChildren<TextMeshProUGUI>().text = StringLiterals.rewardScreenNoButton[(int)DataTransfer.language];

		if(ProgressController.IsHowToPlayShowed() == false)
		{
			FindObjectOfType<HowToPlay>().NextButton();
			HowToPlay(true);//we want to see anim is working
			showCardsButton.interactable = false;
		}
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
            showCardsButton.gameObject.SetActive(true);

        }
	}

	// Restart Button
	public void RestartLevel()
	{
		LevelController levelCont = LevelController.Instance;
		
		if(succeedScreenOpen == true)
			ToggleSucceedScreen();
	
		levelCont.SetupLevel(true);
        showCardsButton.gameObject.SetActive(true);
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

        LevelController.Instance.levelPaused = true;
            DataTransfer.remainingTime += (adsType == "video") ? 4f : 8f;
		adsLoadingGO.SetActive(true);

		Advertisement.Show(
			adsType, 
			new ShowOptions(){
				resultCallback = delegate(ShowResult res) {
					adsLoadingGO.SetActive(false);
					ToggleRewardScreen(false);
                    LevelController.Instance.levelPaused = true;
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

        
        showCardsButton.gameObject.SetActive(!succeedScreen);

    }
    
	public void ShowAllCards()
	{
		LevelController levelCont = LevelController.Instance;

        if (levelCont.showingAllCards == false)
            levelCont.ShowAllCards();
        else
            levelCont.RestoreCards();
	}


	Vector2 CalculateDestination(float x1, float y1, float x2, float y2)
	{
		Vector2 dest;

		if(x1 == x2)
		{
			dest = new Vector2(x1, y1 + eyeHeight);
			if(dest.x - centerOfEye.x > pupil.anchoredPosition.x)
			{
				if(pupil.anchoredPosition.x + Time.deltaTime * speedOfPupil < dest.x - centerOfEye.x)
					dest.x = pupil.anchoredPosition.x + centerOfEye.x + Time.deltaTime * speedOfPupil;
			}
			else
			{
				if(pupil.anchoredPosition.x - Time.deltaTime * speedOfPupil > dest.x - centerOfEye.x)
					dest.x = pupil.anchoredPosition.x + centerOfEye.x - Time.deltaTime * speedOfPupil;
			}

			return dest;
		}

		// Peak point of parabola
		// 20f is a variable that determined by observation
		float r = x1;
		float k = y1 + eyeHeight;

		// Parabolic Equation => y = a(x - r)^2 + k
		// T(r, k) is peek point of parabola.
		// It'll give us something like y = ax^2 + bx + c
		float p_a = (y1 - k) / Mathf.Pow(x1 + eyeWidth - r, 2f);
		float p_b = -2f * p_a * r;
		float p_c = p_a * Mathf.Pow(r, 2f) + k;

		// Linear Equation => y = mx + n
		float m = (y2 - y1) / (x2 - x1);
		float n = (-m * x1) + y1;

		float a = p_a;
		float b = p_b - m;
		float c = p_c - n;

		Vector2 roots = SolveQuadraticEquation(a, b, c);

		if(x1 > x2)
			dest.x = roots.x;
		else
			dest.x = roots.y;

		//Debug.Log("before");
		//Debug.Log("dest.x = " + (dest.x - centerOfEye.x));
		//Debug.Log("pupilPos.x = " + (pupil.anchoredPosition.x));

		if(dest.x - centerOfEye.x > pupil.anchoredPosition.x)
		{
			if(pupil.anchoredPosition.x + Time.deltaTime * speedOfPupil < dest.x - centerOfEye.x)
				dest.x = pupil.anchoredPosition.x + centerOfEye.x + Time.deltaTime * speedOfPupil;
		}
		else
		{
			if(pupil.anchoredPosition.x - Time.deltaTime * speedOfPupil > dest.x - centerOfEye.x)
				dest.x = pupil.anchoredPosition.x + centerOfEye.x - Time.deltaTime * speedOfPupil;
		}

//		Debug.Log("after");
//		Debug.Log("dest.x = " + (dest.x - centerOfEye.x));
//		Debug.Log("pupilPos.x = " + (pupil.anchoredPosition.x));
		
		dest.y = p_a * Mathf.Pow(dest.x, 2f) + p_b * dest.x + p_c;

		return dest;
	}

	Vector2 SolveQuadraticEquation(float a, float b, float c)
	{
		float disc = Mathf.Pow(b, 2.0f) - (4f * a * c);
		float root1 = (-b + Mathf.Sqrt(disc)) / (2f * a);
		float root2 = (-b - Mathf.Sqrt(disc)) / (2f * a);

		if(root1 > root2)
		{
			float temp = root1;
			root1 = root2;
			root2 = temp;
		}

		return new Vector2(root1, root2);
	}

	void UpdateEyeAnimation()
	{
		LevelController levelCont = LevelController.Instance;

		if(levelCont.showingAllCards == true)
		{
			pupil.anchoredPosition = Vector2.zero;
			lastOpenedCard = null;
			return;
		}

		if(lastOpenedCard == null)
		{
			pupil.anchoredPosition = new Vector2(0f, eyeHeight);
			return;
		}
			
		Vector3 lastOpenedCardPos = lastOpenedCard.transform.position;
		Vector2 newPupilPos = CalculateDestination(centerOfEye.x, centerOfEye.y, lastOpenedCardPos.x, lastOpenedCardPos.y);
		pupil.anchoredPosition = new Vector2(newPupilPos.x - centerOfEye.x, newPupilPos.y - centerOfEye.y);
	}

	void Update()
	{
		UpdateEyeAnimation();
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

		restartButton.interactable = !levelCont.showingAllCards;

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

	public void HowToPlay(bool animAct)
	{
        howToPlayScreen.SetActive(true);

        if(animAct)
		    ToggleMenuAnim();
        
    }
   

	public void ToggleMenuAnim()
	{
		menuAnimOpen = !menuAnimOpen;

		LevelController.Instance.levelPaused = menuAnimOpen;
		optionAnimator.SetBool("open", menuAnimOpen);

        if (LevelController.Instance.levelPaused == true)
        {
            float randomValue = UnityEngine.Random.Range(0f, 1.0f);
            if (randomValue >= 0.80f)
                ShowAds("video");
        }
    }  
}
