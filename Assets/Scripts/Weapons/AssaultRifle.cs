using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon
{
    [SerializeField] private float fireRate = 0.5f;
    private float fireTimer = 0f;
    private bool canFire; //does fire rate allow for firing
    protected override void Update()
    {
        base.Update();

        if (!isEmpty && canFire && Input.GetMouseButton(0))
        {
            Shoot();
            canFire=false;
        }
        
        if (!canFire)
        {
            if (fireTimer<fireRate)
            {
                fireTimer+=Time.deltaTime;
            }
            else
            {
                canFire=true;
                fireTimer=0f;
            }
        }

    }


}
