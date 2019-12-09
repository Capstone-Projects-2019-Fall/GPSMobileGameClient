using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
   public virtual void OnDrop(PointerEventData data)
    {
        Drag d = data.pointerDrag.GetComponent<Drag>();
        if(d != null)
        {
            d.ReturnParent = this.transform;
        }

    }
}
