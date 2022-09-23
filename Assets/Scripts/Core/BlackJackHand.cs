using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class BlackJackHand : MonoBehaviour {

	public Text total; //variable to display total vlaue of the hand
	public float xOffset; //offset for card placement in a hand
	public float yOffset; //same
	public GameObject handBase; //parent gameobject for the hand cards
	public int handVals; //total hand value 

	protected DeckOfCards deck; //the deck for the round
	protected List<DeckOfCards.Card> hand; //player's hand of cards
	bool stay = false; //declares variable that determines if the player is done drawing cards

	void Start () {
		SetupHand(); //sets the hand from the start
	}

	protected virtual void SetupHand(){
		deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();//find the deck that has already been created
		hand = new List<DeckOfCards.Card>();//makes a list of cards for "hand"
		HitMe(); //gives a hand 1 card for each time HitMe() is getting called
		HitMe();
	}
	
	public void HitMe(){
		if(!stay){ //if the player has not hit stay
			DeckOfCards.Card card = deck.DrawCard(); //we're getting the next card from the deck using the Next() function of the shuffle bag

			GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject; //instantiates an actual gameobject of the card 

			ShowCard(card, cardObj, hand.Count); //creates visual representation of the card

			hand.Add(card); //adds it to the deck

			ShowValue(); //displays value of the hand
		}
	}

	//sets up visual component of the card and flips up a card from the top of the deck
	protected void ShowCard(DeckOfCards.Card card, GameObject cardObj, int pos){
		cardObj.name = card.ToString(); //names the card in the hierarchy

		cardObj.transform.SetParent(handBase.transform); //sets it under the hand parent
		// rows 49~53 define card's world position based on the anchor and its local to the parent scale
		cardObj.GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);
		cardObj.GetComponent<RectTransform>().anchoredPosition = 
			new Vector2(
				xOffset + pos * 110, 
				yOffset); 

		cardObj.GetComponentInChildren<Text>().text = deck.GetNumberString(card); //sets up its text
		cardObj.GetComponentsInChildren<Image>()[1].sprite = deck.GetSuitSprite(card); //and image
	}

	protected virtual void ShowValue(){
		handVals = GetHandValue(); //here we evaluate the hand and save it
			
		total.text = "Player: " + handVals; //displays the value of the hand

		if(handVals > 21){ //if he value is bigger than 21 the manager's lose condition is called
			GameObject.Find("Game Manager").GetComponent<BlackJackManager>().PlayerBusted();
		}
	}

	//asks manager to count up the whole value of the hand
	public int GetHandValue(){
		BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<BlackJackManager>();

		return manager.GetHandValue(hand);
	}
}
