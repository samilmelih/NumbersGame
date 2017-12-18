using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{

    public static GameController Instance;

    [Header("Unity Stuffs")]
    public GameObject starShow;
    public GameObject cardPrefab;
    public GameObject succeedScreen;
	public GameObject nextNumberArea;
    public Transform TableTransform;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI nextNumberText;
    public TextMeshProUGUI timePassedText;
    public TextMeshProUGUI wrongTriesText;
    public Image starImage;
    public Image[] starLines;
	public Sprite openCardSprite;
	public Sprite closeCardSprite;

	List<GameObject> cardGoList;

    [Header("Level Handler")]
    public int currLevelNo = 0;
    public float timePassed = 0;
	public int nextNumber;
	public int wrongTries;
	public float starPercent;
	public int tableSize = 49;
	public Level currLevel;
	LevelMode currLevelMode;

	private int indexStarLines = 0;
    private float fillSpeed = .7f;
	private float[] starPercents = { 0.297f, 0.627f, 0.957f };

	public bool levelStarted;
	public bool levelPaused;
	public bool showingAllCards;

	Action changeSuccedScreenMethod;
	Action restoreCardsMethod;


    // Use this for initialization
    void Start()
    {
		currLevelNo   = LevelManager.currLevelNo;
		currLevelMode = LevelManager.currLevelMode;
       
        if (Instance == null)
            Instance = this;

		cardGoList = new List<GameObject>();

		changeSuccedScreenMethod = this.ChangeSucceedScreenState;
		restoreCardsMethod       = this.RestoreCards;

        SetupLevel();
    }
		
    public void SetupLevel()
    {
		// Set up star lines
        indexStarLines = 0;
        foreach (var starVar in starLines)
            starVar.gameObject.SetActive(false);

		// Destroy old cards
		foreach(GameObject go in cardGoList)
			Destroy(go);
		
		cardGoList.Clear();

		// Set up variables
		timePassed = 0;
		wrongTries = 0;
		nextNumber = 1;
		levelStarted = false;
		currLevelNo++;

		currLevel = LevelManager.levels[currLevelNo - 1];
		currLevel.completed = false;

		TableTransform.gameObject.SetActive(true);
		nextNumberArea.SetActive(true);

		if(currLevelMode == LevelMode.TRY)
		{
			timePassedText.transform.parent.gameObject.SetActive(false);
		}
		else if(currLevelMode == LevelMode.TIME)
		{
			wrongTriesText.transform.parent.gameObject.SetActive(false);
		}

        List<int> numbers = setNumbersList();
		for (int cardNo = 1; cardNo <= tableSize; cardNo++)
        {
			Transform card = Instantiate(cardPrefab, TableTransform).transform;
			cardGoList.Add(card.gameObject);

            if (currLevel.design.Contains(cardNo) == false)
            {
				Card cardComponenet = card.GetComponent<Card>();
				cardComponenet.active = false;
            }
            else
            {
				int index = Random.Range(0, numbers.Count);
				Card cardComponenet = card.GetComponent<Card>();

				cardComponenet.active = true;
				cardComponenet.cardNumber = numbers[index];

                numbers.RemoveAt(index);
            }
        }
    }

	void Update()
	{
		if (currLevel == null || currLevel.completed == true)
			return;
		
		if(levelStarted == true && levelPaused == false)
			timePassed += Time.deltaTime;

		if (nextNumber > currLevel.totalCardCount)
		{
			// After level is completed. Next number is fixed to last opened number
			nextNumber--;

			currLevel.completed = true;
			currLevel.cleared = true;

			StartCoroutine(ExecuteAfterTime(1.0f, changeSuccedScreenMethod));

			if (LevelManager.levels.Contains(currLevel) == false)
			{
				// Mode is end. We need to add some button to return main menu
				// For now it will return to begining.
				currLevelNo = 0;
			}
		}

		UpdateTextMesh();
	}

	void SaveProgress()
	{
		PlayerProgress progress = new PlayerProgress(
			currLevel.mode,
			currLevel.levelNo,
			starPercent,
			timePassed,
			wrongTries,
			currLevel.cleared
		);

		ProgressController.SaveProgress(progress);
	}

    /// <summary>
    /// show or hide the screen
    /// </summary>
    public void ChangeSucceedScreenState()
    {
        // Yapılacak hata sayısı için üst ve alt limit belirliyorum ve buna göre oranlıyorum.
        // Aynı şekilde süre içinde. Mantık bu kadar basit.
        // tableSize * factor -> factor sayesinde ayar yapabiliriz. Duruma göre belki
        // level sayısınıda faktör olarak ekleriz.

        float starPercentForTries;
        float wrongTryUpperLimit = currLevel.totalCardCount * 2.0f;
        float wrongTryLowerLimit = currLevel.totalCardCount * 0.8f;

        starPercentForTries = (wrongTryUpperLimit - wrongTries) / (wrongTryUpperLimit - wrongTryLowerLimit);
        starPercentForTries = Mathf.Clamp01(starPercentForTries);
        //Debug.Log("starPercentForTries: " + starPercentForTries);

        float starPercentForTime;
        float passedTimeUpperLimit = currLevel.totalCardCount * 3.2f;
        float passedTimeLowerLimit = currLevel.totalCardCount * 1.5f;

        starPercentForTime = (passedTimeUpperLimit - timePassed) / (passedTimeUpperLimit - passedTimeLowerLimit);
        starPercentForTime = Mathf.Clamp01(starPercentForTime);
        //Debug.Log("starPercentForTime: " + starPercentForTime);

		if(currLevelMode == LevelMode.TIME_AND_TRY)
			starPercent = (starPercentForTime + starPercentForTries) / 2.0f;
		else if(currLevelMode == LevelMode.TRY)
			starPercent = starPercentForTries;
		else if(currLevelMode == LevelMode.TIME)
			starPercent = starPercentForTime;

        //Debug.Log("starPercent: " + currLevel.starPercent);

		SaveProgress();

		succeedScreen.SetActive(!succeedScreen.activeSelf);
		TableTransform.gameObject.SetActive(!TableTransform.gameObject.activeSelf);
		nextNumberArea.SetActive(!nextNumberArea.activeSelf);

        StartCoroutine(FillImage(starPercent));
    }

	public void ShowAllCards()
	{
		showingAllCards = true;

		foreach(GameObject go in cardGoList)
		{
			Card card = go.GetComponent<Card>();
			if(card.cardNumber == 0)
				continue;

			card.OpenCard();
		}

		StartCoroutine(ExecuteAfterTime(2.0f, restoreCardsMethod));
	}

	public void RestoreCards()
	{
		foreach(GameObject go in cardGoList)
		{
			Card card = go.GetComponent<Card>();
			if(card.cardNumber == 0)
				continue;

			if(card.cardCleared == false)
				card.CloseCard();
		}

		showingAllCards = false;
	}

	IEnumerator ExecuteAfterTime(float time, Action method)
	{
		yield return new WaitForSeconds(time);
		method();
	}

	IEnumerator FillImage(float _fillAmount)
	{
		float filled = 0;
		while (filled <= _fillAmount && indexStarLines < starPercents.Length)
		{
			if (filled >= starPercents[indexStarLines])
			{
				starLines[indexStarLines].gameObject.SetActive(true);
				indexStarLines++;
			}

			filled = filled + Time.deltaTime * fillSpeed;
			starImage.fillAmount = filled;
			yield return null;
		}
	}

	/// <summary>
	/// we need the number list to make a random list of numbers
	/// </summary>
	List<int> setNumbersList()
	{
		List<int> numbers = new List<int>();
		for (int i = 1; i <= currLevel.totalCardCount; i++)
			numbers.Add(i);

		return numbers;
	}

    void UpdateTextMesh()
    {
        levelText.text = "LEVEL " + currLevelNo;
        nextNumberText.text = nextNumber.ToString();
        
		if(
			currLevelMode == LevelMode.TIME_AND_TRY ||
			currLevelMode == LevelMode.TIME
		){
			timePassedText.text = String.Format("{0:F2}", timePassed);
		}

		if(
			currLevelMode == LevelMode.TIME_AND_TRY ||
			currLevelMode == LevelMode.TRY
		){
			wrongTriesText.text = wrongTries.ToString();
		}
	}
}
