using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Card, ICardInterface
{
    public Strike() : base(id:0, name:"Strike 1", detail:"Does 20 damage.", cardFlavor:"", level:1, mem_cost:2)
    {
    }

    public override void playCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            player.executeAttack(enemy, 20);
            player.Memory -= MemoryCost;
        }        
    }
}
