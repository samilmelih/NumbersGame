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




    // Use this for initialization
    void Start()
    {
        if (Instance == null)
            Instance = this;

        levelTime = basicLevelMultiplier;

        cards = new List<GameObject>();

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

        if (succeedScreen.activeSelf)
        {
            
        }

        levelCompleted = true;

		int tableSize = row * row;
		int starCountForTries;

		if(wrongTries <= (int) (tableSize * 0.25f))		// + 1/4
		{
			starCountForTries = 3;
		}
		else if(wrongTries <= (int) (tableSize * 0.5f))	// + 2/4
		{
			starCountForTries = 2;
		}
		else if(wrongTries <= (int) (tableSize * 0.75f))	// + 3/4
		{
			starCountForTries = 1;
		}
		else
		{
			starCountForTries = 1;	// FIXME: We do not have 0 star sprite yet
		}

		int starCountForTime;

		if(timePassed <= tableSize * 0.25f)		// + 1/4
		{
			starCountForTime = 3;
		}
		else if(timePassed <= tableSize * 0.5f)	// + 2/4
		{
			starCountForTime = 2;
		}
		else if(timePassed <= tableSize * 0.75f)	// + 3/4
		{
			starCountForTime = 1;
		}
		else
		{
			starCountForTime = 1;	// FIXME: We do not have 0 star sprite yet
		}

		// We round up here
		starCount = (int) Mathf.Ceil((starCountForTime + starCountForTries) / 2.0f);

	/*
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
	*/

        string path = string.Format("Sprites/UISprites/Stars/{0}Star", starCount);
        Sprite spr = Resources.Load<Sprite>(path);
        starShow.GetComponent<Image>().sprite = spr;
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
            if (level < 8)
                row++;
            else
            {
                //think something else to increase the level
            }

            ChangeSucceedScreenState();
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
