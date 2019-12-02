using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a variation of enemy. Tanky, heavy hitter.
public class VirusHeavy : Enemy
{
    System.Random rand = new System.Random();

    protected override void Awake()
    {
        base.Awake();
        Attack = 30;
        Armor = 0;
        Regen_Rate = 0;
        Att_Modifier = 1;
        MaxHealth = 200;
        Health = MaxHealth;
        Exp = 10;
        Loot = 10;
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
                _cc.ChangePlayerHealth(-this.CalculateDamage(player, rand.Next((int)Attack - 10, (int)Attack) * Att_Modifier));
                break;
        }
    }
}
