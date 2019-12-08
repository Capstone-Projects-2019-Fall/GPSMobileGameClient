﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseDefense : Card
{

    public static int _Id = 4;
    public string _Name = "Decrease Defense";
    public string _Detail = "Makes the enemy more vulnerable to damage.";
    public string _Flavor = "Even the strongest barriers have weaknesses.";
    public int _Level = 1;
    public int _MemoryCost = 4;
    public double _UpgradeCost = 75;
    public float _DefenseModifier = 0.9f;
    public Sprite _CardArt = Resources.Load<Sprite>("Sprites/UI/Card Art/Decrease Defense");
    public Sprite _CardBannerArt = Resources.Load<Sprite>("Sprites/UI/Card Art Banners/decrease_defense-banner.png");

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
            enemy.BuffReceived(new Buff(name: _DefenseModifier.ToString("#.##") + "x Defense", defenseModifier: _DefenseModifier));
            _cc.ChangeMemory(-MemoryCost);
        }
    }

    public override void UpgradeCard()
    {
        if (_Level <= 10)
        {
            _UpgradeCost += (_Level - 1) * 100;
            _Level++;
            _DefenseModifier += 0.1f;

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
                _DefenseModifier += 0.05f;
            }
        }
    }
}
