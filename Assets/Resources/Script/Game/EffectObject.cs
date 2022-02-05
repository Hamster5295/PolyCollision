using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public string effName;
    public float length;
    public bool useAnimationLength;
    public AnimationClip ani;

    private float cd;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        cd -= Time.deltaTime;
        if (cd < 0)
        {
            gameObject.SetActive(false);
            EffectPool.Release(effName, this);
        }
    }

    public void Init()
    {
        gameObject.SetActive(true);
        cd = useAnimationLength ? ani.length : length;
    }
}
