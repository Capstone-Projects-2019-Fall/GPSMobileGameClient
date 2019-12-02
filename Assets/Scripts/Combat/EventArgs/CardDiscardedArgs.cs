using System;
using UnityEngine;

public class CardDiscardedArgs : EventArgs
{
    public Card Card { get; set; }
    public GameObject CardGO { get; set; }
}
