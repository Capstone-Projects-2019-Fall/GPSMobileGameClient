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
    public Sprite _CardArt = Resources.Load<Sprite>("Sprites/UI/Card Art/Draw Cards");

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
            _cc.SelectedPlayerDrawCards(_DrawAmount);
            _cc.ChangeMemory(-MemoryCost);
        }
    }

    // Updates the level in the card from the database.
    public void SetCardLevel(int level)
    {
        _Level = level;
        _UpgradeCost = 1000 + (_Level - 1) * 100;
        _DrawAmount = 1 + Mathf.FloorToInt((_Level - 1) / 2);
        _Name = "Draw Cards " + Utils.ToRoman(_Level);
        _Detail = "Draw " + _DrawAmount + " card from your deck.";
    }

    // Upgrades the card. Max level of 10. Extra card every 2 levels.
    public override void UpgradeCard()
    {
        if (_Level < 10)
        {
            _Level++;
            _UpgradeCost = 1000 + (_Level - 1) * 100;
            _DrawAmount = 1 + Mathf.FloorToInt((_Level - 1) / 2);
            _Name = "Draw Cards " + Utils.ToRoman(_Level);
            _Detail = "Draw " + _DrawAmount + " card from your deck.";
        }
    }
    
}
