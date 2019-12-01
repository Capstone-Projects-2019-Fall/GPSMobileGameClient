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
[RequireComponent(typeof(UIController))]
public class DeckManager : Singleton<DeckManager>
{
    private CombatController _cc;
    private UIController _uic;

    private Deck _deck;  // The deck currently being used in combat
    private Deck _nonexhaustedDeck; // An 'image' of the deck that players left their home base with
    private Deck _hand; // All the cards currently in the player's hand
    private Deck _discard; // The player's discard pile; cards from their hand are sent here at the end of each turn

    [SerializeField] private int _maxHandSize = 7; // Dictates the maximum hand size

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
    public Deck Discard {
        get => _discard;
    }
    public int MaxHandSize {
        get => _maxHandSize;
    }

    #endregion ------------------------------------------------------------------------------------------

    private void Awake()
    {
        // Grab reference to the CombatController and subscribe to related events
        _cc = gameObject.GetComponent<CombatController>();
        Assert.IsNotNull(_cc);
        _cc.CardsDrawn += OnCardDrawnAction; // event handler for card drawn
        _cc.CardPlayed += OnCardPlayedAction; // event handler for card played (removes card from hand)
        _cc.CardDiscarded += OnCardDiscardedAction; // event handler for card discarded (moves card to discard pile)

        _uic = gameObject.GetComponent<UIController>();
        Assert.IsNotNull(_uic);

        // Generate random deck for testing
        List<Card> randCards = GenerateRandomCardList(40);
        _hand = new Deck();
        _deck = new Deck(randCards);
        _nonexhaustedDeck = new Deck(_deck);
        _deck.ShuffleDeck();

    }

    // Start is called before the first frame update
    // Currently generating a random deck for testing purposes
    void Start()
    {

    }

    // Draws a card and adds it to the deck
    public bool DrawCard()
    {
        if (_deck.Cards.Count == 0) // deck is empty
        {
            Debug.Log("Deck is out of cards!");
            return false;
        } else if(_hand.CurrentLength >= _maxHandSize) // hand is full
        {
            Debug.Log("Hand is full, draw was skipped!");
            return false;
        } else // draw the card
        {
            Card drawnCard = _deck.DrawCard();
            _hand.AddCard(drawnCard);
            GameObject cardGO = CardFactory.CreateCardGameObject(drawnCard);
            
            cardGO.transform.SetParent(_uic.HandZone);
            cardGO.transform.localScale = new Vector3(1, 1, 1);

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

    #region Event Handlers ------------------------------------------------------------------

    // Event handler for the CardsDrawn event
    public void OnCardDrawnAction(object sender, DrawEventArgs e)
    {
        for (int i = 0; i < e.NumCards; i++)
        {
            DrawCard();
        }
    }

    // Event hanlder for the CardPlayed event
    public void OnCardPlayedAction(object sender, CardPlayedArgs e)
    {
        Hand.RemoveCard(e.Card);
    }

    public void OnCardDiscardedAction(object sender, CardDiscardedArgs e)
    {
        Hand.RemoveCard(e.Card);

    }

    #endregion ------------------------------------------------------------------------------
}


