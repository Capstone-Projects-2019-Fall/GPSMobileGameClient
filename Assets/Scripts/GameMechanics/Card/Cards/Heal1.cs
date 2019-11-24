using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal1 : Card
{
    public override int Id => 1;

    public override string Name => "Heal";

    public override string Detail => "Restores 30 HP.";

    public override string Flavor => "Automated hardware and software repair inspired by organic systems.";

    public override int Level => 1;

    public override int MemoryCost => 3;

    public override int UpgradeCost => 100;

    public override void PlayCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            player.addHealth(30.0f);
            _cc.ChangeMemory(-MemoryCost);
        }
    }
}
