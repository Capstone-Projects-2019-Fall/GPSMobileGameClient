using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private static Dictionary<int, Card> CardsById;
    private Deck deck;
    private Deck nonexhaustedDeck;
    private Deck hand;
    // Dictates how many cards the starting hand should have.
    [SerializeField] private int start_amount = 5;

    public int Start_Amount { get => start_amount; set => start_amount = value; }

    System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {

        List<Card> randCards = GenerateCardList(40);
        deck = new Deck(randCards);
        nonexhaustedDeck = new Deck(deck);

        foreach(Card c in nonexhaustedDeck.Cards)
        {
            Debug.Log(c.Name);
        }
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

    // This generates a list of random cards of the given size
    public List<Card> GenerateCardList(int size)        
    {
        List<Card> randomCards = new List<Card>();
        for(int i = 0; i < size; i++)
        {
            randomCards.Add(CardFactory.CreateRandomCard());
        }

        return randomCards;
    }
}
