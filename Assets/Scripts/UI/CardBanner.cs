using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* CardBanner Description: 
 * Primary controller script attached to a Card Banner prefab. Card Banners are used in CardScrollLists to create nice-looking interactive lists
 * of cards. The primary function of this script is to serve as a wrapper for a Card object (which will populated dynamically along with the Card
 * Banner, but some additional local UI control can be added here as well.
 */
public class CardBanner : MonoBehaviour
{
    [SerializeField] private Card _myCard;

    #region Accessors ------------------------------------------------------------------------------------------------------------------

    public Card Card {
        get => _myCard;
        set => _myCard = value;
    }

    #endregion -------------------------------------------------------------------------------------------------------------------------
}
