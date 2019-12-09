using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Buff : MonoBehaviour
{
    [SerializeField]
    private string name = "";
    [SerializeField]
    private int roundDuration = 1;
    [SerializeField]
    private float attackModifier = 1f;
    [SerializeField]
    private float defenseModifier = 1f;

    public string Name { get { return name; } set { name = value; } }
    public int RoundDuration { get { return roundDuration; } set { roundDuration = value; } }
    public float Attack_Modifier { get { return attackModifier; } set { attackModifier = value; } }
    public float Defense_Modifier { get { return defenseModifier; } set { defenseModifier = value; } }

    public Buff()
    {
    }

    public Buff(string name, int roundDuration = 1, float attackModifier = 1, float defenseModifier = 1)
    {
        this.name = name;
        this.roundDuration = roundDuration;
        this.attackModifier = attackModifier;
        this.defenseModifier = defenseModifier;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
