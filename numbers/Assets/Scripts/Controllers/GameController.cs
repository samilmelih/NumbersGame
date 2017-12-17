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
    public TextMeshProUGUI remainingTimeText;
    public Image starImage;
    public Image[] starLines;

	public Sprite openCardSprite;
	public Sprite closeCardSprite;

	List<GameObject> cardGoList;
	public List<int> currOpenedCards;

    LevelMode currLevelMode = LevelMode.TIME_AND_TRY;   // This is just for test
    public Level currLevel;

    [Header("Level Handler")]
    public int currLevelNo = 0;
    public float timePassed = 0;
	public int tableSize = 49;


    [HideInInspector]
    public int nextNumber;

    [HideInInspector]
    public int wrongTries;

	private int indexStarLines = 0;
    private float fillSpeed = .7f;

	public bool levelStarted;
	public bool levelPaused;

    private float[] starPercents = { 0.297f, 0.627f, 0.957f };

    Dictionary<LevelMode, Dictionary<int, Level>> levels;

	Action changeSuccedScreenMethod;
	Action restoreCardsMethod;

    // Use this for initialization
    void Start()
    {
        currLevelNo = PlayerPrefs.GetInt("level");
        currLevelMode = LevelMode.TIME_AND_TRY;
       
        if (Instance == null)
            Instance = this;

		cardGoList = new List<GameObject>();

		changeSuccedScreenMethod = this.ChangeSucceedScreenState;
		restoreCardsMethod       = this.RestoreCards;

        InitializeLevels();
        SetupLevel();
    }
		
    void InitializeLevels()
    {
		levels = new Dictionary<LevelMode, Dictionary<int, Level>>();

		List<Level> readLevels = LevelGenerator.ReadLevels();

		foreach(Level lvl in readLevels)
		{
			AddLevelToDictionary(lvl);
		}
    }

    void AddLevelToDictionary(Level level)
    {
        if (levels.ContainsKey(level.mode) == false)
            levels[level.mode] = new Dictionary<int, Level>();

        levels[level.mode][level.levelNo] = level;
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

		currOpenedCards = new List<int>();

		// Set up variables
		timePassed = 0;
		wrongTries = 0;
		nextNumber = 1;
		levelStarted = false;
		currLevelNo++;

		currLevel = levels[currLevelMode][currLevelNo];
		currLevel.completed = false;

		TableTransform.gameObject.SetActive(true);
		nextNumberArea.SetActive(true);

        List<int> numbers = setNumbersList();
        for (int cardOrder = 1; cardOrder <= tableSize; cardOrder++)
        {
			Transform card = Instantiate(cardPrefab, TableTransform).transform;
			cardGoList.Add(card.gameObject);

            if (currLevel.design.Contains(cardOrder) == false)
            {
                card.GetComponent<Button>().enabled = false;
                card.GetComponent<Card>().GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                card.GetComponent<Card>().transform.GetComponentInChildren<Image>().enabled = false;
            }
            else
            {
                int index = Random.Range(0, numbers.Count);

                card.GetComponent<Button>().enabled = true;
                card.GetComponent<Card>().cardNumber = numbers[index];
                card.GetComponent<Card>().GetComponentInChildren<TextMeshProUGUI>().text = numbers[index].ToString();
                card.GetComponent<Card>().GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                card.GetComponent<Card>().transform.GetComponentInChildren<Image>().enabled = true;

                numbers.RemoveAt(index);
            }

            card.GetComponent<Card>().cardCleared = false;
        }
    }

	void Update()
	{
		if (currLevelMode == LevelMode.TIME_AND_TRY)
		{
			if (currLevel == null || currLevel.completed == true)
				return;

			if(levelStarted == true && levelPaused == false)
				timePassed += Time.deltaTime;

			if (nextNumber > currLevel.totalCardCount)
			{
				// After level is completed. Next number is fixed to last opened numbers
				nextNumber--;

				StartCoroutine(ExecuteAfterTime(1.0f, changeSuccedScreenMethod));

				currLevel.completed = true;
				currLevel.cleared = true;

				if (levels[currLevelMode].ContainsKey(currLevelNo) == false)
				{
					// Mode is end. We need to add some button to return main menu
					// For now it will return to begining.
					currLevelNo = 1;
				}
			}

			UpdateTextMesh();
		}
	}

    /// <summary>
    /// show or hide the screen
    /// </summary>
    public void ChangeSucceedScreenState()
    {
        succeedScreen.SetActive(!succeedScreen.activeSelf);
        TableTransform.gameObject.SetActive(!TableTransform.gameObject.activeSelf);
		nextNumberArea.SetActive(!nextNumberArea.activeSelf);

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

        currLevel.starPercent = (starPercentForTime + starPercentForTries) / 2.0f;
        //Debug.Log("starPercent: " + currLevel.starPercent);

        StartCoroutine(FillImage(currLevel.starPercent));
    }

	public void ShowAllCards()
	{
		levelPaused = true;

		foreach(GameObject go in cardGoList)
		{
			Card card = go.GetComponent<Card>();
			if(card.cardNumber != 0 && card.cardCleared == false)
			{
				if(currOpenedCards.Contains(card.cardNumber) == false)
					card.OpenCard();
				else
					card.CloseCard();
			}
		}

		StartCoroutine(ExecuteAfterTime(2.0f, restoreCardsMethod));
	}

	public void RestoreCards()
	{
		foreach(GameObject go in cardGoList)
		{
			Card card = go.GetComponent<Card>();
			if(card.cardNumber != 0)
			{
				if(currOpenedCards.Contains(card.cardNumber) == false)
					card.CloseCard();
				else
					card.OpenCard();
			}
		}

		levelPaused = false;
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
        timePassedText.text = String.Format("{0:F2}", timePassed);
        wrongTriesText.text = wrongTries.ToString();
    }
}
