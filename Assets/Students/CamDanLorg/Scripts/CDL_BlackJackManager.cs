using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDL_BlackJackManager : BlackJackManager
{
    //Bug 4: When Dealer Busts, Hit me button does not hide.
    //Bug 4: HidePlayerButton not being called in Dealerbust, PlayerWin, PlayerLose.

    /*public new void DealerBusted()
    {
        HidePlayerButtons();
        GameOverText("DEALER BUSTS!", Color.green); //displays game over tex.t
    }*/
}
