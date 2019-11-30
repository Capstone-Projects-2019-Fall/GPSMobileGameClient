using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbstractEntity : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    private bool _alive = true;
    private bool _combat = false;
    private BuffHandler _buffHandler;

    public float Health {
        get => _health;
        set{
             _health = value;
             if (_health <= 0)
            {
                IsAlive = false;
            }
             HealthEventArgs args = new HealthEventArgs { Health = _health };
            OnHealthChanged(args);            
        } 
    }
    public bool IsAlive { get => _alive; set => _alive = value; }
    public bool InCombat { get => _combat; set => _combat = value; }
    public List<Buff> GetBuffList { get => _buffHandler.buffList; set => _buffHandler.buffList = value; }
    public BuffHandler GetBuffHandler { get => _buffHandler; set => _buffHandler = value; }
    public event EventHandler<HealthEventArgs> HealthChanged;

    public void OnHealthChanged(HealthEventArgs e)
    {
        HealthChanged?.Invoke(this, e);
    }

    // Initializes an abstract player.
    protected virtual void Awake()
    {
        Health = _health;
        IsAlive = true;
        _buffHandler = gameObject.AddComponent<BuffHandler>();
    }

    // Executes an attack against another entity.
    public virtual void ExecuteAttack(AbstractEntity entity, float attack_damage)
    {
        float attackModifier = _buffHandler.calculateAttackModifier();
        entity.DamageReceived(attack_damage * attackModifier);
    }
    
    // Receives damage.
    public virtual void DamageReceived(float damage)
    {
        float defenseModifier = _buffHandler.calculateDefenseModifier();
        float totalDamage = damage * defenseModifier;
        if(this is Enemy)
        {
            Delta.AddDamage(totalDamage);
        }        
        Health -= totalDamage;        
    }

    // Adds a buff to the entity
    public virtual void BuffReceived(Buff buff)
    {
        _buffHandler.addBuff(buff);
    }

    // Removes a buff from the entity.
    public virtual void BuffRemoved(Buff buff)
    {
        _buffHandler.removeBuff(buff);
    }

    // These handle the start and end of combat
    public virtual void StartCombat() => InCombat = true;
    public virtual void EndCombat() => InCombat = false;


    private void Start()
    {
        _buffHandler = new BuffHandler();
    }
}