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
        var cards = Assembly.GetAssembly(typeof(Card)).GetTypes()
           .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Card)));


        CardsById = new Dictionary<int, Card>();

        foreach (var card in cards)
        {
            Card instantiatedCard = Activator.CreateInstance(card) as Card;
            CardsById.Add(instantiatedCard.Id, instantiatedCard);
        }
        // TODO: Get/generate cards somehow from cardFactory
        
        deck = new Deck(40, generateDeck(40));
        nonexhaustedDeck = new Deck(deck);
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

    // This randomly generates a deck
    public List<Card> generateDeck(int size)        
    {
        List<Card> cards = new List<Card>();
        List<Card> prefabCards = Enumerable.ToList(CardsById.Values);
        for(int i = 0; i < size; i++)
        {
            cards.Add(prefabCards[rand.Next(5)]);
        }

        return cards;
    }
}
