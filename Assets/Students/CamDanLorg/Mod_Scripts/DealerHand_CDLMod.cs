using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerHand_CDLMod : OverDealerHand
{
        private bool cdlReveal;

    //Bug 3: If the dealer has higher card value than the player, the dealer still hits.  - Fixed, I hate Unity and coding. SADGE
    protected override void SetupHand()
    { //overriding parent class with it's own info
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
        Hand_CDLMod playerHand = GameObject.Find("Player Hand Value").GetComponent<Hand_CDLMod>();
        return handVal >= playerHand.handVals;

    }
    
    // --------- MOD COMMENTS ---------
    //We're overriDing showvalue completely only to implement balance update here
    protected override void ShowValue()
    { //completely overriding the parent function

        if(hand.Count > 1)
        { //if the hand has more than 1 card
            if(!cdlReveal)
            { //and it's not revealed
                handVals = hand[1].GetCardHighValue(); //Shows the value of the card revealed. 

                total.text = "Dealer: " + handVals + " + ???"; //changes text element to reflect that.
            }
            else 
            {
                handVals = GetHandValue(); //Counts all cards in the dealers hand. 

                total.text = "Dealer: " + handVals; //displays 

                //assings black jack manager. 
                Manager_CDLMod manager = GameObject.Find("Game Manager").GetComponent<Manager_CDLMod>();

                if(handVals > 21)
                { //if dealer hand value goes over 21;
                    manager.DealerBusted(); //calls dealer bust funciton from manager. 
                    manager.BalanceUpdate();
                } 
                else if(!DealStay(handVals))
                { //otherwise, if the dealer didn't stay, the dealer will call the hit me function
                    Debug.Log("CDL");
                    Invoke("HitMe", 1);
                }
                else 
                {   //otherwise 
                    //Calls the value of the player hand. 
                    Hand_CDLMod playerHand = GameObject.Find("Player Hand Value").GetComponent<Hand_CDLMod>();

                    //compares the value of the dealer hand to the player hand, who ever is larger, wins. 
                    if(handVals < playerHand.handVals){
                        manager.BalanceUpdate();
                        manager.PlayerWin();
                    } 
                    else
                    {
                        manager.PlayerLose();
                    }
                }
                
                if (handVals == 21)
                {
                    manager.BalanceUpdate();
                    manager.CDL_DealerBlackJack();
                }
            }
        }
    }

    public new void RevealCard()
    {
        cdlReveal = true;

        GameObject cardOne = transform.GetChild(0).gameObject;
        
        //change the next card sprite
        cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
		cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

		//visualizes the card. 
		ShowCard(hand[0], cardOne, 0);

		//shows the value of the card. 
		ShowValue();
    }
}
