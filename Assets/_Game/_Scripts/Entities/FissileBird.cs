using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FissileBird : Bird
{
    private Rigidbody2D child0;
    private Rigidbody2D child1;

    protected override void Start()
    {
        base.Start();

        child0 = transform.GetChild(0).GetComponent<Rigidbody2D>();
        child1 = transform.GetChild(1).GetComponent<Rigidbody2D>();

        child0.gameObject.SetActive(false);
        child1.gameObject.SetActive(false);

    }
    protected override void OnSecondClickAction()
    {
        child0.gameObject.SetActive(true);
        child1.gameObject.SetActive(true);

        child0.transform.parent = null;
        child1.transform.parent = null;

        child0.velocity = rb.velocity - (Vector2)child0.transform.up * 2;
        child1.velocity = rb.velocity + (Vector2)child0.transform.up * 2;

        base.OnSecondClickAction();
    }
}
