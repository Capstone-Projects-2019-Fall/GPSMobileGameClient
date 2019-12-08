﻿using UnityEngine;
using UnityEngine.EventSystems;


/* Drag Description:
 * A utility script to be attached to objects that you want to add drag and drop functionality to. This version of Drag is 'scene agnostic',
 * in that it should be plug-in-and-play ready in a variety of scenes and on a variety of objects (check out the CombatDrag.cs script for
 * the previous version of Drag that interaces with the Combat scene)
 */ 
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
