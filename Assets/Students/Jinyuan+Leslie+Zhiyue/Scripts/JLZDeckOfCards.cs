using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class JLZDeckOfCards : DeckOfCards
{

    public Text cardsLeft;

    // Mod: show how many cards in the deck are left 
    private void Update()
    {
        if(deck != null) cardsLeft.text = "Cards in the Deck: " + deck.Count;

    }

    // Bug Fixed: Use 4 decks of cards instead of 1 deck
    // Mod: still keep using 1 deck of cards
    protected override void AddCardsToDeck()
    {
        base.AddCardsToDeck();
    }

    // BUG FIX: reshuffle deck when it has less than 20 cards
    // Mod: reshuffle deck when it is empty
    protected override bool IsValidDeck()
    {
        return deck != null && deck.Count > 0;
    }


    // Bug Fixed: Remove a card each time drawing a card
    public override Card DrawCard()
    {
        //Debug.Log("Cards in Deck: " + deck.Count);
        Card nextCard = deck.Next();

        deck.Remove(nextCard);

        return nextCard;
    }
}
