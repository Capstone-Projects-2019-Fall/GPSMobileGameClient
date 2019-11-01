using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNode : NodeStructure
{
    private static string _type = "Enemy";
    private static Sprite _sprite = Resources.Load<Sprite>("Sprites/enemy-node-01");

    public override string Type 
    {
        get => _type;
    }

    public override Sprite Sprite 
    {
        get => _sprite;
    }
}
