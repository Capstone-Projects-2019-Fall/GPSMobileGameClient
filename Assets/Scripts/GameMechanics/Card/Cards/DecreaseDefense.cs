using System.Collections;
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
    public Sprite _CardBannerArt = Resources.Load<Sprite>("Sprites/UI/Card Art Banners/decrease_defense-banner");

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
            _cc.DebuffEnemy(new Buff(name: _DefenseModifier.ToString("#.##") + "x Defense", defenseModifier: _DefenseModifier));
            _cc.ChangeMemory(-MemoryCost);
        }
    }

    // Updates the level in the card from the database.
    public void SetCardLevel(int level)
    {
        _Level = level;
        _UpgradeCost = 75 + (_Level - 1) * 100;
        _DefenseModifier = 0.9f + (_Level - 1) * 0.1f;
        _Name = "Decrease Defense " + Utils.ToRoman(_Level);
    }

    // Upgrades the card. Max level of 10.
    public override void UpgradeCard()
    {
        if (_Level < 10)
        {
            _Level++;
            _UpgradeCost = 75 + (_Level - 1) * 100;
            _DefenseModifier = 0.9f + (_Level - 1) * 0.1f;
            _Name = "Decrease Defense " + Utils.ToRoman(_Level);
        }
    }
}
