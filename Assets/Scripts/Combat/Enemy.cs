using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Enemy : AbstractEntity
{
    [SerializeField] private float _attack = 10f;
    [SerializeField] private float _armor = 0f;
    [SerializeField] private float _att_modifier = 1f;
    [SerializeField] private float _regen_rate = 0f;
    [SerializeField] private float _exp = 1f;
    // Loot value dictates the amount of the loot after a player kills said enemy. 
    [SerializeField] private int loot = 1;
    static public CombatController _cc;

    public float Attack { get { return _attack; } set { _attack = value; } }
    public float Armor { get { return _armor; } set { _armor = value; } }
    public float Regen_Rate { get { return _regen_rate; } set { _regen_rate = value; } }
    public float Att_Modifier { get { return _att_modifier; } set { _att_modifier = value; } }
    public float Exp { get { return _exp; } set { _exp = value; } }
    public int Loot { get { return loot; } set { loot = value; } }

    // Executes an attack against the player
    public virtual void executeAttack(Player player, string enemyMove = "default move")
    {
        switch(enemyMove)
        {
            case "punch":
                // break;
            case "kick":
                // break;
            default:
                _cc.ChangePlayerHealth(-this.CalculateDamage(player, _attack));
                Camera.main.gameObject.GetComponent<Shake>().ShakeIt();
                break;
        }
    }

    protected override void Awake()
    {
        Attack = 10;
        Armor = 0;
        Regen_Rate = 0;
        Att_Modifier = 1;
        MaxHealth = 50;
        Health = MaxHealth;
        Exp = 1;
        Loot = 1;
        IsAlive = true;
        GetBuffHandler = gameObject.AddComponent<BuffHandler>();
        _cc = GameObject.Find("CombatUtils").GetComponent<CombatController>();
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
            EndCombat();
        }
    }
}