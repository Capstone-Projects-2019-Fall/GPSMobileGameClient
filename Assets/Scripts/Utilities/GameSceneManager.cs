using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Handles all scene management
public class GameSceneManager : AbstractSceneManager
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Enemy tapped event.
    public override void enemyTapped(GameObject enemy)
    {
        SceneManager.LoadScene("CombatInstance");
    }
}
