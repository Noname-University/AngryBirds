using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IDestroyable
{
    [SerializeField]
    private float health;

    private int obstacleIndex = 0;

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Hit(float damage)
    {
        health -= damage;
        if (health <= 7.5 && health > 5)
        {
            transform.GetChild(obstacleIndex).gameObject.SetActive(true);
        }
        else if (health <= 5 && health > 2.5)
        {
            transform.GetChild(obstacleIndex).gameObject.SetActive(true);
        }
        else if (health <= 2.5 && health > 0)
        {
            transform.GetChild(obstacleIndex).gameObject.SetActive(true);
        }
        else if (health <= 0)
        {
            Destroy();
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
