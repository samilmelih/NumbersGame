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
    public LevelGenerator levelGenerator;

    private bool cardSelected;

    public void SelectCard()
    {
        GetComponentInChildren<Image>().color = selectColor;
        cardSelected = true;
    }

    public void DeselecCard()
    {
        GetComponentInChildren<Image>().color = deselectColor;
        cardSelected = false;
    }

    public void OnCardClikced()
    {
        if (!cardSelected)
        {
            SelectCard();
            levelGenerator.AddCardToList(this);
        }
        else
        {
            DeselecCard();
            levelGenerator.RemoveCardFromList(this);
        }
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        return index.CompareTo(((CardSelection)obj).index);
    }
}
