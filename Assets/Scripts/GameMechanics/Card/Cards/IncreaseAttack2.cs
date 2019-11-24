using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAttack2 : Card
{
    public override int Id => 8;

    public override string Name => "Increase Attack 2";

    public override string Detail => "Deal 60% more damage with attacks for 1 round.";

    public override string Flavor => "We must optimize ourselves to overcome such existential threats.";

    public override int Level => 2;

    public override int MemoryCost => 3;

    public override int UpgradeCost => 300;

    public override void PlayCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            player.BuffReceived(new Buff(name:"1.6x Damage",attackModifier:1.6f));
            _cc.ChangeMemory(-MemoryCost);
        }
    }
}
