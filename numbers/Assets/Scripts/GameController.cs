using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    private float starPercent = 0;
    private int starCount;
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
    [HideInInspector]
    public GameObject goWrongCard;

    private List<int> numbers;
    [Header("Unity Stuffs")]
    public GameObject starShow;
    public GameObject cardPrefab;
    public GameObject succeedScreen;
    public Transform TableTransform;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI currentNumberText;
    public TextMeshProUGUI timePassedText;
    public TextMeshProUGUI wrongTriesText;
    public TextMeshProUGUI remainingTimeText;


    [Header("Level Handler")]
    public int level = 1;
    public int row = 3;

    [HideInInspector]
    public int cellSize = 180;

    [HideInInspector]
    public int currentNumber = 1;

    [HideInInspector]
    public int wrongTries = 0;

    public float timePassed = 0;
    public float levelTime;
    public float basicLevelMultiplier = 10;


    // Use this for initialization
    void Start()
    {

        if (Instance == null)
            Instance = this;

        levelTime = basicLevelMultiplier;

        cards = new List<GameObject>();

        setNumbersList();
        SetUpGame();

    }
    /// <summary>
    /// we need the number list to make a ramdom list of numbers
    /// </summary>
    void setNumbersList()
    {
        numbers = new List<int>();
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < row; j++)
            {
                numbers.Add(row * (i) + (j + 1));
                //if i and j starts from 0...
                //row * currentRowIndex + currentColumnIndex + 1 ... calculation of current number of a matrix as a vector
            }
        }
    }
    /// <summary>
    /// show or hide the screen
    /// </summary>
    /// <param name="failed">if we fail then we have to show that info</param>
    public void ChangeSucceedScreenState(bool failed = false)
    {
        succeedScreen.SetActive(!succeedScreen.activeSelf);

        if (succeedScreen.activeSelf)
        {
            wrongTriesText.text = "Wrong tries: " + wrongTries + " Times";
            timePassedText.text = String.Format("Time Passed : {0:F2} seconds", timePassed);
        }
        levelCompleted = true;

        //2^level + time + 2*(mistake)
        starPercent = Mathf.Pow(2, level) + (int)timePassed + 2 * wrongTries;

        starPercent /= Mathf.Pow(row, 2);

        if (starPercent < 2)
            starCount = 3;
        else if (starPercent >= 2 && starPercent <= 4)
            starCount = 2;
        else
            starCount = 1;

        Debug.Log("star Percent" + starPercent);

        string path = string.Format("Sprites/UISprites/Stars/{0}Star", starCount);
        Sprite spr = Resources.Load<Sprite>(path);
        starShow.GetComponent<Image>().sprite = spr;



    }
    public void SetUpGame()
    {

        timePassed = 0;
        foreach (GameObject gObject in cards)
        {
            Destroy(gObject);
        }
        cards = new List<GameObject>();

        setNumbersList();

        currentNumber = 1;

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
        if (currentNumber > row * row)
        {
            if (level < 8)
                row++;
            else
            {
                //think something else to increase the level
            }


            ChangeSucceedScreenState();
            currentNumber = 0;
        }

        if (!levelCompleted)
        {
            levelTime -= Time.deltaTime;
            timePassed += Time.deltaTime;
        }

        UpdateTextMesh();



        if (levelTime < 0)
            levelTime = 0;

        remainingTimeText.text = String.Format("Remaining Time : {0:F2}", levelTime);



    }


    void UpdateTextMesh()
    {
        levelText.text = "Current Level  :" + level;
        currentNumberText.text = "Current Number :" + currentNumber;
    }
}
