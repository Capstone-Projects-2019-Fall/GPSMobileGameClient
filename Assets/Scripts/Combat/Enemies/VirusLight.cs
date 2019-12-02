using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a variation of enemy. Squishy, light hitter.
public class VirusLight : Enemy
{
    System.Random rand = new System.Random();

    protected override void Awake()
    {
        base.Awake();
        Attack = 5;
        Armor = 0;
        Regen_Rate = 0;
        Att_Modifier = 1;
        MaxHealth = 30;
        Health = MaxHealth;
        Exp = 2;
        Loot = 20;
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
    public override void executeAttack(Player player, string enemyMove = "default move")
    {
        switch(enemyMove)
        {
            case "punch":
                // break;
            case "kick":
                // break;
            default:
                _cc.ChangePlayerHealth(-this.CalculateDamage(player, rand.Next((int)Attack, (int)Attack + 5) * Att_Modifier));
                break;
        }        
    }
}
