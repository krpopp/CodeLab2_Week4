using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AJT_SwapCard : AJT_Card
{

    
    public AJT_SwapCard(Type cardNum, Suit suit) : base(cardNum, suit)
    {       //constructor to declare a variable of type Card
        this.cardNum = cardNum;
        this.suit = suit;
    }

    public override int GetCardEnhancedValue()
    {
        return base.GetCardEnhancedValue();


    }

    public override void TriggerEnhancedCard()
    {
        base.TriggerEnhancedCard();
         //while (choosing)
        {
            
            //need valueButton and whatever else to make choosing false

        }

    }
}
