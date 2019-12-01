using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Card
{

    public static int _Id = 1;
    public string _Name = "Heal";
    public string _Detail = "Restores 15 HP.";
    public string _Flavor = "Automated hardware and software repair inspired by organic systems.";
    public int _Level = 1;
    public int _MemoryCost = 3;
    public double _UpgradeCost = 100;
    public int _HealAmount = 15;

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
            player.addHealth(_HealAmount);
            _cc.ChangeMemory(-MemoryCost);
        }
    }

    public override void UpgradeCard()
    {
        if(_Level <= 5)
        {
            _UpgradeCost += (_Level - 1) * 15;
            _Level++;
            _HealAmount += 2;

            // Every 2 levels memory cost goes up 1 while Card Level <= 10.
            if (_Level % 5 == 0)
            {
                _MemoryCost += 1;
            }

        } else
        {
            _UpgradeCost += (_Level - 1)^2 * 7;
            _Level++;
            _HealAmount += 1;
            
        }
        _Detail = "Restores " + _HealAmount  + " HP.";
    }
}
