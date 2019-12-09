using UnityEngine;
using UnityEngine.EventSystems;

/* CardBannerDrag Description:
 * A utility script to be attached to objects that you want to add drag and drop functionality to. This script should be attached specifically to a CardImage
 * (the UI data object that populates CardInventoryZones) */
public class CardImageDrag : Drag
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

    /* Called when the player releases the CardImage on top of any object with a drop handler
     * When the new parent is a CardScrollList, a new CardBanner will be created using the data stored within
     * this CardImage, and the original CardImage will be destroyed */
    public override void OnEndDrag(PointerEventData data)
    {
        this.transform.SetParent(_returnParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (_returnParent.name == "CardScrollList")
        {
            Card myCard = gameObject.GetComponent<CardImage>().Card;
            _returnParent.gameObject.GetComponent<CardScrollList>().AddCardToList(myCard);
            gameObject.GetComponent<CardImage>().CardInventoryZone.RemoveCardFromList(gameObject);
        }
    }
}
