using System;
using System.Collections;
using System.Collections.Generic;
using LuLib.Transform;
using UnityEngine;
using Zenject;

public abstract class EnemyController : MonoBehaviour
{
    // specs
    protected abstract float MaxHealth { get; }

    // vars
    protected float Health;
    protected bool Dead;

    protected void Awake()
    {
        Health = MaxHealth;
    }

    public void Damage(float damage)
    {
        // remove health
        Health -= damage;

        // check if dead
        if (Health <= 0)
        {
            Dead = true;
            OnDeath();
        }
    }

    public abstract void OnDeath();
}
