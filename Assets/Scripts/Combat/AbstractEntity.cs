using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float memory = 10f;
    private bool alive = true;
    private bool combat = false;
    private BuffHandler buffHandler;

    public float Memory { get => memory; set => memory = value; }
    public float Health { get => health; set => health = value; }
    public bool IsAlive { get => alive; set => alive = value; }
    public bool InCombat { get => combat; set => combat = value; }
    public List<Buff> GetBuffList { get => buffHandler.buffList; set => buffHandler.buffList = value; }
    public BuffHandler GetBuffHandler { get => buffHandler; set => buffHandler = value; }

    // Initializes an abstract player.
    protected virtual void Awake()
    {
        Health = health;
        Memory = memory;
        IsAlive = true;
        buffHandler = gameObject.AddComponent<BuffHandler>();
    }

    // Executes an attack against another entity.
    public virtual void executeAttack(AbstractEntity entity, float attack_damage)
    {
        float attackModifier = buffHandler.calculateAttackModifier();
        entity.damageReceived(attack_damage * attackModifier);
    }
    
    // Receives damage.
    public virtual void damageReceived(float damage)
    {
        float defenseModifier = buffHandler.calculateDefenseModifier();
        Health -= damage * defenseModifier;
        if (Health <= 0)
        {
            IsAlive = false;
        }
    }

    // Adds a buff to the entity
    public virtual void buffReceived(Buff buff)
    {
        buffHandler.addBuff(buff);
    }

    // Removes a buff from the entity.
    public virtual void buffRemoved(Buff buff)
    {
        buffHandler.removeBuff(buff);
    }

    // These handle the start and end of combat
    public virtual void startCombat() => InCombat = true;
    public virtual void endCombat() => InCombat = false;


    private void Start()
    {
        buffHandler = new BuffHandler();
    }
}