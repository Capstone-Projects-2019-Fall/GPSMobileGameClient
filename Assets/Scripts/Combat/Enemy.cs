using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : AbstractPlayer
{
    private float attack = 10f;
    private float regen_rate = 0f;
    
    public float Attack { get { return attack; } set { attack = value; } }
    public float Regen_Rate { get { return regen_rate; } set { regen_rate = value; } }
    

    public Enemy()
    {
    }

    public Enemy(float health, float memory, float attack, float regen) : base(health, memory)
    {
        Attack = attack;
        regen_rate = regen;
    }

    public void executeAttack(AbstractPlayer entity)
    {
        base.executeAttack(entity, attack);
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
