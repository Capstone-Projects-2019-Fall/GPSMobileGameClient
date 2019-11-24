using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards1 : Card
{
    // 
    public static int _Id = 2;
    public static string _Name = "Draw Cards 1";
    public static string _Detail = "Draw 1 card from your deck";
    public static string _Flavor = "Recursion!";
    public static int _Level = 1;
    public static int _MemoryCost = 1;
    public static int _UpgradeCost = 1000;

    public override int Id => _Id;

    public override string Name => _Name;

    public override string Detail => _Detail;

    public override string Flavor => _Flavor;

    public override int Level => _Level;

    public override int MemoryCost => _MemoryCost;

    public override int UpgradeCost => _UpgradeCost;

    public override void PlayCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            _cc.DrawCards(1);
            _cc.ChangeMemory(-MemoryCost);
        }
    }

    public override int UpgradeCard()
    {
        return 6;
    }
}
