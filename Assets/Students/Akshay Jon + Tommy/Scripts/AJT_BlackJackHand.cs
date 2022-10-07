using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AJT_BlackJackHand : BlackJackHand {

    //public accessor for hands
    public List<DeckOfCards.Card> Hand { get { return hand; } set { hand = value;} }

    //bool for enhanced card buffering
    public bool choosing;

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
        deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();
        hand = new List<DeckOfCards.Card>();
        //Add two cards to player hand
        Hit();
        Hit();
    }

    //Function to add card to player hand, calls all other functions in this class
	public virtual void Hit(){

		DeckOfCards.Card card = deck.DrawCard(); //Store card from top of deck

        GameObject cardObj = Instantiate(deck.GetComponent<AJT_DeckOfCards>().cardPrefab); //Instantiate prefab as that card

        ShowCard(card, cardObj, hand.Count); //Update scene UI to display card correctly 

        hand.Add(card); //Store card in local hand list

        //check if this is an enhanced card, if it is, delay showing total
        if (card is AJT_Card) {
            AJT_Card enhancedCard = card as AJT_Card;
            //ignore the first two cards
            if (hand.Count > 2) {
                choosing = true;
                enhancedCard.TriggerEnhancedCard();
                StartCoroutine(EnhanceBuffer()); //delay show total until decision is made
            } else {
                enhancedCard.usingValue = true;
                ShowValue();
            }
        }
        //otherwise use default value and show hand total
        else
    		ShowValue(); //Update scene UI to display hand total	
	}
    //Coroutine to delay the call to add up the hand total, preventing bust or blackjack while choosing enhanced effect
    protected virtual IEnumerator EnhanceBuffer() { 
        while (choosing) {
            yield return new WaitForEndOfFrame();
        }
        ShowValue();
    }

    //Function to update scene UI for individual card instances 
    new public void ShowCard(DeckOfCards.Card card, GameObject cardObj, int pos) {
        cardObj.name = card.ToString(); //Name the gameobject in the hierarchy

        cardObj.transform.SetParent(handBase.transform); //Assign parent
                                                         //Set scene transforms
        cardObj.GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);

        //Update scene UI elements
        cardObj.GetComponentInChildren<Text>().text = deck.GetNumberString(card);
        cardObj.GetComponentsInChildren<Image>()[1].sprite = deck.GetSuitSprite(card);
    }

    //BUG FIX
    //overidden to check for natural Black Jack
    protected override void ShowValue() {
        handVals = GetHandValue();

        total.text = "Player: " + handVals;

        //check for blackjack
        if (handVals == 21) { 
            //call for natural blackjack
            if (hand.Count == 2) {
                GameObject.Find("Game Manager").GetComponent<AJT_BlackJackManager>().BlackJack();
            }
            //Automatically stay if the player has 21
            else
                GameObject.Find("Game Manager").GetComponent<AJT_BlackJackManager>().PlayerStays();
        //check for bust
        } else if (handVals > 21) {
            GameObject.Find("Game Manager").GetComponent<AJT_BlackJackManager>().PlayerBusted();          
        }
    }

    //public accessor for private base function
    public void GetValue() {
        ShowValue();
    }
}
