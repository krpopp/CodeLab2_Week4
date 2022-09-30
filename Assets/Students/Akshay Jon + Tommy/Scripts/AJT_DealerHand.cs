using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//because this script inherits from DealerHand which in turn inherits from Black Jack Hand
//the overidden functions from Black Jack hand is not made available to this.
public class AJT_DealerHand : DealerHand
{
	//public accessor for hands
	public List<DeckOfCards.Card> Hand { get { return hand; } }

	//new private bool bc can't access base private bool
    bool Reveal = false;

    //BUG FIX
    //removes the remaining cards from the previous round and sets up new hands
	public void ResetHand() {
		//This loop gave us so much trouble.
		//We weren't grabbing all the children, and w/o DestroyImmediate the code doesn't execute in time for other calls
        for (int i = transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

		Reveal = false;
		SetupHand();
    }


	//Overriden to access new private bool reveal
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
