using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAttack : Card
{
    public override int Id => 3;

    public override string Name => "Increase Attack";

    public override string Detail => "Deal 50% more damage with attacks for 1 round.";

    public override string Flavor => "We must optimize ourselves to overcome such existential threats.";

    public override int Level => 1;

    public override int MemoryCost => 2;

    public override void PlayCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            player.BuffReceived(new Buff(name:"1.5x Damage",attackModifier:1.5f));
            _cc.ChangeMemory(-MemoryCost);
        }
    }
}
