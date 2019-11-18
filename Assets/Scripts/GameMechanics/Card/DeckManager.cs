using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using UnityEngine;

/* DeckManager Description:
 * The DeckManager is a unity singleton that exposes the data and functionality of every Deck object to the Unity scene. This is important
 * because Decks themselves are not MonoBehaviours, and mostly are wrappers for lists of cards.
 * The DeckManager is referenced by the CombatController, and provides a useful interface for managing drawing, insertion, or deletion of cards
 * any given deck
 */
public class DeckManager : Singleton<DeckManager>
{
    private static Dictionary<int, Card> CardsById;
    private Deck _deck;
    private Deck _nonexhaustedDeck;
    private Deck _hand;
    // Dictates how many cards the starting hand should have.
    [SerializeField] private int start_amount = 5;

    #region Accessors ----------------------------------------------------------------------------------

    public Deck Deck {
        get => _deck;
    }
    public Deck NonexhaustedDeck {
        get => _nonexhaustedDeck;
    }
    public Deck Hand {
        get => _hand;
    }
    public int Start_Amount {
        get => start_amount;
        set => start_amount = value;
    }

    #endregion ------------------------------------------------------------------------------------------

    System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        List<Card> randCards = GenerateRandomCardList(40);
        _hand = new Deck();
        _deck = new Deck(randCards);
        _nonexhaustedDeck = new Deck(_deck);
    }

    // Draws the starter hand
    public bool DrawStarterHand()
    {
        _deck.ShuffleDeck();

        if(_deck.Cards.Count == 0)
        {
            return false;
        } else
        {
            int i = 0;
            while(i<start_amount)
            {
                _hand.AddCard(_deck.DrawCard());
                i++;
            }
            return true;
        }
    }

    // Draws a card and adds it to the deck
    public bool DrawCard(int num = 1)
    {
        Debug.Log(_hand == null);
        if (_deck.Cards.Count == 0)
        {
            return false;
        }
        else
        {
            int i = 0;
            while(i < num && _deck.Cards.Count != 0)
            {
                _hand.AddCard(_deck.DrawCard());
                i++;
            }
            return true;
        }
    }

    // This resets deck & hand after a combat instance
    public void ResetDeck()
    {
        _deck = new Deck(_nonexhaustedDeck);
        _hand = new Deck();
    }

    /* This generates a list of random cards of the given size
     * Ultimately only useful for testing purposes
     */
    public List<Card> GenerateRandomCardList(int size)        
    {
        List<Card> randomCards = new List<Card>();
        for(int i = 0; i < size; i++)
        {
            randomCards.Add(CardFactory.CreateRandomCard());
        }

        return randomCards;
    }
}
