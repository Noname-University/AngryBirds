using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBird : Bird
{
    [SerializeField]
    private float radius;

    [SerializeField]
    private float explotionForce;

    [SerializeField]
    private LayerMask layerToHit;


    protected override void OnSecondClickAction()
    {
        Explode();
        base.OnSecondClickAction();
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerToHit);
        foreach (Collider2D collider in colliders)
        {
            Vector2 direction = collider.transform.position - transform.position;
            collider.GetComponent<Rigidbody2D>().AddForce(direction * explotionForce * Vector2.Distance(transform.position, collider.transform.position));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
