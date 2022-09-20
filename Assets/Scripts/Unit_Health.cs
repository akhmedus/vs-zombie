using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Health : MonoBehaviour, IDamageable 
{
    public float start_Health;
    protected float health;
    protected bool dead;

    [HideInInspector]
    public event Action On_Death;
    protected virtual void Start()
    {
        health = start_Health;
    }

    public virtual void Hit(float dmg, Vector3 hitPnt, Vector3 hitDir)
    { 
        Damage(dmg);
    }

    public virtual void Damage(float dmg) 
    {
        health -= dmg;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    protected void Die()
    {
        dead = true;
        if (On_Death != null) 
        {
            On_Death();
        }
        Destroy(gameObject);
    }
}
