using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JLZDealerHand : BlackJackHand
{
    public Sprite cardBack;

    bool reveal;

    protected override void SetupHand()
    {
        base.SetupHand();
    }

    protected override void ShowValue()
    {

        if (hand.Count > 1)
        {
            if (!reveal)
            {
                handVals = hand[1].GetCardHighValue();

                total.text = "Dealer: " + handVals + " + ???";
            }
            else
            {
                handVals = GetHandValue();

                total.text = "Dealer: " + handVals;

                JLZBlackJackManager manager = GameObject.Find("Game Manager").GetComponent<JLZBlackJackManager>();

                if (handVals > 21)
                {
                    manager.DealerBusted();
                }
                else if (!DealStay(handVals))
                {
                    Invoke("HitMe", 1);
                }
                else
                {
                    BlackJackHand playerHand = GameObject.Find("Player Hand Value").GetComponent<BlackJackHand>();

                    if (handVals < playerHand.handVals)
                    {
                        manager.PlayerWin();
                    }
                    else
                    {
                        manager.PlayerLose();
                    }
                }
            }
        }
    }

    protected virtual bool DealStay(int handVal)
    {
        return handVal > 17;
    }

    public void RevealCard()
    {
        reveal = true;

        GameObject cardOne = transform.GetChild(0).gameObject;

        cardOne.GetComponentsInChildren<Image>()[0].sprite = null;
        cardOne.GetComponentsInChildren<Image>()[1].enabled = true;

        ShowCard(hand[0], cardOne, 0);

        ShowValue();
    }

}
