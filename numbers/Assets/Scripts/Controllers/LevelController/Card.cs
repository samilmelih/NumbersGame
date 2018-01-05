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

	TextMeshProUGUI cardText;
	Button cardButton;
	Image cardImage;

    // Use this for initialization
    void Start()
    {
        cardText   = GetComponentInChildren<TextMeshProUGUI>();
		cardImage  = GetComponentInChildren<Image>();
		cardButton = GetComponent<Button>();

		if(active == false)
		{
			cardButton.enabled = false;
			cardText.enabled = false;
			cardImage.enabled = false;
		}
		else
		{
			cardText.text      = cardNumber.ToString();
			cardButton.enabled = true;
			cardText.enabled   = false;
			cardImage.enabled  = true;
		}

		cardButton.onClick.AddListener(
            btnCard_Clikced
		);
    }

    // Update is called once per frame
    void Update()
    {
		LevelController gameCont = LevelController.Instance;

		if (cardOpened == false || cardCleared == true || gameCont.showingAllCards == true)
			return;
		
		if(timeLeft <= 0)
		{
			// Card is closed, change sprite.
			cardImage.sprite = LevelController.Instance.closeCardSprite;
        	cardText.enabled = false;
			cardOpened = false;
        	timeLeft = waitingTime;
    	}
		else
			timeLeft -= Time.deltaTime;
    }

    public void btnCard_Clikced()
    {
		LevelController levelCont = LevelController.Instance;

		// Can not click cards when level is paused.
		if(levelCont.showingAllCards || levelCont.levelPaused || levelCont.levelCompleted)
			return;

		// Players can play music even if card is open
		MusicController.Instance.PlayCardNote(this);
		levelCont.UICont.lastOpenedCard = this.gameObject;

		// If level is cleared then return
        if (cardCleared == true)
        	return;

		// First card is opened. We can start time.
		if(levelCont.levelStarted == false)
			levelCont.levelStarted = true;

		// Card is opened, change sprite.
		cardImage.sprite = levelCont.openCardSprite;

		if (cardNumber == levelCont.nextNumber)
        {
			levelCont.nextNumber++;
			cardCleared = true;
			cardOpened  = true;
        }
		else if(levelCont.levelMode == LevelMode.NO_MISTAKE)
		{
			levelCont.levelFinished = true;
		}
        else if(cardOpened == false)
		{
			levelCont.wrongTries++;
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
		LevelController gameCont = LevelController.Instance;

		Color c = cardImage.color;
		c.a = 0.5f;
		cardImage.color = c;

		cardText.enabled = true;
		cardImage.sprite = gameCont.openCardSprite;
		SetAlphaOfCardImage(cardImage, 1f);
	}
	
	public void OpenCardTransparent()
	{
		LevelController gameCont = LevelController.Instance;

		cardText.enabled = true;
		cardImage.sprite = gameCont.openCardSprite;
		SetAlphaOfCardImage(cardImage, 0.5f);
	}
		
	public void CloseCard()
	{
		LevelController gameCont = LevelController.Instance;
		LevelDifficulty difficulty = gameCont.currLevel.difficulty;

		timeLeft = waitingTime;
		cardText.enabled = false;
		cardImage.sprite = gameCont.closeCardSprite;
	}

	void SetAlphaOfCardImage(Image cardImage, float alpha)
	{
		Color c = cardImage.color;
		c.a = alpha;
		cardImage.color = c;
	}
}
