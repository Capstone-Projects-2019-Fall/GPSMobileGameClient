using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    private string name = "";
    private int roundDuration = 1;
    private float attackModifier = 1f;
    private float defenseModifier = 1f;

    public string Name { get { return name; } set { name = value; } }
    public int RoundDuration { get { return roundDuration; } set { roundDuration = value; } }
    public float AttackModifier { get { return attackModifier; } set { attackModifier = value; } }
    public float DefenseModifier { get { return defenseModifier; } set { defenseModifier = value; } }

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
