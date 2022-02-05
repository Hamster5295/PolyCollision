using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [HideInInspector]
    public List<Collider2D> ignoreColliders;

    public string ammoName;
    public float damage, speed, rotateSpeed, lifeTime, forceFactor = 1;
    public TrailRenderer trail;
    public EffectObject effect;

    private bool isAlive = false;
    private float life;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (!isAlive) return;
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);

        life -= Time.deltaTime;
        if (life <= 0)
        {
            isAlive = false;
            CreateEffect();
            AmmoPool.Release(ammoName, this);
        }
    }

    //Call after initializing position
    public void Init()
    {
        life = lifeTime;
        isAlive = true;
        gameObject.SetActive(true);
        if (trail)
        {
            trail.Clear();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAlive) return;
        if (ignoreColliders.Contains(other)) return;

        isAlive = false;

        var unit = other.GetComponent<GameUnit>();
        if (unit)
        {
            unit.TakeDamage(damage);
            other.GetComponent<Rigidbody2D>().AddForceAtPosition(transform.TransformVector(Vector2.up * speed) * forceFactor, transform.position);
        }
        else
        {
            unit = other.transform.parent.GetComponent<GameUnit>();
            if (unit)
            {
                unit.TakeDamage(damage);
                unit.GetComponent<Rigidbody2D>().AddForceAtPosition(transform.TransformVector(Vector2.up * speed) * forceFactor, transform.position);
            }
        }
        CreateEffect();
        AmmoPool.Release(ammoName, this);
    }

    private void CreateEffect()
    {
        var i = EffectPool.Get(effect.effName, effect);
        i.transform.position = transform.position;
        i.Init();
    }
}
