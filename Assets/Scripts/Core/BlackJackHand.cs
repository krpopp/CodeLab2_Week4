using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class BlackJackHand : MonoBehaviour {

	//Ref to scene objects
	public Text total;
	public GameObject handBase;
	//Vars for scene transforms
	public float xOffset;
	public float yOffset;
	//Current value of player hand
	public int handVals;
	//Card classes
	protected DeckOfCards deck;
	protected List<DeckOfCards.Card> hand;
	//Local bool
	bool stay = false;

	void Start () {
		SetupHand();
	}

	//Scene initialization
	protected virtual void SetupHand(){
		//Set references
		deck = GameObject.Find("Deck").GetComponent<DeckOfCards>();
		hand = new List<DeckOfCards.Card>();
		//Add two cards to player hand
		HitMe();
		HitMe();
	}
	
	//Function to add card to player hand, calls all other functions in this class
	public void HitMe(){
		if(!stay){ //We haven't stayed
			DeckOfCards.Card card = deck.DrawCard(); //Store card from top of deck

			GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject; //Instantiate prefab as that card

			ShowCard(card, cardObj, hand.Count); //Update scene UI to display card correctly 

			hand.Add(card); //Store card in local hand list

			ShowValue(); //Update scene UI to display hand total
		}
	}

	//Function to update scene UI for individual card instances 
	protected void ShowCard(DeckOfCards.Card card, GameObject cardObj, int pos){
		cardObj.name = card.ToString(); //Name the gameobject in the hierarchy

		cardObj.transform.SetParent(handBase.transform); //Assign parent
		//Set scene transforms
		cardObj.GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);
		cardObj.GetComponent<RectTransform>().anchoredPosition = 
			new Vector2(
				xOffset + pos * 110, 
				yOffset);

		//Update scene UI elements
		cardObj.GetComponentInChildren<Text>().text = deck.GetNumberString(card);
		cardObj.GetComponentsInChildren<Image>()[1].sprite = deck.GetSuitSprite(card);
	}

	//Function to display player's hand value and check for bust
	protected virtual void ShowValue(){
		handVals = GetHandValue();
		//Display text
		total.text = "Player: " + handVals;
		//Trigger bust if over 21
		if(handVals > 21){
			GameObject.Find("Game Manager").GetComponent<BlackJackManager>().PlayerBusted();
		}
	}
	
	//Returns BlackJackManager function value
	public int GetHandValue(){
		BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<BlackJackManager>();

		return manager.GetHandValue(hand);
	}
}
