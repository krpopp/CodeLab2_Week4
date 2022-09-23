using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour {

	//bugs
	//1. no black jack
	//2. no natural black jack
	//3. dealer doesn't hit if they are less than 21 and less than the player
	//4. Ace only uses the 11 value
	//5. script isn't using 4 decks of cards that are used until only 20 are left
	//6. dealer hits even when they have won but are under 18

	public Text statusText;//UI text
	public GameObject tryAgain;
	public string loadScene;//for loading scene

	//hide buttons and show text
	public void PlayerBusted(){
		HidePlayerButtons();
		GameOverText("YOU BUST", Color.red);
	}
	
	//show text
	public void DealerBusted(){
		GameOverText("DEALER BUSTS!", Color.green);
	}
		
	//show text
	public void PlayerWin(){
		GameOverText("YOU WIN!", Color.green);
	}
		
	//show text
	public void PlayerLose(){
		GameOverText("YOU LOSE.", Color.red);
	}

	//show text and hide buttons
	public void BlackJack(){
		GameOverText("Black Jack!", Color.green);
		HidePlayerButtons();
	}

	//set the text and the color, set Try Again to true
	public void GameOverText(string str, Color color){
		statusText.text = str;
		statusText.color = color;

		tryAgain.SetActive(true);
	}

	//hide hit and stay
	public void HidePlayerButtons(){
		GameObject.Find("HitButton").SetActive(false);
		GameObject.Find("StayButton").SetActive(false);
	}

	//try again button to load the scene
	public void TryAgain(){
		SceneManager.LoadScene(loadScene);
	}

	//?? why is this here?
	public virtual int GetHandValue(List<DeckOfCards.Card> hand){
		int handValue = 0;

		foreach(DeckOfCards.Card handCard in hand){
			handValue += handCard.GetCardHighValue();
		}
		return handValue;
	}
}
