using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
   public void OnDrop(PointerEventData data)
    {
        CombatDrag d = data.pointerDrag.GetComponent<CombatDrag>();
        if(d != null)
        {
            d.returnParent = this.transform;
        }

    }
}
