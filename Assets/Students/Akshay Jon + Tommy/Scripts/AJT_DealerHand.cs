using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//because this script inherits from DealerHand which in turn inherits from Black Jack Hand
//the overidden functions from Black Jack hand is not made available to this.
public class AJT_DealerHand : AJT_BlackJackHand {

	public Sprite cardBack;
	bool reveal = false;

	protected override void SetupHand(){
		//this bool must be false before base.SetupHand to stop dealer from calling HitMe on the first frame of the next round
		reveal = false;

		base.SetupHand();

		//hide first card, get child from handBase instead of own transform
		GameObject cardOne = handBase.transform.GetChild(0).gameObject;
		cardOne.GetComponentInChildren<Text>().text = "";
		cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = false;
		//default value for hidden card
	}


	//Overriden to make behavior different than players
    protected override void ShowValue(){
		if(hand.Count > 1){
			//shows value of dealer's revealed cards only
			if(!reveal){
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
					BlackJackHand playerHand = GameObject.Find("Player Hand").GetComponent<BlackJackHand>();

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

	protected virtual bool DealStay(int handVal) {
		return handVal > 17;
	}

	public virtual void RevealDealer() {
		//sets true when card is revealed
		reveal = true;

		GameObject cardOne = handBase.transform.GetChild(0).gameObject;

		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		//passes previously hidden card to ShowCard, updates game UI to reveal that card
		ShowCard(hand[0], cardOne, 0);
		//updates value of dealer hand
		ShowValue();
	}
}