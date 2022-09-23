using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

//BUG; no logic for natural blackjack
//BUG; with no GetCardLowValue, player can instantly bust on a deal of two aces

public class BlackJackManager : MonoBehaviour {

	public Text statusText;
	public GameObject tryAgain;
	public string loadScene;

	//called from BlackJackHand
	public void PlayerBusted(){
		HidePlayerButtons();
		GameOverText("YOU BUST", Color.red);
	}

	//called from DealerHand
	public void DealerBusted(){
		GameOverText("DEALER BUSTS!", Color.green);
	}
	
	//called from DealerHand
	public void PlayerWin(){
		GameOverText("YOU WIN!", Color.green);
	}
		
	//called from DealerHand
	public void PlayerLose(){
		GameOverText("YOU LOSE.", Color.red);
	}

	//BUG; function not called from anywhere
	public void BlackJack(){
		GameOverText("Black Jack!", Color.green);
		HidePlayerButtons();
	}

	//function to update scene UI elements on game over conditions
	public void GameOverText(string str, Color color){
		statusText.text = str;
		statusText.color = color;

		tryAgain.SetActive(true);
	}
	
	//hides scene UI elements on game over conditions
	public void HidePlayerButtons(){
		GameObject.Find("HitButton").SetActive(false);
		GameObject.Find("StayButton").SetActive(false);
	}

	//reloads scene -- BUG; deck is reset on scene load
	public void TryAgain(){
		SceneManager.LoadScene(loadScene);
	}

	//function to return the total from the cards in hand
	public virtual int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = 0;

		foreach(DeckOfCards.Card handCard in hand){
			handValue += handCard.GetCardHighValue();
		}
		return handValue;
	}
}
