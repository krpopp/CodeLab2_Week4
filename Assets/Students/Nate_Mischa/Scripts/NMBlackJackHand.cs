using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NMBlackJackHand : BlackJackHand
{
    public NMBlackJackHand hitMe;
    public NMDealerHand dealerHand;

    public Button SwapButton;

    bool playerBust = false;
    bool blackJack = false;

    bool hold = false;
    bool swapSelect = false;

    protected override void ShowValue()
    {
        handVals = GetHandValue(); //get player hand value
			
        total.text = "Player: " + handVals; //player hand value text display

        if(handVals > 21) //if player busts, hand exceeds 21
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().PlayerBusted();
            //Bug1: called when player bust(dealer show hands value)
            dealerHand.RevealCardWhenPlayerBust();
            playerBust = true;
        }
        //Bug2&6: call blackjack when player hit 21 or natural Blackjack
        else if (handVals == 21)
        {
            GameObject.Find("Game Manager").GetComponent<BlackJackManager>().BlackJack(); //call GM
            blackJack = true;
        }
    }
    
    public void ReSetUpHand()
    {
        for (int i = 0; i < hand.Count; i++) //check all card obj in hand
        {
            Destroy(handBase.transform.GetChild(i).gameObject); //destroy cards in hand
        }
        hand.Clear(); //clear hand
        SetupHand(); //reset hand
    }

    public void HitMe()
    {
        if (!hold)
        {
            DeckOfCards.Card card = deck.DrawCard(); //draw new card

            GameObject cardObj = Instantiate(Resources.Load("prefab/Card")) as GameObject; //instantiate visuals

            ShowCard(card, cardObj, hand.Count); //reveal card

            hand.Add(card); //add to hand

            ShowValue(); //update value

            SwapButton.interactable = true; //make swap button interactable

            if (playerBust == true || blackJack == true) //if bust or blackjack
            {
                SwapButton.interactable = false; //set to false
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && swapSelect == true) //if left mouse is clicked && swapSelect is true
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current); //trigger raycasting
            eventData.position = Input.mousePosition; //get input
            List<RaycastResult> raycastResults = new List<RaycastResult>(); //generate list of raycasted objects
            EventSystem.current.RaycastAll(eventData, raycastResults); //update list with raycast
            foreach (var result in raycastResults) //debug results, check if objects have been clicked
            {
                Debug.Log(gameObject.name);
                Debug.Log(result.gameObject.name);
            }
        }
    }

    public void SwapMe()
    {

        swapSelect = true;
    }
}
