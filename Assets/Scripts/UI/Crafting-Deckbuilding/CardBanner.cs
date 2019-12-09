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
        _myDrag = GetComponent<CardBannerDrag>();
    }

    public void PlaceInScrollList(CardScrollList scrollList)
    {
        _myScrollList = scrollList;
        transform.SetParent(scrollList.transform.FindDeepChild("CardListContent"));
    }
}
