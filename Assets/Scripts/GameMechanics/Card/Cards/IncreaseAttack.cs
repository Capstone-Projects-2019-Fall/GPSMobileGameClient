using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAttack : Card
{

    public static int _Id = 3;
    public string _Name = "Increase Attack";
    public string _Detail = "Deal 10% more damage with attacks for 1 round.";
    public string _Flavor = "We must optimize ourselves to overcome such existential threats.";
    public int _Level = 1;
    public int _MemoryCost = 2;
    public double _UpgradeCost = 100;
    public float _AttackModifier = 1.1f;
    public Sprite _CardArt = Resources.Load<Sprite>("Sprites/UI/Card Art/Increase Attack");

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
            player.BuffReceived(new Buff(name: _AttackModifier.ToString("#.##") + "x Damage",attackModifier:_AttackModifier));
            _cc.ChangeMemory(-MemoryCost);
        }
    }

    // Updates the level in the card from the database.
    public void SetCardLevel(int level)
    {
        _Level = level;
        _UpgradeCost = 100 + (_Level - 1) * 100;
        _AttackModifier = 1.1f + (_Level - 1) * 0.1f;
        _Name = "Increase Attack " + Utils.ToRoman(_Level);
        _Detail = "Deal " + _AttackModifier + "% more damage with attacks for 1 round.";
    }

    // Upgrades the card. Max level of 10.
    public override void UpgradeCard()
    {
        if (_Level < 10)
        {
            _Level++;
            _UpgradeCost = 100 + (_Level - 1) * 100;
            _AttackModifier = 1.1f + (_Level - 1) * 0.1f;
            _Name = "Increase Attack " + Utils.ToRoman(_Level);
            _Detail = "Deal " + (_AttackModifier - 1) + "% more damage with attacks for 1 round.";
        }
    }
    
}
