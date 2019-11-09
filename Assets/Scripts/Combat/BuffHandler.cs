﻿/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    public List<Buff> buffList = new List<Buff>();

    public void addBuff(Buff buff)
    {
        buffList.Add(buff);
    }

    public void removeBuff(Buff buff)
    {
        buffList.Remove(buff);
    }

    public float calculateAttackModifier()
    {
        float attackModifier = 1;
        foreach (Buff buff in buffList)
        {
            attackModifier *= buff.Attack_Modifier;            
        }
        return attackModifier;
    }

    public float calculateDefenseModifier()
    {
        float defenseModifier = 1;
        foreach (Buff buff in buffList)
        {
            defenseModifier *= buff.Attack_Modifier;
        }
        return defenseModifier;
    }

    public void decrementBuffUsages()
    {
        foreach (Buff buff in buffList)
        {
            buff.RoundDuration--;
            if (buff.RoundDuration == 0)
            {
                buffList.Remove(buff);
            }
        }
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

*/