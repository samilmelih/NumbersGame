﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardNumber;
	public bool active;

	public bool cardOpened;

    private float timeLeft = 1f;
    private float waitingTime = 1f;

    //TODO: Add a sprite to this cards maybe???? Yeap :D

	private TextMeshProUGUI cardText;

    // Use this for initialization
    void Start()
    {
        cardText = GetComponentInChildren<TextMeshProUGUI>();

        GetComponent<Button>().onClick.AddListener(
            btnCard_Clikced
		);
    }

    // Update is called once per frame
    void Update()
    {
		if (cardOpened == false)
		{
			if(timeLeft <= 0)
			{
            	cardText.enabled = false;
            	timeLeft = waitingTime;
        	}
        	
			if(cardText.enabled == true)
			{
				timeLeft -= Time.deltaTime;
			}
		}
    }

    public void btnCard_Clikced()
    {
		// Players can play music even if card is open
		MusicController.Instance.PlayCardTone(this);

        if (cardOpened)
		{
        	return;
		}

        //if (GameController.Instance.goWrongCard != null)
        //{
        //    GameController.Instance.goWrongCard.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        //}

        cardText.enabled = true;

        if (cardNumber == GameController.Instance.nextNumber)
        {
			
            GameController.Instance.nextNumber++;
            cardOpened = true;

            //GameController.Instance.goWrongCard = null;
        }
        else
        {
            GameController.Instance.wrongTries++;
        }

        //check if this number is the same with the number we are looking for

        ///////if it is not shuffle all cards maybe(hard game mode)

        //if it is not return back

        //if it is +1 to the var at the gamecontroller script maybe
    }
}