using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AJT_DealerHand : DealerHand
{
    //because this script inherits from DealerHand which in turn inherits from BlackJackHand
    //the overidden functionality of AJT_BlackJackHand is not made available to this.
    public List<DeckOfCards.Card> Hand { get { return hand; } }

	public bool Reveal;

    protected override void SetupHand(){
		Reveal = false;

		deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();
		hand = new List<DeckOfCards.Card>();
		//Add two cards to player hand
		HitMe();
		HitMe();

        //hide first card
        GameObject cardOne = transform.GetChild(0).gameObject;
        Debug.Log(cardOne.name);
        cardOne.GetComponentInChildren<Text>().text = "";
        cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
        cardOne.GetComponentsInChildren<Image>()[1].enabled = false;
    }

    //BUG FIX
    //removes the remaining cards from the previous round and sets up new hands
    public void ResetHand() {
		foreach (Transform child in transform) { Destroy(child.gameObject); }
		SetupHand();
	}

	protected override void ShowValue(){

		if(hand.Count > 1){

			//shows value of dealer's revealed cards only
			if(!Reveal){
				//sets handVals to value of revealed card
				handVals = hand[1].GetCardHighValue();

				total.text = "Dealer: " + handVals + " + ???";
			} else {
				//shows value of all dealer cards
				handVals = GetHandValue();

				total.text = "Dealer: " + handVals;

				BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<BlackJackManager>();

				//handles dealer bust
				if(handVals > 21){
					manager.DealerBusted();
					//draws new card for dealer if dealer is under 17
				} else if(!DealStay(handVals)){
					Invoke("HitMe", 1);
				} else {
					// once dealer stays, compares dealer and player hand values
					BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

					if(handVals < playerHand.handVals){
						//player wins if player has higher total than dealer
						manager.PlayerWin();
					} else {
						//player loses in case of tie or higher dealer hand
						manager.PlayerLose();
					}
				}
			}
		}
	}

    //function to update game UI to reveal previously hidden dealer card
	public void RevealDealer() {
		//sets true when card is revealed
		Reveal = true;

		GameObject cardOne = transform.GetChild(0).gameObject;

		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		//passes previously hidden card to ShowCard, updates game UI to reveal that card
		ShowCard(hand[0], cardOne, 0);
		//updates value of dealer hand
		ShowValue();
	}
}
