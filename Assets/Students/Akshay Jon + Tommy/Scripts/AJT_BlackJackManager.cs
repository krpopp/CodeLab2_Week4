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
		ShowPlayerButtons();
		GameObject.Find("Player Hand").GetComponent<AJT_BlackJackHand>().ResetHand();
		GameObject.Find("Dealer Hand").GetComponent<AJT_DealerHand>().ResetHand();
	}

    //Function to reset buttons in the scene
	public void ShowPlayerButtons()
	{
		hitButton.SetActive(true);
		stayButton.SetActive(true);
		tryAgain.SetActive(false);
		statusText.text = "";
	}

	//Functions rewrote to access inactive objects
    //Couldn't override bc they aren't static, created new functions
    new public void HidePlayerButtons() {
        hitButton.SetActive(false);
        stayButton.SetActive(false);
    }

    new public void BlackJack(){
        GameOverText("Black Jack!", Color.green);
        HidePlayerButtons();
    }

    new public void PlayerBusted(){
        HidePlayerButtons();
        GameOverText("YOU BUST", Color.red);
    }

    //Wrapped multiple button calls into one function that can be used to auto stay
    public void PlayerStays() {
        HidePlayerButtons();
        GameObject.Find("Dealer Hand").GetComponent<AJT_DealerHand>().RevealDealer();
    }
}
