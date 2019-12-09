using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/* CardInventoryZone Description:
 * A CardInventoryZone is a flexible UI element used for displaying a graphical list of Cards. Similar to the CardScrollList, the main use cases
 * for a CardInventoryZone are the crafting and deckbuilding/homebase scenes, although theoretically the prefab should be encapsulated properly and
 * flexible enough to add to other scenes in the future. */
public class CardInventoryZone : MonoBehaviour
{
    // Data fields
    [SerializeField] private Deck _myCards;
    [SerializeField] private GameObject _cardImagePF;

    // UI-related fields
    [SerializeField] private List<GameObject> _cardImageList;
    [SerializeField] private GameObject _cardViewport;
    [SerializeField] private GameObject _cardListContent;
    [SerializeField] private Transform _contentTrans;
    [SerializeField] private GameObject _myScrollbar;

    private void Awake()
    {
        CardFactory.InitializeFactory(); // Ensure the CardFactory is initialized

        // Load data fields
        _cardImagePF = Resources.Load<GameObject>("Prefabs/UI/CardImage");

        // UI references
        _cardImageList = new List<GameObject>();
        _cardViewport = gameObject.transform.Find("CardViewport").gameObject;
        _cardListContent = _cardViewport.transform.Find("CardListContent").gameObject; // Content MUST be childed under the card viewport to function properly
        _contentTrans = _cardListContent.transform;
        _myScrollbar = gameObject.transform.Find("CardListScrollbar").gameObject;

        // Validate references obtained successfully
        Assert.IsNotNull(_cardImagePF);

        Assert.IsNotNull(_cardImageList);
        Assert.IsNotNull(_cardViewport);
        Assert.IsNotNull(_cardListContent);
        Assert.IsNotNull(_contentTrans);
        Assert.IsNotNull(_myScrollbar);

        // Initialize GUI
        GenerateTestDeck();
        InitializeListContent();
    }


    /* Creates an instance of a Card image from its prefab to be inserted into the CardInventoryZone. This method
     * is called from AddCardToList (which does the additional responsibilities of maintaining the relevant data
     * structures within the CardInventoryZone. This only creates the necessary GameObject. */
    private GameObject CreateCardImage(Card card)
    {
        return null; 
    }

    /* Initialize the CardScrollList's content using _myCards
     * NOTE: The CardScrollList will then populate itself with card images corresponding to its _myCards field
     * NOTE: Cannot call AddCardToList, as this will throw an InvalidOperationException (modifying collection while enumerating over collection)
     */
    private void InitializeListContent()
    {
        foreach (Card c in _myCards.Cards)
        {
            GameObject cardImageGO = CreateCardImage(c);
            _cardImageList.Add(cardImageGO);

            cardImageGO.GetComponent<CardImage>().CardInventoryZone = this;
            cardImageGO.transform.SetParent(_contentTrans);
        }
    }

    /* Generate a starter deck for testing purposes and update the _myCards data field
     */
    private void GenerateTestDeck()
    {
        List<Card> deckList = new List<Card>();
        for (int i = 0; i < 12; i++) { deckList.Add(CardFactory.CreateCard(0)); } // Strike
        for (int i = 0; i < 10; i++) { deckList.Add(CardFactory.CreateCard(1)); } // Heal
        for (int i = 0; i < 10; i++) { deckList.Add(CardFactory.CreateCard(2)); } // Draw Cards
        for (int i = 0; i < 4; i++) { deckList.Add(CardFactory.CreateCard(3)); } // Increase Attack
        for (int i = 0; i < 4; i++) { deckList.Add(CardFactory.CreateCard(4)); } // Decrease Defense

        _myCards = new Deck(deckList);
    }
}
