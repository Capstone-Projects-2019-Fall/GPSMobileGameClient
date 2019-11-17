using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAttack : Card, ICardInterface
{
    public override void playCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            player.buffReceived(new Buff(name:"1.5x Damage",attackModifier:1.5f));
            player.Memory -= MemoryCost;
        }
    }

    // Initializes this card
    protected override void Awake()
    {
        Id = 3;
        Name = "Increase Attack 1";
        Detail = "Deal 50% more damage per attack for 1 round.";
        Flavor = "";
        Level = 1;
        MemoryCost = 2;
    }
}
