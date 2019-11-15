using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : Card, ICardInterface
{

    public override void playCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            player.DeckManager.DrawCard(1);
            player.Memory -= MemoryCost;
        }
    }

    // Initializes this card
    protected override void Awake()
    {
        Id = 2;
        Name = "Draw Cards 1";
        Detail = "Draw 1 additional Card.";
        Flavor = "";
        Level = 1;
        MemoryCost = 1;
    }
}
