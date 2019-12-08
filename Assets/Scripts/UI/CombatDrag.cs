using UnityEngine;
using UnityEngine.EventSystems;

public class CombatDrag : Drag
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

    public override void OnBeginDrag(PointerEventData data)
    {
        returnParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public override void OnDrag(PointerEventData data)
    {
        this.transform.position = data.position;

    }

    public override void OnEndDrag(PointerEventData data)
    {
        this.transform.SetParent(returnParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if(returnParent.name == "PlayZone")
        {
            _ch.PlayCard(_cc.Player, _cc.Enemy);
        }
    }    
}
