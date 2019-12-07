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
    public Sprite _CardArt = Resources.Load<Sprite>("Sprites/UI/Card Art/Heal");

    public override int Id => _Id;

    public override string Name => _Name;

    public override string Detail => _Detail;

    public override string Flavor => _Flavor;

    public override int Level => _Level;

    public override int MemoryCost => _MemoryCost;

    public override double UpgradeCost => _UpgradeCost;

    public override Sprite CardArt => _CardArt;

    public override void PlayCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            _cc.ChangePlayerHealth(_HealAmount);
            _cc.ChangeMemory(-MemoryCost);
        }
    }

    // Updates the level in the card from the database.
    public void SetCardLevel(int level)
    {
        _Level = level;
        _UpgradeCost = 100 + (_Level - 1) * 15;
        _HealAmount = 15 + (_Level - 1) * 2;
        _Name = "Heal " + Utils.ToRoman(_Level);
        _Detail = "Restores " + _HealAmount + " HP.";
    }

    // Upgrades the card. Max level of 10. Extra card every 2 levels.
    public override void UpgradeCard()
    {
        if (_Level < 10)
        {
            _Level++;
            _UpgradeCost = 100 + (_Level - 1) * 15;
            _HealAmount = 15 + (_Level - 1) * 2;
            _Name = "Heal " + Utils.ToRoman(_Level);
            _Detail = "Restores " + _HealAmount + " HP.";
        }
    }
    
}
