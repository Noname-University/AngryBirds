using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour,IDestroyable
{
    [SerializeField]
    private float health;


    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Hit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy();    
        }
    }

}
