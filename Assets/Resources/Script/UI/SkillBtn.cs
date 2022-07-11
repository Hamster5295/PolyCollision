using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : HBtn_Event
{
    public Transform player;
    public float cd, radius, force;
    public Image cdBar;
    public EffectObject eff_explode;

    private float timer = 0;
    private bool isReady = true;
    private GameUnit playerUnit;

    private void Start()
    {
        playerUnit = player.GetComponent<GameUnit>();
    }

    private void Update()
    {
        if (isReady) return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
            isReady = true;
        }

        cdBar.fillAmount = (timer / cd);
    }

    public override void OnClick()
    {
        if (!isReady) return;
        if (!player) return;

        timer = cd;
        isReady = false;
        Explode_Perform();
    }

    private void Explode_Perform()
    {
        EffectPool.Get(eff_explode.effName, eff_explode).transform.position = player.position;
        foreach (var i in Physics2D.OverlapCircleAll(player.position, radius))
        {
            var rb = i.GetComponent<Rigidbody2D>();
            var unit = i.GetComponent<GameUnit>();
            if (rb)
            {
                rb.AddForce((i.transform.position - player.position).normalized * force);
            }
            if (unit)
            {
                if (unit != playerUnit)
                    unit.TakeDamage(10);
            }
        }
    }
}