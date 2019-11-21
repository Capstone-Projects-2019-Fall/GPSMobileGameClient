using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// This class is the Player component of the gameObject.
public class Player : AbstractEntity
{
    private int userId;
    private string username;
    private int level;
    private int currentExp;
    private int gold;
    private DeckManager deckManager;
    private List<Item> inventory;
    private System.Random rand = new System.Random();
    private BuffHandler _buffHandler;

    #region Accessors -----------------------------------------------------------------------------------

    public int UserId { get => userId; set => userId = value; }
    public string Username { get => username; set => username = value; }
    public int Level { get => level; set => level = value; }
    public int CurrentExp { get => currentExp; set => currentExp = value; }
    public int Gold { get => gold; set => gold = value; }
    public DeckManager DeckManager { get => deckManager; set => deckManager = value; }
    public List<Item> Inventory { get => inventory; set => inventory = value; }
    public BuffHandler BuffHandler { get => _buffHandler; set => _buffHandler = value; }

    #endregion ------------------------------------------------------------------------------------------

    // Initializes the player with the default stats of AbstractEntity.
    protected override void Awake()
    {
        base.Awake();
        Level = 1;
        CurrentExp = 0;
        Gold = 0;
        _buffHandler = gameObject.GetComponent<BuffHandler>();
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
    public void addHealth(float restoredHealth)
    {
        Health += restoredHealth;
    }

    /* Executes an attack against another entity.
     * Need to override because the reference defined in AbstractEntity will throw a NullRef
     */
    public override void executeAttack(AbstractEntity entity, float attack_damage)
    {
        float attackModifier = _buffHandler.calculateAttackModifier();
        entity.damageReceived(attack_damage * attackModifier);
    }

    // Initializes the player
    private void InitializePlayer()
    {

    }
    
}