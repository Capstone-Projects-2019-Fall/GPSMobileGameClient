using UnityEngine;
using UnityEngine.EventSystems;


/* CardBannerDrag Description:
 * A utility script to be attached to objects that you want to add drag and drop functionality to. This script should be attached specifically to a CardBanner
 * (the UI data object that populates CardScrollLists) */ 
public class CardBannerDrag : Drag
{
    private Transform _returnParent = null;

    public override Transform ReturnParent {
        get => _returnParent;
        set => _returnParent = value;
    }

    public override void OnBeginDrag(PointerEventData data)
    {
        _returnParent = this.transform.parent;
        this.transform.SetParent(GameObject.Find("Canvas").transform); // Set the parent to the UI canvas to facilitate drops between different UI elements

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public override void OnDrag(PointerEventData data)
    {
        this.transform.position = data.position;

    }

    /* Called when the player releases the CardBanner on top of any object with a drop handler
     * When the new parent is the CardInventoryZone, a new CardImage will be created using the data from this
     * CardBanner's card, and the original CardBanner will be destroyed. */
    public override void OnEndDrag(PointerEventData data)
    {
        this.transform.SetParent(_returnParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if(_returnParent.name == "CardInventoryZone")
        {
            Card myCard = gameObject.GetComponent<CardBanner>().Card;
            _returnParent.gameObject.GetComponent<CardInventoryZone>().AddCardToList(myCard);
            gameObject.GetComponent<CardBanner>().CardScrollList.RemoveCardFromList(gameObject);
        }
    }    
}
