using UnityEngine;
using UnityEngine.EventSystems;

public class CombatDrag : Drag
{
    private Transform _retParent = null;

    private CombatController _cc;
    private CardHandler _ch;

    public override Transform ReturnParent {
        get => _retParent;
        set => _retParent = value;
    }

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
        _retParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public override void OnDrag(PointerEventData data)
    {
        this.transform.position = data.position;

    }

    public override void OnEndDrag(PointerEventData data)
    {
        this.transform.SetParent(_retParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if(_retParent.name == "PlayZone")
        {
            _ch.PlayCard(_cc.Player, _cc.Enemy);
        }
    }    
}
