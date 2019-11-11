using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
   public void  OnDrop(PointerEventData data)
    {
        Drag d = data.pointerDrag.GetComponent<Drag>();
        if(d != null)
        {
            d.returnParent = this.transform;
        }

    }
}
