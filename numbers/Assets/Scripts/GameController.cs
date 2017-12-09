using System;
using System.Collections;
using System.Collections.Generic;
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
    [HideInInspector]
    public bool levelCompleted = false;

    [HideInInspector]
    public List<GameObject> cards;

    public static GameController Instance;

    private List<int> numbers;


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

    [Header("Level Handler")]
    public int level = 1;
    public int row = 3;
    public float timePassed = 0;
    public float levelTime;
    public float basicLevelMultiplier = 10;


    [HideInInspector]
    public int cellSize = 150;

    [HideInInspector]
    public int nextNumber = 1;

    [HideInInspector]
    public int wrongTries = 0;

    private float fillSpeed = .7f;

    Sprite fullStarLineSprite;
    Sprite backStarLineSprite;


    // Use this for initialization
    void Start()
    {
        if (Instance == null)
            Instance = this;

        levelTime = basicLevelMultiplier;

        cards = new List<GameObject>();

        fullStarLineSprite = Resources.Load<Sprite>("Sprites/UISprites/Stars/Star_Lines_Full");
        backStarLineSprite = Resources.Load<Sprite>("Sprites/UISprites/Stars/Star_Lines_Back");

        SetUpGame();
    }

    /// <summary>
    /// we need the number list to make a ramdom list of numbers
    /// </summary>
    void setNumbersList()
    {
        numbers = new List<int>();
        int tableSize = row * row;

        for (int i = 1; i <= tableSize; i++)
            numbers.Add(i);
    }

    public void SetUpGame()
    {
        foreach (GameObject go in cards)
        {
            Destroy(go);
        }

        cards = new List<GameObject>();

        setNumbersList();

        timePassed = 0;
        nextNumber = 1;

        TableTransform.gameObject.SetActive(true);
        nextNumberText.enabled = true;

        TableTransform.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(row * cellSize, row * cellSize);
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < row; j++)
            {
                int val = Random.Range(0, numbers.Count);

                GameObject go = Instantiate(cardPrefab, TableTransform);
                go.GetComponent<Card>().cardNumber = numbers[val];

                numbers.RemoveAt(val);
                cards.Add(go);
            }
        }

        //we dont need this after we get the NUMBERS
        numbers = null;

        wrongTries = 0;
    }

    /// <summary>
    /// show or hide the screen
    /// </summary>
    /// <param name="failed">if we fail then we have to show that info</param>
    public void ChangeSucceedScreenState(bool failed = false)
    {
        succeedScreen.SetActive(!succeedScreen.activeSelf);

        levelCompleted = true;

        // Yapılacak hata sayısı için üst ve alt limit belirliyorum ve buna göre oranlıyorum.
        // Aynı şekilde süre içinde. Mantık bu kadar basit.
        // tableSize * factor -> factor sayesinde ayar yapabiliriz. Duruma göre belki
        // level sayısınıda faktör olarak ekleriz.

        int tableSize = row * row;
        float starPercentForTries;
        float wrongTryUpperLimit = tableSize * 2.0f;
        float wrongTryLowerLimit = tableSize * 0.7f;

        starPercentForTries = (wrongTryUpperLimit - wrongTries) / (wrongTryUpperLimit - wrongTryLowerLimit);
        starPercentForTries = Mathf.Clamp01(starPercentForTries);
        Debug.Log("starPercentForTries: " + starPercentForTries);

        float starPercentForTime;
        float passedTimeUpperLimit = tableSize * 3.0f;
        float passedTimeLowerLimit = tableSize * 1.5f;

        starPercentForTime = (passedTimeUpperLimit - timePassed) / (passedTimeUpperLimit - passedTimeLowerLimit);
        starPercentForTime = Mathf.Clamp01(starPercentForTime);
        Debug.Log("starPercentForTime: " + starPercentForTime);

        float starPercent = (starPercentForTime + starPercentForTries) / 2.0f;
        Debug.Log("starPercent: " + starPercent);

        StartCoroutine(FillImage(starPercent));

        // if 90% of third star if full, fill line sprite
        if (starPercent >= 0.957f)
            starLines[2].sprite = fullStarLineSprite;
        else
            starLines[2].sprite = backStarLineSprite;

        // if 90% of second star if full, fill line sprite
        if (starPercent >= 0.627f)
            starLines[1].sprite = fullStarLineSprite;
        else
            starLines[1].sprite = backStarLineSprite;

        // if 90% of first star if full, fill line sprite
        if (starPercent >= 0.297f)
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
        if (levelTime <= 0 && !succeedScreen.activeSelf)
        {
            //show succeed screen but with failed datas
            //ChangeSucceedScreenState(true);
            levelCompleted = true;
        }

        if (nextNumber > row * row)
        {
            ChangeSucceedScreenState();

            if (level < 8)
                row++;
            else
            {
                //think something else to increase the level
            }

            TableTransform.gameObject.SetActive(false);
            nextNumberText.enabled = false;
            nextNumber = 0;
        }

        if (!levelCompleted)
        {
            levelTime -= Time.deltaTime;
            timePassed += Time.deltaTime;
        }

        if (levelTime < 0)
            levelTime = 0;

        UpdateTextMesh();
    }

    void UpdateTextMesh()
    {
        levelText.text = "LEVEL " + level;
        nextNumberText.text = nextNumber.ToString();
        //remainingTimeText.text = String.Format("{0:F2}", levelTime);
        timePassedText.text = String.Format("{0:F2}", timePassed);
        wrongTriesText.text = wrongTries.ToString();
    }
}
