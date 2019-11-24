using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards1: Card
{

    public static int _Id = 2;
    public string _Name = "Draw Cards";
    public string _Detail = "Draw 1 card from your deck.";
    public string _Flavor = "Recursion!";
    public int _Level = 1;
    public int _MemoryCost = 1;
    public double _UpgradeCost = 1000;
    public int _DrawAmount = 1;

    public override int Id => _Id;

    public override string Name => _Name;

    public override string Detail => _Detail;

    public override string Flavor => _Flavor;

    public override int Level => _Level;

    public override int MemoryCost => _MemoryCost;

    public override double UpgradeCost => _UpgradeCost;

    public override void PlayCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            _cc.DrawCards(_DrawAmount);
            _cc.ChangeMemory(-MemoryCost);
        }
    }

    public override void UpgradeCard()
    {
        if (_Level <= 10)
        {
            _UpgradeCost += (_Level - 1) * 100;
            _Level++;
            // After the first 10 levels increase draw amount by 1 and memory cost by 2.
            if (_Level % 10 == 0)
            {
                _DrawAmount += 1;
                _MemoryCost += 2;
            }

        }
        else
        {
            _UpgradeCost += (_Level - 1) ^ 2 * 15;
            _Level++;

            if (_Level % 10 == 0)
            {
                _DrawAmount += 1;
                _MemoryCost += 2;
            }
        }
        _Detail = "Draw " + _DrawAmount + " card from your deck.";
    }
}
