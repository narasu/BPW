using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Weapon
{
    protected override void Shoot()
    {
        base.Shoot();

        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            
        }
    }
}
