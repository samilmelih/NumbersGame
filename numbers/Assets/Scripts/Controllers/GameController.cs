using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// level 1: 2x2 2sn  0 mistake = 3 star --- 2 sn 1 mistake 2 star -- others 1 star 
    /// level 2: 3x3 4sn  1 mistake = 3 star --- 5 sn 3 mistake 2 star -- others 1 star
    /// level 3: 4x4 8sn  3 mistake = 3 star --- 8 sn 5 msitake 2 star -- others 1 star
    /// 2^level + time + 2*(mistake) = 4/4  10/9 22/16  
    /// </summary>

    public static GameController Instance;


    [Header("Unity Stuffs")]
    public GameObject starShow;
    public GameObject cardPrefab;
    public GameObject succeedScreen;
    public Transform TableTransform;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI nextNumberText;
    public TextMeshProUGUI timePassedText;
    public TextMeshProUGUI wrongTriesText;
    public TextMeshProUGUI remainingTimeText;
    public Image starImage;
    public Image[] starLines;
    Sprite fullStarLineSprite;
    Sprite backStarLineSprite;

    LevelMode currLevelMode = LevelMode.TIME_AND_TRY;   // This is just for test
    public Level currLevel;

    [Header("Level Handler")]
    public int currLevelNo = 1;
    public float timePassed = 0;

    [HideInInspector]
    public int nextNumber;

    [HideInInspector]
    public int wrongTries;

    private float fillSpeed = .7f;

    public int tableSize = 49;

    private float difficultyMultiplier = 0.0f;

    Dictionary<LevelMode, Dictionary<int, Level>> levels;

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
            Instance = this;

        InitializeSprites();
        InitializeLevels();

        SetupLevel();
    }

    void InitializeSprites()
    {
        fullStarLineSprite = Resources.Load<Sprite>("Sprites/UISprites/Stars/Star_Lines_Full");
        backStarLineSprite = Resources.Load<Sprite>("Sprites/UISprites/Stars/Star_Lines_Back");
    }

    // 01 02 03 04 05 06 07
    // 08 09 10 11 12 13 14
    // 15 16 17 18 19 20 21
    // 22 23 24 25 26 27 28
    // 29 30 31 32 33 34 35
    // 36 37 38 39 40 41 42
    // 43 44 45 46 47 48 49
    void InitializeLevels()
    {

        levels = new Dictionary<LevelMode, Dictionary<int, Level>>();

        Level lvl;

        StreamReader streamReader = new StreamReader("levels.txt");
        string levelDesign;

        string[] designArr;
        int i = 1;
        while ((levelDesign = streamReader.ReadLine()) != null)
        {
            designArr = levelDesign.Split(',');
            lvl = new Level(
                (LevelMode)Enum.Parse(typeof(LevelMode), designArr[0]), //LevelMode
                (LevelDifficulty)Enum.Parse(typeof(LevelDifficulty), designArr[1]),//Difficulty
                i++,//LevelNumber
                difficultyMultiplier,//multipiler
                Array.ConvertAll(designArr[2].Split('-'), s => s != "" ? int.Parse(s) : -1)//design arr
                );
            AddLevelToDictionary(lvl);
        }


        #region LevelsAddedByHand




        // tar -> Time and Try

        //Level tat_easy_1 = new Level(
        //    LevelMode.TIME_AND_TRY,
        //    LevelDifficulty.EASY,
        //    1,
        //    0.0f,
        //    new int[] { 25 }
        //);

        //AddLevelToDictionary(tat_easy_1);

        //Level tat_easy_2 = new Level(
        //    LevelMode.TIME_AND_TRY,
        //    LevelDifficulty.EASY,
        //    2,
        //    0.0f,
        //    new int[] { 24, 25, 26 }
        //);

        //AddLevelToDictionary(tat_easy_2);

        //Level tat_easy_3 = new Level(
        //    LevelMode.TIME_AND_TRY,
        //    LevelDifficulty.EASY,
        //    3,
        //    0.0f,
        //    new int[] { 17, 19, 25, 31, 33 }
        //);

        //AddLevelToDictionary(tat_easy_3);

        //Level tat_easy_4 = new Level(
        //    LevelMode.TIME_AND_TRY,
        //    LevelDifficulty.EASY,
        //    4,
        //    0.0f,
        //    new int[] { 17, 18, 19, 24, 26, 31, 32, 33 }
        //);

        //AddLevelToDictionary(tat_easy_4);

        //Level tat_easy_5 = new Level(
        //    LevelMode.TIME_AND_TRY,
        //    LevelDifficulty.MEDIUM,
        //    5,
        //    0.0f,
        //    new int[]{11, 17, 18, 19, 23, 24, 26,
        //            27, 31, 32, 33, 39}
        //);

        //AddLevelToDictionary(tat_easy_5);

        //Level tat_easy_6 = new Level(
        //    LevelMode.TIME_AND_TRY,
        //    LevelDifficulty.MEDIUM,
        //    6,
        //    0.0f,
        //    new int[]{11, 17, 18, 19, 23, 24, 25, 26,
        //            27, 30, 31, 33, 34, 37, 41}
        //);

        //AddLevelToDictionary(tat_easy_6);

        //Level tat_easy_7 = new Level(
        //    LevelMode.TIME_AND_TRY,
        //    LevelDifficulty.MEDIUM,
        //    7,
        //    0.0f,
        //    new int[]{9, 10, 12, 13, 16, 18, 20, 24, 25, 26,
        //            30, 32, 34, 37, 38, 40, 41}
        //);

        //AddLevelToDictionary(tat_easy_7);

        //Level tat_easy_8 = new Level(
        //    LevelMode.TIME_AND_TRY,
        //    LevelDifficulty.MEDIUM,
        //    8,
        //    0.0f,
        //    new int[]{10, 11, 12, 16, 17, 18, 19, 20, 23, 24, 26,
        //            27, 30, 31, 32, 33, 34, 38, 39, 40}
        //);

        //AddLevelToDictionary(tat_easy_8);
        #endregion
    }

    void AddLevelToDictionary(Level level)
    {
        if (levels.ContainsKey(level.mode) == false)
            levels[level.mode] = new Dictionary<int, Level>();

        levels[level.mode][level.levelNo] = level;
    }

    public void SetupLevel()
    {
        currLevel = levels[currLevelMode][currLevelNo];

        List<int> numbers = setNumbersList();

        for (int cardOrder = 1; cardOrder <= tableSize; cardOrder++)
        {
            Transform card = TableTransform.Find(cardOrder.ToString());
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

            card.GetComponent<Card>().cardOpened = false;
        }

        timePassed = 0;
        wrongTries = 0;
        nextNumber = 1;
        currLevel.currCompleted = false;

        TableTransform.gameObject.SetActive(true);
        nextNumberText.enabled = true;
    }

    /// <summary>
    /// we need the number list to make a ramdom list of numbers
    /// </summary>
    List<int> setNumbersList()
    {
        List<int> numbers = new List<int>();
        for (int i = 1; i < currLevel.totalCardCount; i++)
            numbers.Add(i);

        return numbers;
    }

    /// <summary>
    /// show or hide the screen
    /// </summary>
    /// <param name="failed">if we fail then we have to show that info</param>
    public void ChangeSucceedScreenState(bool failed = false)
    {
        succeedScreen.SetActive(!succeedScreen.activeSelf);
        TableTransform.gameObject.SetActive(!TableTransform.gameObject.activeSelf);
        nextNumberText.enabled = !nextNumberText.enabled;

        // Yapılacak hata sayısı için üst ve alt limit belirliyorum ve buna göre oranlıyorum.
        // Aynı şekilde süre içinde. Mantık bu kadar basit.
        // tableSize * factor -> factor sayesinde ayar yapabiliriz. Duruma göre belki
        // level sayısınıda faktör olarak ekleriz.

        float starPercentForTries;
        float wrongTryUpperLimit = currLevel.totalCardCount * 2.0f;
        float wrongTryLowerLimit = currLevel.totalCardCount * 0.7f;

        starPercentForTries = (wrongTryUpperLimit - wrongTries) / (wrongTryUpperLimit - wrongTryLowerLimit);
        starPercentForTries = Mathf.Clamp01(starPercentForTries);
        //Debug.Log("starPercentForTries: " + starPercentForTries);

        float starPercentForTime;
        float passedTimeUpperLimit = currLevel.totalCardCount * 3.0f;
        float passedTimeLowerLimit = currLevel.totalCardCount * 1.5f;

        starPercentForTime = (passedTimeUpperLimit - timePassed) / (passedTimeUpperLimit - passedTimeLowerLimit);
        starPercentForTime = Mathf.Clamp01(starPercentForTime);
        //Debug.Log("starPercentForTime: " + starPercentForTime);

        currLevel.starPercent = (starPercentForTime + starPercentForTries) / 2.0f;
        //Debug.Log("starPercent: " + currLevel.starPercent);

        StartCoroutine(FillImage(currLevel.starPercent));

        // if 90% of third star if full, fill line sprite
        if (currLevel.starPercent >= 0.957f)
            starLines[2].sprite = fullStarLineSprite;
        else
            starLines[2].sprite = backStarLineSprite;

        // if 90% of second star if full, fill line sprite
        if (currLevel.starPercent >= 0.627f)
            starLines[1].sprite = fullStarLineSprite;
        else
            starLines[1].sprite = backStarLineSprite;

        // if 90% of first star if full, fill line sprite
        if (currLevel.starPercent >= 0.297f)
            starLines[0].sprite = fullStarLineSprite;
        else
            starLines[0].sprite = backStarLineSprite;
    }

    IEnumerator FillImage(float _fillAmount)
    {
        float filled = 0;
        while (filled <= _fillAmount)
        {
            filled = filled + Time.deltaTime * fillSpeed;
            starImage.fillAmount = filled;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currLevelMode == LevelMode.TIME_AND_TRY)
        {
            if (currLevel.currCompleted == true)
                return;

            timePassed += Time.deltaTime;

            if (nextNumber >= currLevel.totalCardCount)
            {
                currLevelNo++;
                currLevel.currCompleted = true;
                currLevel.completed = true;

                ChangeSucceedScreenState();

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

    void UpdateTextMesh()
    {
        levelText.text = "LEVEL " + currLevelNo;
        nextNumberText.text = nextNumber.ToString();
        timePassedText.text = String.Format("{0:F2}", timePassed);
        wrongTriesText.text = wrongTries.ToString();
    }
}
