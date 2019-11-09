using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : AbstractPlayer
{
    private int userId;
    private string username;
    private DeckManager deckManager;
    private List<Item> inventory;
    private System.Random rand = new System.Random();

    public int UserId { get => userId; set => userId = value; }
    public string Username { get => username; set => username = value; }
    public DeckManager DeckManager { get => deckManager; set => deckManager = value; }
    public List<Item> Inventory { get => inventory; set => inventory = value; }

    public Player()
    {

    }

    public Player(float health, float memory): base(health, memory)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsAlive && InCombat)
        {
            // Loads back to map scene after death
            SceneManager.LoadScene(0);
            endCombat();
        }
    }

    public void addHealth(float restoredHealth)
    {
        Health += restoredHealth;
    }

    // TODO: Add function for UI to execute card
}
