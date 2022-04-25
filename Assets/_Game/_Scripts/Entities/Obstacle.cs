using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : DestroyableBase
{
    private Rigidbody2D rb;

    private void Start() 
    {
        rb= GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var destroyable = other.gameObject.GetComponent<Enemy>();
        if (destroyable != null)
        {
            destroyable.Hit(rb.velocity.magnitude);
        }
    }

    public override void Hit(float damage)
    {
        base.Hit(damage);
        if (health <= 7.5 && health > 5)
        {
            CloseAllSprites();
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (health <= 5 && health > 2.5)
        {
            CloseAllSprites();
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (health <= 2.5 && health > 0)
        {
            CloseAllSprites();
            transform.GetChild(3).gameObject.SetActive(true);
        }

    }

    private void CloseAllSprites()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
    }



}
