using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayer : MonoBehaviour
{
    private float health = 100f;
    private float memory = 10f;
    private bool alive = true;
    private bool combat = false;
    private BuffHandler buffHandler = new BuffHandler();

    public float Memory { get => memory; set => memory = value; }
    public float Health { get => health; set => health = value; }
    public bool IsAlive { get => alive; set => alive = value; }
    public bool InCombat { get => combat; set => combat = value; }
    public List<Buff> GetBuffList { get => buffHandler.buffList; }

    public AbstractPlayer()
    {

    }

    public AbstractPlayer(float health, float memory)
    {
        Health = health;
        Memory = memory;
        IsAlive = true;
    }

    public virtual void executeAttack(AbstractPlayer entity, float attack_damage)
    {
        float attackModifier = buffHandler.calculateAttackModifier();
        entity.damageReceived(attack_damage * attackModifier);
    }

    public virtual void damageReceived(float damage)
    {
        float defenseModifier = buffHandler.calculateDefenseModifier();
        Health -= damage * defenseModifier;
        if (Health <= 0)
        {
            IsAlive = false;
        }
    }

    public virtual void buffReceived(Buff buff)
    {
        buffHandler.addBuff(buff);
    }

    public virtual void buffRemoved(Buff buff)
    {
        buffHandler.removeBuff(buff);
    }

    public virtual void startCombat() => InCombat = true;
    public virtual void endCombat() => InCombat = false;
}
