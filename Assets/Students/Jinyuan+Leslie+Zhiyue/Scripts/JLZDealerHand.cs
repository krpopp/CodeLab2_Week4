using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JLZDealerHand : DealerHand{

	bool reveal;

	// hide the image and text of the card
	protected override void SetupHand(){
		base.SetupHand();

		GameObject cardOne = transform.GetChild(0).gameObject;
		cardOne.GetComponentInChildren<Text>().text = "";
		cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = false;

		reveal = false;
	}
		
	protected override void ShowValue(){

		// if the dealer has more than 1 card
		if(hand.Count > 1){
			// if not revealled, show one card info in the dealer's hand
			if(!reveal){
				handVals = hand[1].GetCardHighValue();

				total.text = "Dealer: " + handVals + " + ???";
			} else {
				// if revealled, show all values in the dealer's hand
				handVals = GetHandValue();

				total.text = "Dealer: " + handVals;

				BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<BlackJackManager>();

				// if the dealer's cards' value is more than 21, the dealer busts
				if(handVals > 21){
					manager.DealerBusted();
				} else if(!DealStay(handVals)){ // if the value is less than 17, the dealer draws card
					Invoke("HitMe", 1);
				} else { // if the value is between 17 and 21
					BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();
					// if player card's value is more than the dealer's, plaer wins; otherwise, dealer wins
					if(handVals < playerHand.handVals){
						manager.PlayerWin();
					} else {
						manager.PlayerLose();
					}
				}
			}
		}
	}

}
