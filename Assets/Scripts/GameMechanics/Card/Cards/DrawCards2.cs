using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards2 : Card
{
    public override int Id => 6;

    public override string Name => "Draw Cards 2";

    public override string Detail => "Draw 2 card from your deck";

    public override string Flavor => "Recursion Recursion!";

    public override int Level => 2;

    public override int MemoryCost => 2;

    public override int UpgradeCost => 5000;

    public override void PlayCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            _cc.DrawCards(1);
            _cc.ChangeMemory(-MemoryCost);
        }
    }

    public override void UpgradeCard(Player p, Card card)
    {
        
    }
}
