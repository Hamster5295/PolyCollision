using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFire : MonoBehaviour
{
    [HideInInspector]
    public bool isFiring = false;
    // [HideInInspector]
    public int gunLevel = 1, missileLevel = 1;

    public Ammo gunAmmo, missileAmmo;
    public List<GunFirePosition> gunPositions;

    private int gunFireAt = 0;
    private float gunCD = 0, missileCD = 0;
    private Collider2D col_self, col_shield;
    private Dictionary<int, Vector2> mGunPositions = new Dictionary<int, Vector2>();

    private void Start()
    {
        GlobalData.player = transform;

        col_self = GetComponent<Collider2D>();
        col_shield = GetComponent<GameUnit>().shieldObject.GetComponent<Collider2D>();

        foreach (var item in gunPositions)
        {
            mGunPositions.Add(item.index, item.pos.position);
        }
    }

    private void Update()
    {

        //冷却
        if (gunCD > 0) gunCD -= Time.deltaTime;
        if (missileCD > 0) missileCD -= Time.deltaTime;

        if (!isFiring) return;
        if (gunCD <= 0)
        {
            CreateGunAmmo();
            gunCD = GetGunCD();

            gunFireAt++;
            if (gunFireAt > GetGunFirePositions().Count - 1) gunFireAt = 0;
        }
    }

    private float GetGunCD()
    {
        switch (gunLevel)
        {
            case 1: return 0.25f;
            case 2: return 0.15f;
            case 3: return 0.1f;

            default:
                Debug.LogError("错误: Gun等级不对");
                return 100;
        }
    }

    private List<GunFirePosition> GetGunFirePositions()
    {
        return gunPositions.GetRange(0, gunLevel + 1);
    }

    private void CreateGunAmmo()
    {
        var ammo = AmmoPool.Get(gunAmmo.ammoName, gunAmmo);
        ammo.transform.position = transform.TransformPoint(mGunPositions[gunFireAt]);
        ammo.transform.rotation = transform.rotation;
        ammo.ignoreColliders.Clear();
        ammo.ignoreColliders.Add(col_self);
        ammo.ignoreColliders.Add(col_shield);
        ammo.Init();
    }
}

[Serializable]
public struct GunFirePosition
{
    public int index;
    public Transform pos;
}
