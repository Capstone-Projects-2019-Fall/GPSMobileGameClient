using System;
using UnityEngine;
using UnityEngine.Assertions;

/* TurnTimer Description:
 * The TurnTimer is a simple utility object used within the context of the TurnSystem to signal when a player's turn ends
 * TurnTimer has a simple event system associated with it that can notify subscribers when its timer expires.
 */
[RequireComponent(typeof(CombatController))]
public class TurnTimer : Singleton<TurnTimer>
{
    public const float TURN_DURATION = 20.0f; // Maximum number of seconds per turn
    public event EventHandler TimeExpired;

    private float _timeRemaining; // Amount of time remaining on a given turn
    private bool _pauseTimer;

    private CombatController _cc;

    public float TimeRemaining {
        get => _timeRemaining;
    }

    // Called at the start of each player's Action Phase
    public void StartTimer()
    {
        _timeRemaining = TURN_DURATION;
        _pauseTimer = false;
        InvokeRepeating("DecreaseTimeRemaining", 0.1f, 0.1f);
    }

    private void Awake()
    {
        _cc = gameObject.GetComponent<CombatController>();
        Assert.IsNotNull(_cc);
    }

    // Called by via InvokeRepeating as a coroutine to reduce the time remaining on a given turn
    // Doing it this way instead of the normal Unity update & deltatime method will afford additional flexibility in future
    private void DecreaseTimeRemaining()
    {
        if (!_pauseTimer) // If the timer is not paused
        {
            _timeRemaining -= 0.1f;
            if (_timeRemaining < 0.1f)
            {
                CancelInvoke("DecreaseTimeRemaining");
                OnTimeExpired(EventArgs.Empty); // Signal to subscribers
                _pauseTimer = true;
            }
        }
    }

    // Signal to subscribers that the current turn timer has expired
    public void OnTimeExpired(EventArgs e)
    {
        TimeExpired?.Invoke(this, e);
    }
}
