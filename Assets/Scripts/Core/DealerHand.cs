using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DealerHand : BlackJackHand {
   //sprite for the back of cards
	public Sprite cardBack;
   //represents if a  card is revealed.
	bool reveal;

	protected override void SetupHand(){ //overriding parent class with it's own info
		base.SetupHand(); //at first perform everything form the parent class

		GameObject cardOne = transform.GetChild(0).gameObject; //get the first child in hierarchy as it's the first card in the hand
		//lines from 16~18 visualize the card, flipping it and showing a specific image based on whether or not it's revealed
		cardOne.GetComponentInChildren<Text>().text = ""; //empty the text to not show anything
		cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = false;

		reveal = false;
	}
		
	protected override void ShowValue(){ //completely overriding the parent function

		if(hand.Count > 1){ //if the hand has more than 1 card
			if(!reveal){ //and it's not revealed
				handVals = hand[1].GetCardHighValue(); //Shows the value of the card revealed. 

				total.text = "Dealer: " + handVals + " + ???"; //changes text element to reflect that.
			} else {
				handVals = GetHandValue(); //Counts all cards in the dealers hand. 

				total.text = "Dealer: " + handVals; //displays 

				//assings black jack manager. 
				BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<BlackJackManager>();

				if(handVals > 21){ //if dealer hand value goes over 21;
					manager.DealerBusted(); //calls dealer bust funciton from manager. 
				} else if(!DealStay(handVals)){ //otherwise, if the dealer didn't stay, the dealer will call the hit me function
					Invoke("HitMe", 1);
				} else { //otherwise 
					//Calls the value of the player hand. 
					BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

					//compares the value of the dealer hand to the player hand, who ever is larger, wins. 
					if(handVals < playerHand.handVals){
						manager.PlayerWin();
					} else {
						manager.PlayerLose();
					}
				}
			}
		}
	}

	//does the dealer stay. 
	protected virtual bool DealStay(int handVal){
		return handVal > 17; //it stays if the handvalue is bigger than 17 it will stay. 
	}
	
	public void RevealCard(){
		reveal = true; //sets 

		//reveals the first card in the dealers harnd. 
		GameObject cardOne = transform.GetChild(0).gameObject;

		//hides cardback image, shows front image of card and enables it. 
		cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		//visualizes the card. 
		ShowCard(hand[0], cardOne, 0);

		//shows the value of the card. 
		ShowValue();
	}
}
