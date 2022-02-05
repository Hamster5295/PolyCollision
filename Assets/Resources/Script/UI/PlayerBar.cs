using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBar : MonoBehaviour
{
    public enum BarTrace { HP, Shield }
    public BarTrace trace;
    public GameUnit player;

    private HProgressBar bar;

    private void Start()
    {
        bar = GetComponent<HProgressBar>();
    }

    private void Update()
    {
        if (!player) return;
        bar.SetValue(trace == BarTrace.HP ? player.GetHPInPercent() : player.GetShieldInPercent());
    }
}
