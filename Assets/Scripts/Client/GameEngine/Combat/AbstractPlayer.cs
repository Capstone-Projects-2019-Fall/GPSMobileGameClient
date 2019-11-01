using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayer : MonoBehaviour
{
    private float health = 100f;
    private float memory = 10f;
    private bool alive = true;
    private bool combat = false;
    
    public float Memory { get => memory; set => memory = value; }
    public float Health { get => health; set => health = value; }
    public bool IsAlive { get => alive; set => alive = value; }
    public bool InCombat { get => combat; set => combat = value; }
    
    public virtual void damageReceived(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            IsAlive = false;
        }
    }
    
    public virtual void startCombat() => InCombat = true;
    public virtual void endCombat() => InCombat = false;
}
