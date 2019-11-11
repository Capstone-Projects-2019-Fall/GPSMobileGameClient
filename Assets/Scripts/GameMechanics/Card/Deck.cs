using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] private int max_size = 40;
    private List<Card> cards;
    private static System.Random rng = new System.Random();

    public int Max_Size { get => max_size; set => max_size = value; }
    public List<Card> Cards { get => cards; }

    // Constructor to reset a deck
    public Deck()
    {
        cards = new List<Card>();
    }

    // Constructor to set max size of deck
    public Deck(int max_size)
    {
        Max_Size = max_size;
        cards = new List<Card>();
    }

    // Constructor to set max size and a given set of cards.
    public Deck(int max_size, List<Card> cards)
    {
        Max_Size = max_size;
        cards = new List<Card>(cards);
    }

    // Constructor to set a given set of cards.
    public Deck(List<Card> cards)
    {
        cards = new List<Card>(cards);
    }

    // Constructor to copy decks
    public Deck(Deck deck)
    {
        Max_Size = deck.Max_Size;
        cards = new List<Card>(deck.Cards);
    }

    // Start is called before the first frame update
    void Start()
    {
        cards = new List<Card>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Adds card to list
    public void AddCard(Card card)
    {
        if(cards.Count > max_size)
        {
            return;
        } else
        {
            cards.Add(card);
        }
    }

    // Returns bool if card was removed or doesn't exist
    public bool RemoveCard(Card card)
    {
        return cards.Remove(card);
    }

    // Returns a card and removes it from the deck
    public Card DrawCard()
    {
        Card card = cards[0];
        RemoveCard(card);
        return card;
    }
    
    // Shuffles cards in deck
    public void ShuffleDeck()
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

    // Copies card list over
    public void CopyDeck(List<Card> cardList)
    {
        cards = new List<Card>(cardList);
    }

    // Returns a string of all cards.
    public string DisplayDeck()
    {
        string s = "";
        foreach (Card card in cards)
        {
            s += "Name: " + card.Name;
            s += "Level: " + card.Level;
            s += "Memory Cost: " + card.MemoryCost;
            //s += "Pp Left: " + card.PP;
            s += "\n";
        }
        return s;
    }
}
