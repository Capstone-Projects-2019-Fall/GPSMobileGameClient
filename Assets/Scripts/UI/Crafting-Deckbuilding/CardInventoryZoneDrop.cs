using UnityEngine;
using UnityEngine.EventSystems;

public class CardInventoryZoneDrop : Drop
{
   public override void OnDrop(PointerEventData data)
    {
        Drag d = data.pointerDrag.GetComponent<Drag>();
        if(d != null)
        {
            d.ReturnParent = this.transform;
        }

    }
}
