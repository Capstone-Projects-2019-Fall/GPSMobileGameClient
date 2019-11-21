using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private int _maxSize = 40;
    private List<Card> _cards;

    public int MaxSize { get => _maxSize; set => _maxSize = value; }
    public List<Card> Cards { get => _cards; }

    #region Deck constructors ---------------------------------------------------------------------------

    // Constructor to reset a deck
    public Deck()
    {
        _cards = new List<Card>();
    }

    // Constructor to set max size of deck
    public Deck(int max_size)
    {
        MaxSize = max_size;
        _cards = new List<Card>();
    }

    // Constructor to set max size and a given set of cards.
    public Deck(int max_size, List<Card> cards)
    {
        MaxSize = max_size;
        _cards = new List<Card>(cards);
    }

    // Constructor to set a given set of cards.
    public Deck(List<Card> cards)
    {
        _cards = cards.ConvertAll(card => CardFactory.CreateCard(card.Id));
    }

    // Constructor to copy decks
    public Deck(Deck deck)
    {
        MaxSize = deck.MaxSize;
        _cards = deck.Cards.ConvertAll(card => CardFactory.CreateCard(card.Id));
    }

    #endregion ------------------------------------------------------------------------------------------

    // Adds card to list
    public void AddCard(Card card)
    {
        if(_cards.Count > _maxSize)
        {
            return;
        } else
        {
            _cards.Add(card);
        }
    }

    // Returns bool if card was removed or doesn't exist
    public bool RemoveCard(Card card)
    {
        return _cards.Remove(card);
    }

    // Returns a card and removes it from the deck
    public Card DrawCard()
    {
        Card card = _cards[0];
        RemoveCard(card);
        return card;
    }
    
    // Shuffles cards in deck
    public void ShuffleDeck()
    {
        int n = _cards.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Card value = _cards[k];
            _cards[k] = _cards[n];
            _cards[n] = value;
        }
    }

    // Copies card list over
    public void CopyDeck(List<Card> cardList)
    {
        _cards = new List<Card>(cardList);
    }

    // Returns a string of all cards.
    public string DisplayDeck()
    {
        string s = "";
        foreach (Card card in _cards)
        {
            s += "Name: " + card.Name;
            s += "Level: " + card.Level;
            s += "Memory Cost: " + card.MemoryCost;
            s += "\n";
        }
        return s;
    }
}
