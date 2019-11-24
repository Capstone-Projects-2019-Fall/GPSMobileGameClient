using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseDefense1 : Card
{
    public override int Id => 4;

    public override string Name => "Decrease Defense 1";

    public override string Detail => "Makes the enemy more vulnerable to damage";

    public override string Flavor => "Even the strongest barriers have weaknesses.";

    public override int Level => 1;

    public override int MemoryCost => 4;

    public override int UpgradeCost => 75;

    public override void PlayCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            enemy.BuffReceived(new Buff(name: "0.75x Defense", defenseModifier: 0.75f));
            _cc.ChangeMemory(-MemoryCost);
        }
    }
}
