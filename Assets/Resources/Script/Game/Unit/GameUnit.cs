using System.Collections;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    // [HideInInspector]
    public float hp, shield;
    public string unitName;
    public bool isPlayer;
    public float maxHp, maxShield, shieldRechargeRate, shieldRechargeDeltaTime, speed;
    public float score;

    public GameObject shieldObject;
    public EffectObject deathEffect, collisionEffect;

    private new Rigidbody2D rigidbody;
    private SpriteRenderer sprite_shield;
    private Collider2D collider_shield;
    private float shieldCD;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        gameObject.SetActive(true);
        if (!isPlayer) GlobalData.currentEnemyCount++;

        hp = maxHp;
        shield = maxShield;

        rigidbody = GetComponent<Rigidbody2D>();

        if (shieldObject)
        {
            sprite_shield = shieldObject.GetComponent<SpriteRenderer>();
            collider_shield = shieldObject.GetComponent<Collider2D>();


            if (maxShield == 0)
            {
                ColorUtil.SetColorAlpha(sprite_shield, 0);
                collider_shield.enabled = false;
            }
        }

        StartCoroutine(CheckDeath());
        StartCoroutine(RechargeShield());
    }

    private void Update()
    {
        if (GlobalData.isGameOver) return;

        if (maxShield != 0)
            ColorUtil.SetColorAlpha(sprite_shield, shield / maxShield);

        if (shieldObject)
            if (shield == 0) collider_shield.enabled = false; else collider_shield.enabled = true;

        if (!isPlayer)
        {
            if (Vector2.Distance(transform.position, GlobalData.player.position) > GlobalData.maxDistanceToPlayer)
            {
                hp = 0;
            }
        }
    }

    public float GetHPInPercent()
    {
        return hp / maxHp;
    }

    public float GetShieldInPercent()
    {
        if (maxShield == 0) return 0;
        else return shield / maxShield;
    }

    protected virtual void OnDeath()
    {
        StopAllCoroutines();

        var i = EffectPool.Get(deathEffect.effName, deathEffect);
        i.transform.position = transform.position;
        i.Init();

        if (!isPlayer)
        {
            if (!GlobalData.isGameOver)
                GlobalData.score += score;
            GlobalData.currentEnemyCount--;
        }
        else
        {
            GlobalData.isGameOver = true;
        }

        UnitPool.Release(unitName, this);
    }

    public void TakeDamage(float damage)
    {
        damage = Mathf.Round(damage);

        shieldCD = shieldRechargeDeltaTime;
        if (shield >= damage)
        {
            shield -= damage;
        }
        else if (shield > 0)
        {
            shield = 0;
        }
        else
        {
            hp -= damage;
        }
    }

    private IEnumerator CheckDeath()
    {
        yield return new WaitUntil(() => hp <= 0);
        OnDeath();
    }

    private IEnumerator RechargeShield()
    {
        while (hp > 0)
        {
            yield return 0;
            if (shieldCD <= 0)
            {
                shield += shieldRechargeRate * Time.deltaTime;
                if (shield >= maxShield) shield = maxShield;
            }
            else
            {
                shieldCD -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        float damage = (rigidbody.velocity * rigidbody.mass - other.rigidbody.velocity * other.rigidbody.mass).magnitude * GlobalData.collisionDamageFactor;

        TakeDamage(damage);
        var i = EffectPool.Get(collisionEffect.effName, collisionEffect);
        i.transform.position = other.contacts[0].point;
        i.transform.rotation = transform.rotation;
        i.Init();

    }
}
