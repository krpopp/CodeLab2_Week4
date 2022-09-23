using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class JLZBlackJackHand : BlackJackHand {

	public Text total;
	public float xOffset;
	public float yOffset;
	public GameObject handBase;
	public int handVals;

	protected DeckOfCards deck;
	protected List<DeckOfCards.Card> hand;
	bool stay = false;

	void Start () {
		SetupHand();
	}

	protected virtual void SetupHand(){
		// get DeckOfCards
		deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();
		// create a list of cards from DeckOfCards
		hand = new List<DeckOfCards.Card>();
		HitMe();
		HitMe();
	}
	
	public void HitMe(){
		if(!stay){
			// draw card
			DeckOfCards.Card card = deck.DrawCard();

			// create a game object with a sprite for the drawn card above
			GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject;

			ShowCard(card, cardObj, hand.Count);

			hand.Add(card);

			ShowValue();
		}
	}

	// show the card's name and image in the input position
	protected void ShowCard(DeckOfCards.Card card, GameObject cardObj, int pos){
		cardObj.name = card.ToString();

		cardObj.transform.SetParent(handBase.transform);
		cardObj.GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);
		cardObj.GetComponent<RectTransform>().anchoredPosition = 
			new Vector2(
				xOffset + pos * 110, 
				yOffset);

		cardObj.GetComponentInChildren<Text>().text = deck.GetNumberString(card);
		cardObj.GetComponentsInChildren<Image>()[1].sprite = deck.GetSuitSprite(card);
	}

	//display the value of the card
	protected virtual void ShowValue(){
		handVals = GetHandValue();
			
		total.text = "Player: " + handVals;

		// if the sum value of cards are above 21, the player busts
		if(handVals > 21){
			GameObject.Find("Game Manager").GetComponent<BlackJackManager>().PlayerBusted();
		}
	}
	// get the value of cards on hand
	public int GetHandValue(){
		BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<BlackJackManager>();

		return manager.GetHandValue(hand);
	}
}
