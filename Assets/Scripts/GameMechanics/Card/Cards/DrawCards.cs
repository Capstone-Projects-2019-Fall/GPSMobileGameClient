﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : Card
{
    public override int Id => 2;

    public override string Name => "DrawCards";

    public override string Detail => "Draw 1 card from your deck";

    public override string Flavor => "Recursion!";

    public override int Level => 1;

    public override int MemoryCost => 1;

    public override void PlayCard(Player player, Enemy enemy)
    {
        if (player.Memory >= MemoryCost)
        {
            _cc.DrawCards(1);
            _cc.ChangeMemory(-MemoryCost);
        }
    }
}
