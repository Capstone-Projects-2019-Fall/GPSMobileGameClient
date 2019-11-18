using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAttack : Card, ICardInterface
{
    public IncreaseAttack() : base(id: 3, name: "Increase Attack 1", detail: "Deal 50% more damage per attack for 1 round.", cardFlavor: "", level: 1, mem_cost: 2)
    {
    }

    public override void playCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            player.buffReceived(new Buff(name:"1.5x Damage",attackModifier:1.5f));
            player.Memory -= MemoryCost;
        }
    }
}
