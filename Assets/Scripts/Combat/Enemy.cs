using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : AbstractPlayer
{
    [SerializeField] private float attack = 10f;
    [SerializeField] private float armor = 0f;
    [SerializeField] private float att_modifier = 1f;
    [SerializeField] private float regen_rate = 0f;
    
    public float Attack { get { return attack; } set { attack = value; } }
    public float Armor { get { return armor; } set { armor = value; } }
    public float Regen_Rate { get { return regen_rate; } set { regen_rate = value; } }
    public float Att_Modifier { get { return att_modifier; } set { att_modifier = value; } }
    
    public Enemy()
    {
    }

    public Enemy(float health, float memory, float attack, float armor,float regen, float att_modifier) : base(health, memory)
    {
        Attack = attack;
        Armor = armor;
        Regen_Rate = regen;
        Att_Modifier = att_modifier;
    }

    public void executeAttack(AbstractPlayer entity)
    {
        base.executeAttack(entity, attack);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
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