using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : Card, ICardInterface
{
    public DrawCards() : base(id: 2, name: "Draw Cards 1", detail: "Draw 1 additional Card.", cardFlavor: "", level: 1, mem_cost: 1)
    {
    }

    public override void playCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            player.DeckManager.DrawCard(1);
            player.Memory -= MemoryCost;
        }
    }
}
