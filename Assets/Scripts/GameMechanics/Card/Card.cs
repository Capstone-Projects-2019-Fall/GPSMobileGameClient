using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[CreateAssetMenu(menuName ="Card")]
public abstract class Card : MonoBehaviour, ICardInterface
{
    [SerializeField] private int id;
    [SerializeField] private string cardName;
    [SerializeField] private string cardDetail;
    [SerializeField] private string cardFlavor;
    public Sprite art;
    [SerializeField] private int level;
    [SerializeField] private int memoryCost;

    public int Id { get => id; set => id = value; }
    public string Name { get => cardName; set => cardName = value; }
    public string Detail { get => cardDetail; set => cardDetail = value; }
    public string Flavor { get => cardFlavor; set => cardFlavor = value; }
    public int Level { get => level; set => level = value; }
    public int MemoryCost { get => memoryCost; set => memoryCost = value; }
    
    public virtual void playCard(Player p, Enemy e) { }
    
    // Initializes this card
    protected virtual void Awake()
    {
        Id = -1;
        Name = "Null Card";
        Detail = "Null card.";
        Flavor = "How the heck did this card get generated??";
        Level = 1;
        MemoryCost = 1;
    }
}
