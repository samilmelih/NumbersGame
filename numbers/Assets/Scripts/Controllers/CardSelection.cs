﻿using System;
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

    public void OnCardClikced()
    {
        if (!cardSelected)
        {
            GetComponentInChildren<Image>().color = selectColor;
            cardSelected = true;
			if(levelGenerator == null)
				Debug.Log("wtf");
            levelGenerator.AddCardToList(this);
        }
        else
        {
            GetComponentInChildren<Image>().color = deselectColor;
            cardSelected = false;
            levelGenerator.RemoveCardFromList(this);
        }
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        return this.index.CompareTo(((CardSelection)obj).index);
    }
}
