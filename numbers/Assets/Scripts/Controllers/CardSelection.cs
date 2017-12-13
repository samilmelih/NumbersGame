using System;
using UnityEngine;
using UnityEngine.UI;


public class CardSelection : MonoBehaviour, IComparable
{
    [Header("Card Info")]
    public int index;
    public Color selectColor = Color.blue;
    public Color deselectColor = Color.white;
    [Header("UnityStuffs")]
    public LevelManager generatorMaster;

    private bool cardSelected;
    // Use this for initialization
    void Start()
    {
        generatorMaster = LevelManager.Instance;
    }


    public void OnCardClikced()
    {
        if (!cardSelected)
        {
            GetComponentInChildren<Image>().color = selectColor;
            cardSelected = true;
            generatorMaster.AddCardToList(this);
        }
        else
        {
            GetComponentInChildren<Image>().color = deselectColor;
            cardSelected = false;
            generatorMaster.RemoveCardFromList(this);
        }
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        return this.index.CompareTo(((CardSelection)obj).index);
    }
}
