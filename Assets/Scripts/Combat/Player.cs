using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// This class is the Player component of the gameObject.
public class Player : AbstractEntity
{
    private int _memory; 
    private int _maxMemory;
    private int _userId;
    private string _username;
    private int _level;
    private int _currentExp;
    private double _gold;
    private DeckManager _deckManager;
    private List<Item> _inventory;

    #region Accessors -----------------------------------------------------------------------------------

    public int Memory { get => _memory; set => _memory = value; }
    public int MaxMemory { get => _maxMemory; set => _maxMemory = value; }
    public int UserId { get => _userId; set => _userId = value; }
    public string Username { get => _username; set => _username = value; }
    public int Level { get => _level; set => _level = value; }
    public int CurrentExp { get => _currentExp; set => _currentExp = value; }
    public double Gold { get => _gold; set => _gold = value; }
    public DeckManager DeckManager { get => _deckManager; set => _deckManager = value; }
    public List<Item> Inventory { get => _inventory; set => _inventory = value; }

    #endregion ------------------------------------------------------------------------------------------

    // Initializes the player with the default stats of AbstractEntity.
    protected override void Awake()
    {
        base.Awake();
        _maxMemory = 10;
        _memory = _maxMemory;
        _level = 1;
        _currentExp = 0;
        _gold = 0;


        // TODO: Call server to get player values?
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Adds health to the player
    public void AddHealth(float restoredHealth)
    {
        Health = Mathf.Min(MaxHealth, Health + restoredHealth);
    }

    /* Executes an attack against another entity.
     * Need to override because the reference defined in AbstractEntity will throw a NullRef
     */
    public override void ExecuteAttack(AbstractEntity entity, float attack_damage)
    {
        float attackModifier = GetBuffHandler.calculateAttackModifier();
        entity.DamageReceived   (attack_damage * attackModifier);
    }

    // Initializes the player
    private void InitializePlayer()
    {

    }
    
}