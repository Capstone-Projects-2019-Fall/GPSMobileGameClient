using System;
using UnityEngine;

public class CardPlayedArgs : EventArgs
{
    public Card Card { get; set; }
    public GameObject CardGO { get; set; }
}
