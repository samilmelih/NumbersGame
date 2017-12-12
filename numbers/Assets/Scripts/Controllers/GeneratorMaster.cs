using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GeneratorMaster : MonoBehaviour
{
    public static GeneratorMaster Instance;

    public List<CardSelection> selectedCardList;

    private DropdownController dropdownController;

    public GameObject cardPrefab;
    public Transform cardTabletransform;


    private int maxMapSize = 7;//7x7
    // Use this for initialization
    void Start()
    {
        if (Instance != null)
            return;

        dropdownController = DropdownController.Instance;
        Instance = this;

        selectedCardList = new List<CardSelection>();



    }

    public void RemoveCardFromList(CardSelection card)
    {
        selectedCardList.Remove(card);
    }

    public void AddCardToList(CardSelection card)
    {
        selectedCardList.Add(card);
    }

    public void DeselectCards()
    {
        List<CardSelection> cards = new List<CardSelection>(selectedCardList);
        foreach (CardSelection card in cards)
        {
            card.OnCardClikced();
        }
    }

    public void CreateLevel()
    {

        //check if we have all data that we need


        //create level if you need(we will in 2. update)


        //save to the file

        //we dont have to sort but it will look better and clean
        selectedCardList.Sort();


        string levelContext = string.Format("{0},{1},", dropdownController.levelMode,
            dropdownController.levelDifficulty);

        foreach (var card in selectedCardList)
        {
            levelContext += card.index + "-";
        }

        StreamWriter streamWriter = new StreamWriter("levels.txt", true);

        streamWriter.WriteLine(levelContext);

        streamWriter.Close();
    }

    public void GenerateMap()
    {

        for (int i = 0; i < maxMapSize; i++)
        {
            for (int j = 0; j < maxMapSize; j++)
            {
                GameObject go = Instantiate(cardPrefab, cardTabletransform);
                go.GetComponent<CardSelection>().index = maxMapSize * i + j;
            }

        }
    }



}
