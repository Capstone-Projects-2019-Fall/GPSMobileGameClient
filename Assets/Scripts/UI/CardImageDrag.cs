using UnityEngine;
using UnityEngine.EventSystems;

public class CardImageDrag : Drag
{
    [SerializeField] private Transform _returnParent = null;

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

    public override void OnEndDrag(PointerEventData data)
    {
        this.transform.SetParent(_returnParent);    
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
