﻿/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : AbstractPlayer
{
    private int userId;
    private string username;
    private List<Card> cards;
    private List<Card> nonexhaustedCards;
    private List<Item> inventory;
    private System.Random rand = new System.Random();

    public int UserId { get => userId; set => userId = value; }
    public string Username { get => username; set => username = value; }
    public List<Card> Cards { get => cards; set => cards = value; }
    public List<Card> NonexhaustedCards { get => nonexhaustedCards; set => nonexhaustedCards = value; }
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

    public List<Card> DrawCards(int numberOfCards = 5)
    {
        List<Card> hand = new List<Card>();
        for (int i = 0; i < numberOfCards; i++)
        {
            int index = rand.Next(0, nonexhaustedCards.Count);
            hand.Add(nonexhaustedCards[index]);
            nonexhaustedCards.RemoveAt(index);
        }
        return hand;
    }
}


*/