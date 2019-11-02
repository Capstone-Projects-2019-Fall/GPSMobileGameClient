using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : AbstractPlayer
{
    private float armor = 0f;
    private float attack = 10f;
    private float regen_rate = 0f;
    private float attack_modifier = 1f;

    public float Armor { get { return armor; } set { armor = value; } }
    public float Attack { get { return attack; } set { attack = value; } }
    public float Regen_Rate { get { return regen_rate; } set { regen_rate = value; } }
    public float Attack_Modifier { get { return attack_modifier; } set { attack_modifier = value; } }

    public Enemy()
    {
    }

    public Enemy(float health, float memory, float armor, float attack, float regen, float modifier_attack) : base(health, memory)
    {
        Armor = armor;
        Attack = attack;
        regen_rate = regen;
        Attack_Modifier = modifier_attack;
    }

    public override void executeAttack(AbstractPlayer entity)
    {
        float damageDealt = Attack * Attack_Modifier;
        entity.damageReceived(damageDealt);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAlive && InCombat)
        {
            // Loads back to map scene after death
            SceneManager.LoadScene(0);
            endCombat();
        }
    }
}
