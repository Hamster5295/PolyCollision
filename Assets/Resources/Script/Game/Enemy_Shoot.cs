using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    public Ammo ammo;
    public Vector2 point;
    public float interval;

    private float cd;
    private Collider2D col_self, col_shield;

    private void Start()
    {
        col_self = GetComponent<Collider2D>();
        col_shield = transform.Find("Shield").GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!GlobalData.player) return;

        cd -= Time.deltaTime;
        if (cd < 0)
        {
            var a = AmmoPool.Get(ammo.ammoName, ammo);
            a.transform.position = transform.TransformPoint(point);
            a.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, GlobalData.player.position - transform.position));
            a.ignoreColliders.Clear();
            a.ignoreColliders.Add(col_self);
            a.ignoreColliders.Add(col_shield);
            a.Init();
            cd = interval;
        }
    }
}
