using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHPBar : MonoBehaviour
{
    public GameUnit unit;
    public float trackSpeed = 0.1f;

    private float maxScale;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, unit.GetHPInPercent() * originalScale, trackSpeed);
    }
}
