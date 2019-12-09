using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunButtonHandler : MonoBehaviour
{
    [SerializeField] private CombatController _cc;

    public void Awake()
    {
        _cc = GameObject.Find("CombatUtils").GetComponent<CombatController>();
    }

    public void FleeCombat()
    {
        _cc.FleeCombat();
    }
}
