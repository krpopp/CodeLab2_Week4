using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AJT_BlackJackManager : BlackJackManager
{
	AJT_DeckOfCards deck;
	AJT_DealerHand dealerHand;
	AJT_BlackJackHand playerHand;

    //BUG FIX
    //references to access inactive game objects
	[SerializeField] GameObject hitButton, stayButton, valueButton, actionButtonOne, actionButtonTwo;

	protected virtual void Start() {
		deck = GameObject.Find("Deck").GetComponent<AJT_DeckOfCards>();
		dealerHand = GameObject.Find("Dealer Hand").GetComponent<AJT_DealerHand>();
		playerHand = GameObject.Find("Player Hand").GetComponent<AJT_BlackJackHand>();
	}


	//function to return the total from the cards in hand
	public override int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = 0;

		//Get highest possible total of hand
		foreach(DeckOfCards.Card handCard in hand){
			if (handCard != null) { 
				if (!(handCard is AJT_Card))
					handValue += handCard.GetCardHighValue();
				else
				{
					AJT_Card c = handCard as AJT_Card;
					handValue += c.GetCardValue();
				}
			}
		}
        //BUG FIX
        //Checks if the total is over 21 and incrementally change ace values
		if (handValue > 21) {
		 	foreach(DeckOfCards.Card handCard in hand) {
		 		if (handCard.GetCardHighValue() == 11) handValue -= 10;
		 		if (handValue <= 21) break;
		 	}
		}
		return handValue;
	}

    //BUG FIX
    //instead of reloading the scene on Try Again it resets the scene
    //resets scene to preserve deck contents
	public void ResetScene() {
		ShowPlayerButtons();
		playerHand.ResetHand();
		dealerHand.ResetHand();
	}

    //Function to reset buttons in the scene
	public void ShowPlayerButtons()
	{
		hitButton.SetActive(true);
		stayButton.SetActive(true);
		tryAgain.SetActive(false);
		valueButton.SetActive(false);
		actionButtonOne.SetActive(false);
		statusText.text = "";
	}

	//Functions rewrote to access more and inactive objects
    public void HideAllButtons() {
        hitButton.SetActive(false);
        stayButton.SetActive(false);
		actionButtonOne.SetActive(false);
		valueButton.SetActive(false);
	}

	//Couldn't override bc they aren't static, created new functions
	new public void BlackJack(){
        GameOverText("BLACK JACK", Color.green);
		HideAllButtons();
    }

    new public void PlayerBusted(){
		GameOverText("YOU" + '\n' + "BUST", Color.red);
		HideAllButtons();
    }

	new public void DealerBusted() {
		GameOverText("DEALER BUSTS" + '\n' + "YOU WIN", Color.green);
		if (!dealerHand.reveal) { 
			dealerHand.reveal = true;
			dealerHand.RevealCard();
		}
		HideAllButtons();
	}

    //Wrapped multiple button calls into one function that can be used to auto stay
    public void PlayerStays() {
        dealerHand.RevealDealer();
		HideAllButtons();
    }

	public void ShowValueButton(AJT_Card card)
	{
		//Enable button
		valueButton.SetActive(true);
		//Set button's onClick event to delegate void from AJT_Card param
		Button b = valueButton.GetComponent<Button>();
		b.onClick.RemoveAllListeners(); //Remove any previous delegates
		b.onClick.AddListener(delegate(){card.UseValue();}); //Add delegate void
	}

	public void ShowActionButtonOne(AJT_Card card, string action)
	{
		//Enable button
		actionButtonOne.SetActive(true);
		actionButtonOne.GetComponentInChildren<Text>().text = action; //Set button text to string param

		Button b = actionButtonOne.GetComponent<Button>();
		b.onClick.RemoveAllListeners(); //Remove any previous delegates
		b.onClick.AddListener(delegate(){card.ActionOne();}); //Add delegate void
	}

    public void ShowActionButtonTwo(AJT_Card card, string action)
    {
        actionButtonTwo.SetActive(true);
        actionButtonTwo.GetComponentInChildren<Text>().text = action;

        Button b = actionButtonTwo.GetComponent<Button>();
		b.onClick.RemoveAllListeners(); //Remove any previous delegates
		b.onClick.AddListener(delegate () { card.ActionTwo(); }); //Add delegate void
	}

	//Functions for abstract Card classes to create and destroy cards
	public void DestroyCard(GameObject go) {
		DestroyImmediate(go);
	}

	public GameObject CreateCard() {
		GameObject newCard = Instantiate(deck.GetComponent<AJT_DeckOfCards>().cardPrefab);
		return newCard;
	}

	//Display the function of the enhanced to the player
	public void EnhancedCardTooltip(int index) {
		statusText.color = Color.white;
		switch (index) {
			case 2:
				statusText.text = "USE NEGATIVE VALUE" + '\n' + "OR" + '\n' + "USE VALUE";
				break;
			case 3:
				statusText.text = "SWAP WITH DEALER" + '\n' + "OR" + '\n' + "USE VALUE";
				break;
			case 4:
				statusText.text = "GIVE TO DEALER" + '\n' + "OR" + '\n' + "USE VALUE";
				break;
			case 5:
				break;
		}
	}
}
