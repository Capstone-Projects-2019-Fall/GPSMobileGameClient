using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal2 : Card
{
    public override int Id => 7;

    public override string Name => "Heal 2";

    public override string Detail => "Restores 35 HP.";

    public override string Flavor => "Automated hardware and software repair inspired by organic systems.";

    public override int Level => 2;

    public override int MemoryCost => 3;

    public override int UpgradeCost => 500;

    public override void PlayCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            player.addHealth(35.0f);
            _cc.ChangeMemory(-MemoryCost);
        }
    }
}
