using UnityEngine;
using UnityEngine.EventSystems;

public class CombatDrop : Drop
{
   public override void OnDrop(PointerEventData data)
    {
        CombatDrag d = data.pointerDrag.GetComponent<CombatDrag>();
        if(d != null)
        {
            d.ReturnParent = this.transform;
        }

    }
}
