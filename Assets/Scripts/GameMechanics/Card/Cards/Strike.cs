using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Card, ICardInterface
{
    public override int Id {
        get => 0;
    }
    public override string Name {
        get => "Strike";
    }
    public override string Detail {
        get => "Deal 20 damage to the enemy.";
    }
    public override string Flavor {
        get => "Simple solutions to complicated problems.";
    }
    public override int Level {
        get => 1;
    }
    public override int MemoryCost {
        get => 2;
    }


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

    }
}
