﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    public int cardNumber = 0;

    private bool cardOpened = false;

    //TODO: Add a sprite to this cards maybe????

    private TextMeshProUGUI txTextMeshProUguı;

    // Use this for initialization
    void Start()
    {
        txTextMeshProUguı = GetComponentInChildren<TextMeshProUGUI>();

        txTextMeshProUguı.text = cardNumber.ToString();

        GetComponent<Button>().onClick.AddListener(
            btnCard_Clikced
            );

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btnCard_Clikced()
    {
        if (cardOpened)
            return;

        if (GameController.Instance.goWrongCard != null)
        {
            GameController.Instance.goWrongCard.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }

        txTextMeshProUguı.enabled = true;

        if (cardNumber == GameController.Instance.currentNumber)
        {
            GameController.Instance.currentNumber++;
            cardOpened = true;

            GameController.Instance.goWrongCard = null;


        }
        else
        {
            GameController.Instance.goWrongCard = gameObject;
        }
        //check if this number is the same with the number we are looking for

        ///////if it is not shuffle all cards maybe(hard game mode)

        //if it is not return back

        //if it is +1 to the var at the gamecontroller script maybe
    }
}
