using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NMBlackJackHand : BlackJackHand
{
    public NMDealerHand dealerHand;

    public Button SwapButton;

    bool playerBust = false;
    bool blackJack = false;

    bool hold = false;
    bool swapSelect = false;

    public GameObject SelectedCard;
    public bool playersCard;

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
            foreach (var result in raycastResults)
            {
                //split the name of the raycasted object by ' ' and store them into a string array
                string[] splitArray = result.gameObject.name.Split(char.Parse(" "));
                if (splitArray[0] == "The")//check the first element of the string array equal to "The"
                {
                    //save the first selected card and its ownership 
                    if (SelectedCard == null)
                    {
                        SelectedCard = result.gameObject; //store the card selected into the SelectedCard
                        Debug.Log(result.gameObject.name); //debug the name of the card we click

                        //check if selected card belongs to player
                        if (SelectedCard.transform.parent == gameObject.transform)
                        {
                            playersCard = true;
                            Debug.Log("first select player's card");
                        }
                        else
                        {
                            playersCard = false;
                            Debug.Log("first select dealer's card");
                        }
                    }
                    //Legal: only swap when the combination of selected cards are 1 player and 1 dealer, otherwise the swap is 'illegal'
                    //check if the swap is legal
                    else
                    {
                        //check if the first selected card belongs to player
                        if (playersCard == true)
                        {
                            //if the second selected card belongs to dealer, make the swap
                            if (result.gameObject.transform.parent == dealerHand.transform)
                            {
                                Debug.Log("Legal Swap, deactivate swap button");
                                SwapCards(SelectedCard, result.gameObject);
                                //clear the SelectedCard automatically
                                SelectedCard = null;
                                //deactivate the swap button
                                SwapButton.interactable = false;
                                //set the button color to white
                                SwapButton.image.color = Color.white;
                            }
                            else
                            {
                                //if the second selected card also belongs to player, exit swap mode
                                Debug.Log("Illegal Swap, exit swap mode");
                                //clear the SelectedCard
                                SelectedCard = null;
                                //exit swap mode
                                swapSelect = false;
                                //set the button color to white
                                SwapButton.image.color = Color.white;
                            }
                        }
                        //if the first selected card belongs to dealer
                        else
                        {
                            //if the second selected card belongs to player, make the swap
                            if (result.gameObject.transform.parent == gameObject.transform)
                            {
                                Debug.Log("Legal Swap, deactivate swap button");
                                SwapCards(result.gameObject,SelectedCard);
                                SelectedCard = null;
                                SwapButton.interactable = false;
                                SwapButton.image.color = Color.white;
                            }
                            else
                            {
                                //if the second selected card also belongs to dealer, exit swap mode
                                Debug.Log("Illegal Swap, exit swap mode");
                                SelectedCard = null;
                                swapSelect = false;
                                SwapButton.image.color = Color.white;
                            }
                        }
                    }
                }
            }//debug results, check if objects have been clicked
        }
    }

    //called when swap button is clicked
    public void SwapMe()
    {
        //if we are in the swap mode, click to exit
        if (swapSelect)
        {
            swapSelect = false;
            SwapButton.image.color = Color.white;
            //clear the first selected card when manually exit
            SelectedCard = null;
        }
        //if we are not in the swap mode, click to enter
        else
        {
            swapSelect = true;
            SwapButton.image.color = Color.green;
        }

    }

    // called when we make a legal swap
    public void SwapCards(GameObject playersCard, GameObject dealersCard)
    {
        //assign variables
        int originalPlayerIndex;
        int originalDealerIndex;
        Vector3 tempPosition = Vector3.zero;
        Transform tempTransform = new RectTransform();
        DeckOfCards.Card tempCard = new DeckOfCards.Card(DeckOfCards.Card.Type.A,DeckOfCards.Card.Suit.CLUBS);
        
        //if dealer's card is the first one which is also the face down one, then reveal it
        if (dealersCard.GetComponent<RectTransform>().GetSiblingIndex() == 0)
        {
            //show face down card but do not enter dealer stay
            dealerHand.RevealCardWhenSwap();
            //update the text without the '???'
            dealerHand.isSwapped = true;
        }

        //store the original child sorting index for both cards
        originalPlayerIndex = playersCard.GetComponent<RectTransform>().GetSiblingIndex();
        originalDealerIndex = dealersCard.GetComponent<RectTransform>().GetSiblingIndex();

        //swap hand value
        tempCard = hand[playersCard.GetComponent<RectTransform>().GetSiblingIndex()];
        hand[playersCard.GetComponent<RectTransform>().GetSiblingIndex()] =
            dealerHand.GetDealerHandVale(dealersCard.GetComponent<RectTransform>().GetSiblingIndex());
        dealerHand.SetDealerHandValue(dealersCard.GetComponent<RectTransform>().GetSiblingIndex(),tempCard);
        ShowValue();
        
        //swap playersCard and dealersCard rect position
        tempPosition = playersCard.GetComponent<RectTransform>().position;
        playersCard.GetComponent<RectTransform>().position = dealersCard.GetComponent<RectTransform>().position;
        dealersCard.GetComponent<RectTransform>().position = tempPosition;
        
        //swap playersCard and dealersCard parent
        tempTransform = playersCard.GetComponent<RectTransform>().parent;
        playersCard.GetComponent<RectTransform>().parent = dealersCard.GetComponent<RectTransform>().parent;
        dealersCard.GetComponent<RectTransform>().parent = tempTransform;
        
        //swap playersCard and dealersCard child index
        playersCard.GetComponent<RectTransform>().SetSiblingIndex(originalDealerIndex);
        dealersCard.GetComponent<RectTransform>().SetSiblingIndex(originalPlayerIndex);
    }
}
