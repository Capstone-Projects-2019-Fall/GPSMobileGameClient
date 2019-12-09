using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* CardImage Description:
 * A CardImage is a special type of UI element that populates a CardInventoryZone. They are called "images" because they 
 * look exactly like a Card would look when in a live combat situation, but they have all of their combat functionality
 * stripped away from them (as such, no references to CombatController, CardHandlers, DeckManagers, UIController, etc. are
 * necessary */
 [RequireComponent(typeof(CardImageDrag))]
public class CardImage : MonoBehaviour
{
    // Data
    [SerializeField] private Card _myCard;
    [SerializeField] private CardImageDrag _myDrag;

    // UI
    [SerializeField] private CardInventoryZone _myInventoryZone;

    #region Accessors ----------------------------------------------------------------------------------------

    public Card Card {
        get => _myCard;
        set => _myCard = value;
    }

    public CardInventoryZone CardInventoryZone {
        get => _myInventoryZone;
        set => _myInventoryZone = value;
    }

    #endregion -----------------------------------------------------------------------------------------------

    private void Awake()
    {
        _myDrag = GetComponent<CardImageDrag>();
    }

    public void PlaceInInventoryZone(CardInventoryZone cInvZone)
    {
        _myInventoryZone = cInvZone;
        transform.SetParent(cInvZone.transform.FindDeepChild("CardListContent"));
    }
}
