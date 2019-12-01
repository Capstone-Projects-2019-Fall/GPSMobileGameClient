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

    public override void UpgradeCard()
    {
        if (_Level <= 10)
        {
            _UpgradeCost += (_Level - 1) * 100;
            _Level++;
            _AttackModifier += 0.1f;

            // Every 2 levels memory cost goes up 1 while Card Level < 10.
            if (_Level % 2 == 0)
            {
                _MemoryCost += 1;
            }

        }
        else
        {
            _UpgradeCost += (_Level - 1) ^ 2 * 5;
            _Level++;

            if (_Level % 2 == 0)
            {
                _AttackModifier += 0.05f;
            }
        }
    }
}
