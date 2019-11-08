using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private Deck deck;
    private Deck nonexhaustedDeck;
    private Deck hand;
    private CardManager cm;
    // Dictates how many cards the starting hand should have.
    [SerializeField] private int start_amount = 5;

    public int Start_Amount { get => start_amount; set => start_amount = value; }

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Get/generate cards somehow from server inventory
        // deck = new Deck(40, cards);
        // nonexhaustedDeck = new Deck(40, cards);
        cm = new CardManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Draws the starter hand
    public bool DrawStarterHand()
    {
        deck.ShuffleDeck();

        if(deck.Cards.Count == 0)
        {
            return false;
        } else
        {
            int i = 0;
            while(i<start_amount)
            {
                hand.AddCard(deck.DrawCard());
                i++;
            }
            return true;
        }
    }

    // Draws a card and adds it to the deck
    public bool DrawCard(int num = 1)
    {
        if (deck.Cards.Count == 0)
        {
            return false;
        }
        else
        {
            int i = 0;
            while(i < num && deck.Cards.Count != 0)
            {
                hand.AddCard(deck.DrawCard());
                i++;
            }
            return true;
        }
    }

    // This resets deck & hand after a combat instance
    public void ResetDeck()
    {
        deck = new Deck(nonexhaustedDeck);
        hand = new Deck();
    }
}
