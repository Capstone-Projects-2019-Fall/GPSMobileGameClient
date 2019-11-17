using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Card, ICardInterface
{
    public override void playCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            Debug.LogFormat("Start of Strike: EnemyHP: {0}, PlayerMem: {1}", enemy.Health, player.Memory);
            player.executeAttack(enemy, 20);
            player.Memory -= MemoryCost;
            Debug.LogFormat("End of Strike: EnemyHP: {0}, PlayerMem: {1}", enemy.Health, player.Memory);
        }        
    }

    // Initializes this card
    protected override void Awake()
    {
        Id = 0;
        Name = "Strike 1";
        Detail = "Deal 20 damage.";
        Flavor = "";
        Level = 1;
        MemoryCost = 2;
    }
}
