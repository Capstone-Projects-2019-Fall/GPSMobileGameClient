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
    [SerializeField] private Deck _cards;
    [SerializeField] private GameObject _cardBannerPF;

    // UI-related fields
    [SerializeField] private List<GameObject> _cardBannerList;
    [SerializeField] private GameObject _cardViewport;
    [SerializeField] private GameObject _myScrollbar;

    #region Accessors -------------------------------------------------------------------------------------------------------
    public Deck Cards {
        get => _cards;
        set => _cards = value;
    }

    #endregion


    private void Awake()
    {
        // Load data fields
        _cardBannerPF = Resources.Load<GameObject>("Prefabs/UI/CardBanner");

        // UI references
        _cardBannerList = new List<GameObject>();
        _cardViewport = gameObject.transform.Find("CardViewport").gameObject;
        _myScrollbar = gameObject.transform.Find("CardListScrollbar").gameObject;

        // Validate references obtained successfully
        Assert.IsNotNull(_cardBannerPF);

        Assert.IsNotNull(_cardBannerList);
        Assert.IsNotNull(_cardViewport);
        Assert.IsNotNull(_myScrollbar);
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

        return cardBannerGO;
    }

    public void AddCardToList(Card card)
    {
        GameObject cardBannerGO = MonoBehaviour.Instantiate(_cardBannerPF);
        cardBannerGO.transform.Find()
    }

    public void RemoveCardFromList()
    {

    }

    // Generate a starter deck for testing purposes
    private List<Card> GenerateTestDeckList()
    {
        List<Card> deckList = new List<Card>();
        for (int i = 0; i < 12; i++) { deckList.Add(CardFactory.CreateCard(0)); } // Strike
        for (int i = 0; i < 10; i++) { deckList.Add(CardFactory.CreateCard(1)); } // Heal
        for (int i = 0; i < 10; i++) { deckList.Add(CardFactory.CreateCard(2)); } // Draw Cards
        for (int i = 0; i < 4; i++) { deckList.Add(CardFactory.CreateCard(3)); } // Increase Attack
        for (int i = 0; i < 4; i++) { deckList.Add(CardFactory.CreateCard(4)); } // Decrease Defense

        return deckList;
    }
}
