using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class BlackJackHand : MonoBehaviour {

	public Text total;//total of hand
	public float xOffset;//UI offset
	public float yOffset;
	public GameObject handBase;//game object
	public int handVals;//numbers held in hand

	protected DeckOfCards deck;//from DeckOfCards script
	protected List<DeckOfCards.Card> hand;//hand from the DeckOfCards
	bool stay = false;//hit or stay with hand

	void Start () {
		SetupHand();
	}

	//set up initail hand, get hand from list of DeckOfCards,
	//Hit twice for starting with two cards
	protected virtual void SetupHand(){
		deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();
		hand = new List<DeckOfCards.Card>();
		HitMe();
		HitMe();
	}
	
	//if not staying, draw card from list, insantiate card from prefabs,
	//show card to player, add card to hand, display the value
	public void HitMe(){
		if(!stay){
			DeckOfCards.Card card = deck.DrawCard();

			GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject;

			ShowCard(card, cardObj, hand.Count);

			hand.Add(card);

			ShowValue();
		}
	}

	//show card name and value, from DeckOfCards script function,
	//put the card into position for the player hand
	protected void ShowCard(DeckOfCards.Card card, GameObject cardObj, int pos){
		cardObj.name = card.ToString();

		cardObj.transform.SetParent(handBase.transform);
		cardObj.GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);
		cardObj.GetComponent<RectTransform>().anchoredPosition = 
			new Vector2(
				xOffset + pos * 110, 
				yOffset);

		//?? not sure why [1]
		cardObj.GetComponentInChildren<Text>().text = deck.GetNumberString(card);
		cardObj.GetComponentsInChildren<Image>()[1].sprite = deck.GetSuitSprite(card);
	}

	//show value of player hand, if over 21, player has busted
	protected virtual void ShowValue(){
		handVals = GetHandValue();
			
		total.text = "Player: " + handVals;

		if(handVals > 21){
			GameObject.Find("Game Manager").GetComponent<BlackJackManager>().PlayerBusted();
		}
	}

	public int GetHandValue(){
		BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<BlackJackManager>();

		return manager.GetHandValue(hand);
	}
}
