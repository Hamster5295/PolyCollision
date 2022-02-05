using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBtn : HBtn_Event
{
    public PlayerFire fire;

    public override void OnPointerDown()
    {
        fire.isFiring = true;
    }

    public override void OnPointerUp()
    {
        fire.isFiring = false;
    }
}
