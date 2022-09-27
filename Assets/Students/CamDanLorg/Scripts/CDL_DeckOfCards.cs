using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDL_DeckOfCards : DeckOfCards
{

    //Bug 1: Ace only has one value. (should 1 or 11) - fixed. 
    //Bug 2: No BlackJack End state. - FIXED
    //Bug 3: If the dealer has higher card value than the player, the dealer still hits. - Fixed SADGE
    //Bug 4: When Dealer Busts, Hit me button does not hide. - FIXED
    //Bug 5: No BlackJack when player or dealer hits 21. - FIXED
    //Bug 6: Black jack win/lose state
}
