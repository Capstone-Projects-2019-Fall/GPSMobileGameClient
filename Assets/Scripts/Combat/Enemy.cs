using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Enemy : AbstractEntity
{
    [SerializeField] private float attack = 10f;
    [SerializeField] private float armor = 0f;
    [SerializeField] private float att_modifier = 1f;
    [SerializeField] private float regen_rate = 0f;
    [SerializeField] private float exp = 1f;
    // Loot value dictates the amount of the loot after a player kills said enemy. 
    [SerializeField] private int loot = 1;

    public float Attack { get { return attack; } set { attack = value; } }
    public float Armor { get { return armor; } set { armor = value; } }
    public float Regen_Rate { get { return regen_rate; } set { regen_rate = value; } }
    public float Att_Modifier { get { return att_modifier; } set { att_modifier = value; } }
    public float Exp { get { return exp; } set { exp = value; } }
    public int Loot { get { return loot; } set { loot = value; } }

    // Executes an attack against the player
    public virtual void executeAttack(Player player)
    {
        base.executeAttack(player, attack);
    }

    protected override void Awake()
    {
        Attack = 10;
        Armor = 0;
        Regen_Rate = 0;
        Att_Modifier = 1;
        Health = 50;
        Memory = 0;
        Exp = 1;
        Loot = 1;
        IsAlive = true;
        GetBuffHandler = gameObject.AddComponent<BuffHandler>();
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