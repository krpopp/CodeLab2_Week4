using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDL_DealerHand : DealerHand
{
    private bool cdlReveal;

    //Bug 3: If the dealer has higher card value than the player, the dealer still hits.  - Fixed, I hate Unity and coding. SADGE
    protected override void SetupHand(){ //overriding parent class with it's own info
        base.SetupHand(); //at first perform everything form the parent class

        GameObject cardOne = transform.GetChild(0).gameObject; //get the first child in hierarchy as it's the first card in the hand
        //lines from 16~18 visualize the card, flipping it and showing a specific image based on whether or not it's revealed
        cardOne.GetComponentInChildren<Text>().text = ""; //empty the text to not show anything
        cardOne.GetComponentsInChildren<Image>()[0].sprite = cardBack;
        cardOne.GetComponentsInChildren<Image>()[1].enabled = false;

        cdlReveal = false;
    }
    
    protected override bool DealStay(int handVal)
    { 
        CDL_BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<CDL_BlackJackHand>();
        return handVal >= playerHand.handVals;

    }
    
    protected override void ShowValue(){ //completely overriding the parent function

        if(hand.Count > 1){ //if the hand has more than 1 card
            if(!cdlReveal){ //and it's not revealed
                handVals = hand[1].GetCardHighValue(); //Shows the value of the card revealed. 

                total.text = "Dealer: " + handVals + " + ???"; //changes text element to reflect that.
            } else {
                handVals = GetHandValue(); //Counts all cards in the dealers hand. 

                total.text = "Dealer: " + handVals; //displays 

                //assings black jack manager. 
                CDL_BlackJackManager manager = GameObject.Find("Game Manager").GetComponent<CDL_BlackJackManager>();

                if(handVals > 21){ //if dealer hand value goes over 21;
                    manager.DealerBusted(); //calls dealer bust funciton from manager. 
                } else if(!DealStay(handVals)){ //otherwise, if the dealer didn't stay, the dealer will call the hit me function
                    Debug.Log("CDL");
                    Invoke("HitMe", 1);
                } else { //otherwise 
                    //Calls the value of the player hand. 
                    CDL_BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<CDL_BlackJackHand>();

                    //compares the value of the dealer hand to the player hand, who ever is larger, wins. 
                    if(handVals < playerHand.handVals){
                        /*Debug.Log(handVals);
                        Debug.Log(playerHand.handVals);*/
                        manager.PlayerWin();
                    } else {
                        /*Debug.Log(handVals);
                        Debug.Log(playerHand.handVals);*/
                        manager.PlayerLose();
                    }
                }
                
                if (handVals == 21)
                {
                    GameObject.Find("Game Manager").GetComponent<CDL_BlackJackManager>().BlackJack();
                }
            }
        }
    }

    public void CDL_RevealCard()
    {
        cdlReveal = true;

        GameObject cardOne = transform.GetChild(0).gameObject;
        
        cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		//visualizes the card. 
		ShowCard(hand[0], cardOne, 0);

		//shows the value of the card. 
		ShowValue();
    }
}
