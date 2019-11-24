using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike2 : Card
{
    public override int Id => 9;

    public override string Name => "Strike 2";

    public override string Detail => "Deal 30 damage to the enemy.";

    public override string Flavor => "Simple solutions to complicated problems.";

    public override int Level => 2;

    public override int MemoryCost => 5;

    public override int UpgradeCost => 200;

    public override void PlayCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            Debug.LogFormat("Start of Strike: EnemyHP: {0}, PlayerMem: {1}", enemy.Health, player.Memory);
            player.ExecuteAttack(enemy, 30);
            _cc.ChangeMemory(-MemoryCost);
            Debug.LogFormat("End of Strike: EnemyHP: {0}, PlayerMem: {1}", enemy.Health, player.Memory);
        }        
    }
}
