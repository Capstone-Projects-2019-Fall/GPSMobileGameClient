using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseDefense : Card, ICardInterface
{
    public DecreaseDefense() : base(id: 4, name: "Decrease Defense 1", detail: "The Enemy's defenses are decreased by 25%", cardFlavor: "", level: 1, mem_cost: 4)
    {
    }

    public void playCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            enemy.buffReceived(new Buff(name: "0.75x Defense", defenseModifier: 0.75f));
            player.Memory -= MemoryCost;
        }
    }
}
