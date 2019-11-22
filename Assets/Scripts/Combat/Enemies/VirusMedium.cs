using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a variation of enemy. Medium Brawler
public class VirusMedium : Enemy
{
    System.Random rand = new System.Random();

    protected override void Awake()
    {
        base.Awake();
        Attack = 8;
        Armor = 0;
        Regen_Rate = 0;
        Att_Modifier = 1;
        Health = 75;
        Exp = 5;
        Loot = 6;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Attacks the player
    public override void executeAttack(Player player)
    {
        
        player.DamageReceived(rand.Next((int)Attack - 3, (int)Attack + 3) * Att_Modifier);
    }
}
