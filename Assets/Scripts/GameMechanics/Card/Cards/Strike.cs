using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Card
{

    public static int _Id = 0;
    public string _Name = "Strike";
    public string _Detail = "Deal 20 damage to the enemy.";
    public string _Flavor = "Simple solutions to complicated problems.";
    public int _Level = 1;
    public int _MemoryCost = 2;
    public double _UpgradeCost = 50;
    public int _DamageAmount = 20;
    public Sprite _CardArt = Resources.Load<Sprite>("Sprites/UI/Card Art/Strike");

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
            player.ExecuteAttack(enemy, _DamageAmount);
            _cc.ChangeMemory(-MemoryCost);
        }        
    }

    public override void UpgradeCard()
    {
        if (_Level <= 10)
        {
            _UpgradeCost += (_Level - 1) * 10;
            _Level++;
            _DamageAmount += 5;

            // Every 2 levels memory cost goes up 1 while Card Level <= 10.
            if (_Level % 2 == 0)
            {
                _MemoryCost += 1;
            }

        }
        else
        {
            _UpgradeCost += (_Level - 1) ^ 2 * 15;
            _Level++;

            if (_Level % 2 == 0)
            {
                _DamageAmount += 2;
            }
        }
        _Detail = "Deal " + _DamageAmount + " damage to the enemy.";
    }
}
