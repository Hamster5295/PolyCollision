using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Simple : MonoBehaviour
{
    public float initialForceFactor, initialAngularFactor;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GlobalData.player)
        {
            rb.AddForce(initialForceFactor * Vector2.ClampMagnitude(GlobalData.player.position - transform.position, 1) * Time.deltaTime);
            rb.AddTorque(initialAngularFactor * Time.deltaTime);
        }
    }
}
