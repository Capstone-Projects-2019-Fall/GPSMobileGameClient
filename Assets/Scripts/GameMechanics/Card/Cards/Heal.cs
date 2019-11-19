using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Card, ICardInterface
{

    public override void playCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            player.addHealth(30.0f);
            player.Memory -= MemoryCost;
        }
    }

    // Initializes this card
    protected override void Awake()
    {
        Id = 1;
        Name = "Heal 1";
        Detail = "Restores 30 HP.";
        Flavor = "";
        Level = 1;
        MemoryCost = 3;
    }
}
