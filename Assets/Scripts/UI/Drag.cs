using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform returnParent = null;

    private CombatController _cc;
    private CardHandler _ch;

    private void Awake()
    {
        _cc = GameObject.Find("CombatUtils").GetComponent<CombatController>();
        _ch = gameObject.GetComponent<CardHandler>();
    }

    private void Start()
    {

    }

    public void OnBeginDrag(PointerEventData data)
    {
        returnParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData data)
    {
        this.transform.position = data.position;

    }

    public void OnEndDrag(PointerEventData data)
    {
        this.transform.SetParent(returnParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if(returnParent.name == "PlayZone")
        {
            _ch.PlayCard(_cc.Player, _cc.Enemy);
        }
    }
    
}
