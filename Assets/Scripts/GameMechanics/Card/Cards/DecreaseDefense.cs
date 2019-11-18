using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseDefense : Card, ICardInterface
{
    // plays this specific card
    public override void playCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            enemy.buffReceived(new Buff(name: "0.75x Defense", defenseModifier: 0.75f));
            player.Memory -= MemoryCost;
        }
    }

    // Initializes this card
    protected override void Awake()
    {
        Id = 4;
        Name = "Decrease Defense 1";
        Detail = "The Enemy's defenses are decreased by 25%";
        Flavor = "";
        Level = 1;
        MemoryCost = 4;
    }
}
