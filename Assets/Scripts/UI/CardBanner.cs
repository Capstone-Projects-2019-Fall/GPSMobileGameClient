using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/* CardBanner Description: 
 * Primary controller script attached to a Card Banner prefab. Card Banners are used in CardScrollLists to create nice-looking interactive lists
 * of cards. The primary function of this script is to serve as a wrapper for a Card object (which will populated dynamically along with the Card
 * Banner, but some additional local UI control can be added here as well.
 */
[RequireComponent(typeof(CardBannerDrag))]
public class CardBanner : MonoBehaviour
{
    // Data
    [SerializeField] private Card _myCard;
    [SerializeField] private CardBannerDrag _myDrag;

    // UI
    [SerializeField] private CardScrollList _myScrollList;
    [SerializeField] private string _cardName;
    [SerializeField] private Sprite _bannerArt;

    #region Accessors ------------------------------------------------------------------------------------------------------------------

    public Card Card {
        get => _myCard;
        set => _myCard = value;
    }

    public CardScrollList CardScrollList {
        get => _myScrollList;
        set => _myScrollList = value;
    }

    #endregion -------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        _myDrag = gameObject.GetComponent<CardBannerDrag>();
        _cardName = gameObject.transform.Find("card_name").GetComponent<Text>().text;
        _bannerArt = gameObject.transform.Find("art").GetComponent<Image>().sprite;

        Assert.IsNotNull(_myDrag);
    }

    /* Updates the sprite and name of the banner in accordance with the given card
     * Mainly called from the CardScrollList */
    public void UpdateBanner(Card c)
    {
        _myCard = c;
        _cardName = c.Name;
        _bannerArt = c.CardBannerArt;
    }

    public void PlaceInScrollList(CardScrollList scrollList)
    {
        _myScrollList = scrollList;
        transform.SetParent(scrollList.transform.FindDeepChild("CardListContent"));
    }
}
