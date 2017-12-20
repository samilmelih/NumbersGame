using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    [Header("Unity Stuffs")]
    public GameObject cardPrefab;
	public Transform tableTransform;
	public Sprite openCardSprite;
	public Sprite closeCardSprite;
	public LevelUIController UICont;

	List<GameObject> cardGoList;

    [Header("Level Handler")]
    public int currLevelNo;
    public float timePassed;
	public int nextNumber;
	public int wrongTries;
	public float starPercent;
	public int tableSize = 49;
	public Level currLevel;
	public LevelMode currLevelMode;

	public int indexStarLines;
    public float fillSpeed = .7f;
	public float[] starPercents = { 0.297f, 0.627f, 0.957f };

	public bool levelStarted;
	public bool levelPaused;
	public bool showingAllCards;

	Action changeSuccedScreenMethod;
	Action restoreCardsMethod;



    // Use this for initialization
    void Start()
	{
		if (Instance == null)
            Instance = this;		

		cardGoList = new List<GameObject>();

		changeSuccedScreenMethod = this.ChangeSucceedScreenState;
		restoreCardsMethod       = this.RestoreCards;

		currLevelNo   = LevelManager.currLevelNo;
		currLevelMode = LevelManager.currLevelMode;

        SetupLevel();
    }
		
	public void SetupLevel(bool restart = false)
    {
		if(restart == true)
			currLevelNo--;

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

		UICont.SetupUI();

        List<int> numbers = setNumbersList();
		for (int cardNo = 1; cardNo <= tableSize; cardNo++)
        {
			Transform card = Instantiate(cardPrefab, tableTransform).transform;
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

		UICont.UpdateInfo();
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

        float starPercentForTime;
        float passedTimeUpperLimit = currLevel.totalCardCount * 3.2f;
        float passedTimeLowerLimit = currLevel.totalCardCount * 1.5f;

        starPercentForTime = (passedTimeUpperLimit - timePassed) / (passedTimeUpperLimit - passedTimeLowerLimit);
        starPercentForTime = Mathf.Clamp01(starPercentForTime);

		if(currLevelMode == LevelMode.TIME_AND_TRY)
			starPercent = (starPercentForTime + starPercentForTries) / 2.0f;
		else if(currLevelMode == LevelMode.TRY)
			starPercent = starPercentForTries;
		else if(currLevelMode == LevelMode.TIME)
			starPercent = starPercentForTime;

		SaveProgress();
		UICont.ToggleSucceedScreen();
		StartCoroutine(UICont.FillStarImage(starPercent));
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
	/// we need the number list to make a random list of numbers
	/// </summary>
	List<int> setNumbersList()
	{
		List<int> numbers = new List<int>();
		for (int i = 1; i <= currLevel.totalCardCount; i++)
			numbers.Add(i);

		return numbers;
	}
}
