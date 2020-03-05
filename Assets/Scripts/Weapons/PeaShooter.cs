using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Weapon
{
    
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    
    protected override void Start()
    {
        base.Start();
    }
    
    protected override void Shoot()
    {
        base.Shoot();
        Instantiate(bullet, spawnPoint.position, transform.rotation);
    }
}
