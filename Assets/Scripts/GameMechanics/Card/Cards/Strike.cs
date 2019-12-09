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
    public Sprite _CardBannerArt = Resources.Load<Sprite>("Sprites/UI/Card Art Banners/strike-banner");

    public override int Id => _Id;

    public override string Name => _Name;

    public override string Detail => _Detail;

    public override string Flavor => _Flavor;

    public override int Level => _Level;

    public override int MemoryCost => _MemoryCost;

    public override double UpgradeCost => _UpgradeCost;

    public override Sprite CardArt => _CardArt;

    public override Sprite CardBannerArt => _CardBannerArt;

    public override void PlayCard(Player player, Enemy enemy)
    {
        CombatController _cc = GameObject.Find("CombatUtils").GetComponent<CombatController>();

        if (player.Memory >= MemoryCost)
        {
            _cc.ChangeEnemyHealth(-player.CalculateDamage(enemy, _DamageAmount));
            //_cc.Enemy.GetComponent<Shak   e>().ShakeIt();
            Camera.main.GetComponent<Shake>().ShakeIt();
            _cc.ChangeMemory(-MemoryCost);
        }        
    }

    // Updates the level in the card from the database.
    public void SetCardLevel(int level)
    {
        _Level = level;
        _UpgradeCost = 100 + (_Level - 1) * 10;
        _DamageAmount = 20 + (_Level - 1) * 5;
        _Name = "Strike " + Utils.ToRoman(_Level);
        _Detail = "Deal " + _DamageAmount + " damage to the enemy.";
    }

    // Upgrades the card. Max level of 10.
    public override void UpgradeCard()
    {
        if (_Level < 10)
        {
            _Level++;
            _UpgradeCost = 100 + (_Level - 1) * 10;
            _DamageAmount = 20 + (_Level - 1) * 5;
            _Name = "Strike " + Utils.ToRoman(_Level);
            _Detail = "Deal " + _DamageAmount + " damage to the enemy.";
        }
    }
}
