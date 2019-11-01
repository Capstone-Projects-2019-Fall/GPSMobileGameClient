using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyNode : NodeStructure
{
    private static string _type = "Friendly";
    private static Sprite _sprite = Resources.Load<Sprite>("Sprites/friendly-node-01");

    public override string Type 
    {
        get => _type;
    }

    public override Sprite Sprite
    {
        get => _sprite;
    }
}
