using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
}
