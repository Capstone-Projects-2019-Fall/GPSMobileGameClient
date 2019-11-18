using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[CreateAssetMenu(menuName ="Card")]
public abstract class Card : ICardInterface
{
    abstract public int Id { get; }
    abstract public string Name { get; }
    abstract public string Detail { get; }
    abstract public string Flavor { get; }
    abstract public int Level { get; }
    abstract public int MemoryCost { get; }
    //abstract public Sprite CardSprite { get; }
    
    public virtual void playCard(Player p, Enemy e) { }
    
    // Initializes this card
}
