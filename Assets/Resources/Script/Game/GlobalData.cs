using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public const float collisionDamageFactor = 3;
    public const float maxDistanceToPlayer = Mathf.Infinity;
    public static float score = 0;  //TODO: 启动关卡的时候清零
    public static Transform player;
    public static int currentEnemyCount;
    public static bool isGameOver = false;
}
