using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_BlackJackManager : BlackJackManager
{
    //BUG FIX
    //references to access inactive game objects
	[SerializeField] GameObject hitButton, stayButton;


	//function to return the total from the cards in hand
	public override int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = 0;

		//Get highest possible total of hand
		foreach(DeckOfCards.Card handCard in hand){
			handValue += handCard.GetCardHighValue();
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
		GameObject.Find("Player Hand Value").GetComponent<AJT_BlackJackHand>().ResetHand();
		GameObject.Find("Dealer Hand Value").GetComponent<AJT_DealerHand>().ResetHand();
		ShowPlayerButtons();
	}

    //Function to reset buttons in the scene
	public void ShowPlayerButtons()
	{
		hitButton.SetActive(true);
		stayButton.SetActive(true);
		tryAgain.SetActive(false);
		statusText.text = "";
	}

}
