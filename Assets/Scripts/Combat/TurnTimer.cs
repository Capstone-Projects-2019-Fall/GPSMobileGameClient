using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTimer : Singleton<TurnTimer>
{
    public const float TURN_DURATION = 20.0f; // Maximum number of seconds per turn
    private float _timeRemaining; // Amount of time remaining on a given turn

    private CombatController ccont;


    public void StartTimer()
    {
        _timeRemaining = TURN_DURATION;
        InvokeRepeating("DecreaseTimeRemaining", 0.1f, 0.1f);
    }

    private void Awake()
    {
        ccont = gameObject.GetComponent<CombatController>();
    }

    
    
    // Update is called once per frame
    private void Update()
    {
        
    }
    
    // Called by via InvokeRepeating as a coroutine to reduce the time remaining on a given turn
    // Doing it this way instead of the normal Unity update & deltatime method will afford additional flexibility in future
    private void DecreaseTimeRemaining()
    {
        _timeRemaining -= 0.1f;
        if(_timeRemaining < 0.1f)
        {
            // TODO: Fire off turn end event
        }
    }
}
