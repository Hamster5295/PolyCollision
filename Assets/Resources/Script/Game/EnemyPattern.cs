using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern : MonoBehaviour
{
    public List<EnemyPatternInfo> enemies;
}

[System.Serializable]
public struct EnemyPatternInfo
{
    public GameUnit enemy;
    public Transform pos;
}
