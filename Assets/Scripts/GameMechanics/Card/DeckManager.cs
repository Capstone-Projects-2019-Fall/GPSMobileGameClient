using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Assertions;

/* DeckManager Description:
 * The DeckManager is a unity singleton that exposes the data and functionality of every Deck object to the Unity scene. This is important
 * because Decks themselves are not MonoBehaviours, and mostly are wrappers for lists of cards.
 * The DeckManager is referenced by the CombatController, and provides a useful interface for managing drawing, insertion, or deletion of cards
 * any given deck
 */
[RequireComponent(typeof(CombatController))]
public class DeckManager : Singleton<DeckManager>
{
    private CombatController _cc;

    private Deck _deck;  // The deck currently being used in combat
    private Deck _nonexhaustedDeck; // An 'image' of the deck that players left their home base with
    private Deck _hand; // All the cards currently in the player's hand
    [SerializeField] private int _startAmount = 5; // Dictates how many cards the starting hand should have.

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
        get => _startAmount;
        set => _startAmount = value;
    }

    #endregion ------------------------------------------------------------------------------------------

    private void Awake()
    {
        // Grab reference to the CombatController and subscribe to related events
        _cc = gameObject.GetComponent<CombatController>();
        Assert.IsNotNull(_cc);
        _cc.CardsDrawn += OnCardDrawnAction;
    }

    // Start is called before the first frame update
    // Currently generating a random deck for testing purposes
    void Start()
    {
        List<Card> randCards = GenerateRandomCardList(40);
        _hand = new Deck();
        _deck = new Deck(randCards);
        _nonexhaustedDeck = new Deck(_deck);
        _deck.ShuffleDeck();

    }

    // Draws a card and adds it to the deck
    // TODO: Add implementation for discard pile and reshuffling
    public bool DrawCard()
    {
        if (_deck.Cards.Count == 0)
        {
            return false;
        } else
        {
            _hand.AddCard(_deck.DrawCard());
            return true;
        }
    }

    // Event handler for the DrawnCards event
    public void OnCardDrawnAction(object sender, DrawEventArgs e)
    {
        for(int i = 0; i < e.NumCards; i++)
        {
            DrawCard();
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
