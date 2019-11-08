using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardInterface
{
    bool InCombat { get; set; }

    void playCard(Card card, Enemy enemy);
}
