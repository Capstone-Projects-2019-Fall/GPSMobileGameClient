﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : AbstractPlayer
{

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
}