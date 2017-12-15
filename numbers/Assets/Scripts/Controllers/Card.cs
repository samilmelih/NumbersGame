﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardNumber;
	public bool active;

	// This is true when next card is itself
	public bool cardCleared;

	// This is true when number is showed to player
	public bool cardOpened;

    private float timeLeft = 0.5f;
    private float waitingTime = 0.5f;

	private TextMeshProUGUI cardText;

	Image cardImage;

    // Use this for initialization
    void Start()
    {
        cardText = GetComponentInChildren<TextMeshProUGUI>();
		cardImage = GetComponentInChildren<Image>();

        GetComponent<Button>().onClick.AddListener(
            btnCard_Clikced
		);
    }

    // Update is called once per frame
    void Update()
    {
		// FIXME: Sprite change code should be change. We should not
		// reassign every Update. Find a solution.
		if (cardCleared == false)
		{
			if(timeLeft <= 0)
			{
				// Card is closed, change sprite.
				cardImage.sprite = GameController.Instance.closeCardSprite;

            	cardText.enabled = false;
				cardOpened = false;
            	timeLeft = waitingTime;
        	}
			else if(cardOpened == true)
			{
				timeLeft -= Time.deltaTime;
			}
		}
    }

    public void btnCard_Clikced()
    {
		GameController gameCont = GameController.Instance;

		// Can not click cards when level is paused.
		if(gameCont.levelPaused == true)
			return;

		// Players can play music even if card is open
		MusicController.Instance.PlayCardTone(this);

		// If level is cleared then return
        if (cardCleared == true)
        	return;

		// First card is opened. We can start time.
		if(gameCont.levelStarted == false)
			gameCont.levelStarted = true;

		// Card is opened, change sprite.
		cardImage.sprite = gameCont.openCardSprite;

		if (cardNumber == gameCont.nextNumber)
        {
			gameCont.nextNumber++;
			gameCont.currOpenedCards.Add(cardNumber);
			cardCleared = true;
			cardOpened  = true;

        }
        else if(cardOpened == false)
		{
			gameCont.wrongTries++;
        }

		cardText.enabled = true;
		cardOpened = true;


        // check if this number is the same with the number we are looking for
        // if it is not shuffle all cards maybe(hard game mode)
        // if it is not return back
        // if it is +1 to the var at the gamecontroller script maybe
    }

	public void OpenCard()
	{
		GameController gameCont = GameController.Instance;

		cardText.enabled = true;
		cardImage.sprite = gameCont.openCardSprite;
	}

	public void CloseCard()
	{
		GameController gameCont = GameController.Instance;

		cardText.enabled = false;
		cardImage.sprite = gameCont.closeCardSprite;
	}
}
