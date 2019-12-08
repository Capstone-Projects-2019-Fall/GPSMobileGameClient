using UnityEngine;
using UnityEngine.EventSystems;


/* Drag Description:
 * A utility script to be attached to objects that you want to add drag and drop functionality to. This version of Drag is 'scene agnostic',
 * in that it should be plug-in-and-play ready in a variety of scenes and on a variety of objects (check out the CombatDrag.cs script for
 * the previous version of Drag that interaces with the Combat scene)
 */ 
public class CardBannerDrag : Drag
{
    [SerializeField] private Transform _returnParent = null;

    public override void OnBeginDrag(PointerEventData data)
    {
        _returnParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public override void OnDrag(PointerEventData data)
    {
        this.transform.position = data.position;

    }

    public override void OnEndDrag(PointerEventData data)
    {
        this.transform.SetParent(_returnParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }    
}
