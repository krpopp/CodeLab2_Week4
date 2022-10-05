using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JLZDealerHand : DealerHand
{
    // BUG FIX: dealer stay when value is greater than or equal to 17
    protected override bool DealStay(int handVal)
    {
        return handVal >= 17;
    }

}
