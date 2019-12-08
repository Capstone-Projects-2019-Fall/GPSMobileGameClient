using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/* CardScrollList Description:
 * A CardScrollList is a modular UI component that can display a list of cards using specialized card banner prefabs. These card banners are draggable, allowing the
 * player to drag them from the CardScrollList viewport to other parts of the user interface. The main use case for the CardScrollList is the Home Base / Deckbuilding
 * interface (players can drag cards between their deck and their collection), but in principle a CardScrollList can be added to other relevant scenes.
 */
public class CardScrollList : MonoBehaviour
{
    // Data fields
    [SerializeField] private Deck _myCards;
    [SerializeField] private GameObject _cardBannerPF;

    // UI-related fields
    [SerializeField] private List<GameObject> _cardBannerList;
    [SerializeField] private GameObject _cardViewport;
    [SerializeField] private GameObject _cardListContent;
    [SerializeField] private Transform _contentTrans;
    [SerializeField] private GameObject _myScrollbar;

    #region Accessors -------------------------------------------------------------------------------------------------------

    public Deck MyCards {
        get => _myCards;
        set => _myCards = value;
    }

    #endregion -------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        CardFactory.InitializeFactory(); // Ensure the CardFactory is initialized

        // Load data fields
        _cardBannerPF = Resources.Load<GameObject>("Prefabs/UI/CardBanner");

        // UI references
        _cardBannerList = new List<GameObject>();
        _cardViewport = gameObject.transform.Find("CardViewport").gameObject;
        _cardListContent = _cardViewport.transform.Find("CardListContent").gameObject; // Content MUST be childed under the card viewport to function properly
        _contentTrans = _cardListContent.transform;
        _myScrollbar = gameObject.transform.Find("CardListScrollbar").gameObject;

        // Validate references obtained successfully
        Assert.IsNotNull(_cardBannerPF);

        Assert.IsNotNull(_cardBannerList);
        Assert.IsNotNull(_cardViewport);
        Assert.IsNotNull(_cardListContent);
        Assert.IsNotNull(_contentTrans);
        Assert.IsNotNull(_myScrollbar);

        // Initialize GUI
        GenerateTestDeck();
        ResetListContent();
    }

    /* Creates an instance of a Card banner prefab to be inserted into the scroll view area of the CardScrollList.
     * Note that this method is called from the AddCardToList method in order to fully add the prefab to the proper
     * place in the hierarchy (so you will typically want to call that method when interacting with a CardScrollList).
     */
    private GameObject CreateBannerPrefab(Card card)
    {
        GameObject cardBannerGO = MonoBehaviour.Instantiate(_cardBannerPF); // create instance of banner prefab

        cardBannerGO.transform.Find("art").GetComponent<Image>().sprite = card.CardBannerArt;  // dynamically populate banner UI elements
        cardBannerGO.transform.Find("card_name").GetComponent<Text>().text = card.Name;
        cardBannerGO.GetComponent<CardBanner>().Card = card;

        return cardBannerGO;
    }

    /* Add a card to the list (both the GUI card banner and Card to the _myCards deck)
     */
    public void AddCardToList(Card card)
    {
        GameObject cardBannerGO = CreateBannerPrefab(card);
        _myCards.AddCard(cardBannerGO.GetComponent<CardBanner>().Card);

        cardBannerGO.transform.SetParent(_contentTrans);
    }

    /* Removes a card via the correspond card banner game object
     * Having a reference to the game object IN ADDITION to the actual Card is important for actually updating the UI
     */
    public void RemoveCardFromList(GameObject cardGO)
    {
        _myCards.RemoveCard(cardGO.GetComponent<CardBanner>().Card); // TODO: Make this more reference-secure
        cardGO.Destroy();
    }

    /* Fully resets the content within the CardScrollList (also used for initialization purposes)
     * NOTE: The CardScrollList will then populate itself with card banners corresponding to its _myCards field
     */
    private void ResetListContent()
    {
        // Cleanup previous
        foreach(GameObject go in _cardBannerList) { RemoveCardFromList(go); }

        // Create new
        foreach(Card c in _myCards.Cards) { AddCardToList(c); }
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
