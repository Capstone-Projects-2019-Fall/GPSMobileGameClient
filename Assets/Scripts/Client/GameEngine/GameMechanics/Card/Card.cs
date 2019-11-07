using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string name;
    private int level;
    private int memoryCost;
    private int pp;
    private float attack;
    private Buff buff;

    public string Name { get => name; set => name = value; }
    public int Level { get => level; set => level = value; }
    public int MemoryCost { get => memoryCost; set => memoryCost = value; }
    public int PP { get => pp; set => pp = value; }
    private float Attack { get => attack; set => attack = value; }
    public Buff CardBuff{get => buff; set => buff = value;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
