using System.Collections.Generic;
using UnityEngine;

public class GeneratorMaster : MonoBehaviour
{
    public static GeneratorMaster Instance;

    private List<int> selectedCardIndexList;

    public GameObject cardPrefab;
    public Transform cardTabletransform;

    private int row = 7;
    private int column = 7;

    // Use this for initialization
    void Start()
    {
        if (Instance != null)
            return;

        Instance = this;

        selectedCardIndexList = new List<int>();
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                GameObject go = Instantiate(cardPrefab, cardTabletransform);
                go.GetComponent<CardSelection>().index = row * i + j;
            }

        }


    }

    internal void RemoveCardFromList(int index)
    {
        selectedCardIndexList.Remove(index);
    }

    public void AddCardToList(int cardIndex)
    {
        selectedCardIndexList.Add(cardIndex);
    }


}
