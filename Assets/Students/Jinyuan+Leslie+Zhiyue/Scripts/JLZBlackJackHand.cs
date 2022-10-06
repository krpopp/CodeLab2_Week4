using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class JLZBlackJackHand : BlackJackHand {

	protected override void SetupHand(){
		// get DeckOfCards
		deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();
		// create a list of cards from DeckOfCards
		hand = new List<DeckOfCards.Card>();
		//HitMe();
		//HitMe();

		//player wins if the hand cards equal to 21
		// BUG FIX: BlackJack when player starts with 21
		/*if (handVals == 21)
		{
            JLZBlackJackManager gameManager = GameObject.Find("Game Manager").GetComponent<JLZBlackJackManager>();
			ChipsManager chipsManager = GameObject.Find("ChipsManager").GetComponent<ChipsManager>();
            gameManager.BlackJack();
			chipsManager.PlayerWin();
        }*/
	}
	

	//display the value of the card
	protected override void ShowValue(){
		handVals = GetHandValue();
			
		total.text = "Player: " + handVals;

		// if the sum value of cards are above 21, the player busts
		if(handVals > 21){
			GameObject.Find("Game Manager").GetComponent<JLZBlackJackManager>().PlayerBusted();

        }
	}


}
