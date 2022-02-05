using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyPattern> enemyPatterns;
    public float interval;

    private void Start()
    {
        StartCoroutine(Summon());
    }

    private IEnumerator Summon()
    {
        float cd = 0;

        while (!GlobalData.isGameOver)
        {
            yield return 0;
            cd -= Time.deltaTime;
            if (cd <= 0)
            {
                cd = interval;
                SummonEnemy();
                continue;
            }

            if (GlobalData.currentEnemyCount <= 0)
            {
                SummonEnemy();
                continue;
            }
        }
    }

    private void SummonEnemy()
    {
        var p = enemyPatterns[Random.Range(0, enemyPatterns.Count)].enemies;

        foreach (var item in p)
        {
            var e = UnitPool.Get(item.enemy.unitName, item.enemy);
            e.transform.position = item.pos.position + GlobalData.player.position;
            e.transform.localScale = item.pos.localScale;
            e.transform.eulerAngles = item.pos.eulerAngles;

            e.Init();
        }
    }
}