using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Card, ICardInterface
{
    public Heal() : base(id: 1, name: "Heal 1", detail: "Restores 30 HP.", cardFlavor: "", level: 1, mem_cost: 3)
    {
    }

    public override void playCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            player.addHealth(30.0f);
            player.Memory -= MemoryCost;
        }
    }
}
