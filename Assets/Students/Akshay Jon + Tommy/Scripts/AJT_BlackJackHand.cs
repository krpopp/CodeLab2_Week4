using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_BlackJackHand : BlackJackHand {

    public GameObject cardPrefab;

    //public accessor for hands
    public List<DeckOfCards.Card> Hand { get { return hand; } }

    //BUG FIX
    //removes the remaining cards from the previous round and sets up new hands
	public virtual void ResetHand() {
		//This loop gave us so much trouble.
		//We weren't grabbing all the children, and w/o DestroyImmediate the code doesn't execute in time for other calls
        for (int i = handBase.transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(handBase.transform.GetChild(i).gameObject);
        }
		SetupHand();
    }

    protected override void SetupHand() {
        //Set references
        deck = GameObject.Find("Deck").GetComponent<AJT_DeckOfCards>();
        hand = new List<DeckOfCards.Card>();
        //Add two cards to player hand
        Hit();
        Hit();
    }

    //Function to add card to player hand, calls all other functions in this class
	public virtual void Hit(){
		DeckOfCards.Card card = deck.DrawCard(); //Store card from top of deck

		GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject; //Instantiate prefab as that card

		ShowCard(card, cardObj, hand.Count); //Update scene UI to display card correctly 

		hand.Add(card); //Store card in local hand list

		ShowValue(); //Update scene UI to display hand total	
	}

    //BUG FIX
    //overidden to check for natural Black Jack
    protected override void ShowValue()
    {
        base.ShowValue();

        //check for blackjack
        if (handVals == 21) { 
            //call for natural blackjack
            if (hand.Count == 2) {
                GameObject.Find("Game Manager").GetComponent<AJT_BlackJackManager>().BlackJack();
            }
            //Automatically stay if the player has 21
            else
                GameObject.Find("Game Manager").GetComponent<AJT_BlackJackManager>().PlayerStays();
        }
    }


}
