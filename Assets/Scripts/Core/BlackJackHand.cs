using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class BlackJackHand : MonoBehaviour {

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

	//BUG: no natural black jacks
	//Make the player's hand and add two cards
	protected virtual void SetupHand(){
		deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();
		hand = new List<DeckOfCards.Card>();
		HitMe();
		HitMe();
	}

	//Create a new card and add it to the player's hand
	public void HitMe(){
		if(!stay){
			DeckOfCards.Card card = deck.DrawCard();

			GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject;

			ShowCard(card, cardObj, hand.Count);

			hand.Add(card);

			ShowValue();
		}
	}

	//Arranging the card in the canvas
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

	//Display the player's score, determine if the player has busted
	protected virtual void ShowValue(){
		handVals = GetHandValue();
			
		total.text = "Player: " + handVals;

		if(handVals > 21){
			GameObject.Find("Game Manager").GetComponent<BlackJackManager>().PlayerBusted();
		}
	}

	//Find the player's score
	public int GetHandValue(){
		BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<BlackJackManager>();

		return manager.GetHandValue(hand);
	}
}
