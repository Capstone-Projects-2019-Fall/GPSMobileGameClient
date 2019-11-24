using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike1 : Card
{
    public override int Id => 0;
    
    public override string Name => "Strike 1";

    public override string Detail => "Deal 20 damage to the enemy.";
    
    public override string Flavor => "Simple solutions to complicated problems.";
    
    public override int Level => 1;
    
    public override int MemoryCost => 2;

    public override int UpgradeCost => 50;

    public override void PlayCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            Debug.LogFormat("Start of Strike: EnemyHP: {0}, PlayerMem: {1}", enemy.Health, player.Memory);
            player.ExecuteAttack(enemy, 20);
            _cc.ChangeMemory(-MemoryCost);
            Debug.LogFormat("End of Strike: EnemyHP: {0}, PlayerMem: {1}", enemy.Health, player.Memory);
        }        
    }
}
