using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestroyableBase : MonoBehaviour,IDestroyable
{
    [SerializeField]
    protected float health;

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    public virtual void Hit(float damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Destroy();
        }
    }
}
