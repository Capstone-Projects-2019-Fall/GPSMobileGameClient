using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    private bool _alive = true;
    private bool _combat = false;
    private BuffHandler _buffHandler;

    public float Health { get => _health; set => _health = value; }
    public bool IsAlive { get => _alive; set => _alive = value; }
    public bool InCombat { get => _combat; set => _combat = value; }
    public List<Buff> GetBuffList { get => _buffHandler.buffList; set => _buffHandler.buffList = value; }
    public BuffHandler GetBuffHandler { get => _buffHandler; set => _buffHandler = value; }

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
        Health -= damage * defenseModifier;
        if (Health <= 0)
        {
            IsAlive = false;
        }
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