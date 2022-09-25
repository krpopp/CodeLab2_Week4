using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_BlackJackManager : BlackJackManager
{
	[SerializeField] GameObject hitButton, stayButton;


	//function to return the total from the cards in hand
	public override int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = 0;

		foreach(DeckOfCards.Card handCard in hand){
			handValue += handCard.GetCardHighValue();
		}
		if (handValue > 21) {
		 	foreach(DeckOfCards.Card handCard in hand) {
		 		if (handCard.GetCardHighValue() == 11) handValue -= 10;
		 		if (handValue <= 21) break;
		 	}
		}
		return handValue;
	}

	public void ResetScene() {
		GameObject.Find("Player Hand Value").GetComponent<AJT_BlackJackHand>().ResetHand();
		GameObject.Find("Dealer Hand Value").GetComponent<AJT_DealerHand>().ResetHand();
		ShowPlayerButtons();
	}

	public void ShowPlayerButtons()
	{
		hitButton.SetActive(true);
		stayButton.SetActive(true);
		tryAgain.SetActive(false);
		statusText.text = "";
	}

}
