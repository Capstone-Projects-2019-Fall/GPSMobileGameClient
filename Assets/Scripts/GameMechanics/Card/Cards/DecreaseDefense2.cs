using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseDefense2 : Card
{
    public override int Id => 5;

    public override string Name => "Decrease Defense 2";

    public override string Detail => "Makes the enemy more vulnerable to damage";

    public override string Flavor => "Even the strongest barriers have weaknesses.";

    public override int Level => 2;

    public override int MemoryCost => 7;

    public override int UpgradeCost => 300;

    public override void PlayCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            enemy.BuffReceived(new Buff(name: "0.65x Defense", defenseModifier: 0.65f));
            _cc.ChangeMemory(-MemoryCost);
        }
    }
}
