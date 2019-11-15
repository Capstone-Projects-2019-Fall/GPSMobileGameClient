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
        Id = 3;
        Name = "Increase Attack 1";
        Detail = "Deal 50% more damage per attack for 1 round.";
        Flavor = "";
        Level = 1;
        MemoryCost = 2;
    }
}
