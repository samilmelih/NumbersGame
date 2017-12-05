using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{


    [HideInInspector]
    public List<GameObject> cards;
    public static GameController Instance;
    [HideInInspector]
    public GameObject goWrongCard;

    private List<int> numbers;
    [Header("Unity Stuffs")]
    public GameObject cardPrefab;
    public Transform TableTransform;
    public TextMeshProUGUI levelText;
    public GameObject succeedScreen;

    [Header("Level Handler")]
    public int level = 1;
    public int row = 3;

    [HideInInspector]
    public int cellSize = 100;

    [HideInInspector]
    public int currentNumber = 1;

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
            Instance = this;

        cards = new List<GameObject>();

        setNumbersList();
        SetUpGame();

    }

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

    public void ChangeSucceedScreenState()
    {
        succeedScreen.SetActive(!succeedScreen.activeSelf);
    }
    public void SetUpGame()
    {
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
        if (currentNumber > row * row)
        {
            row++;
            levelText.text = "Current Level :" + level;
            ChangeSucceedScreenState();
        }

    }
}
