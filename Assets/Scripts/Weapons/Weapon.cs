using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    
    [SerializeField][Tooltip("Total ammo excluding content of mag")]protected int totalAmmo=14; 
    [SerializeField][Tooltip("How many bullets does the mag take")]protected int magSize=7;
    [SerializeField][Tooltip("How much damage does each bullet do")]protected int damage=1;
    [Space]
    protected int currentAmmo; // ammo in current mag
    protected bool isEmpty; // is the mag empty?
    
    protected Camera cam;
    protected Vector3 rayHitPosition;
    [SerializeField] protected Transform hitParticles;
    
    
    protected virtual void Start()
    {
        //Weapon starts with an empty mag and will immediately reload
        isEmpty=true;
        currentAmmo=0;
        Reload();

        //camera component is used to handle raycast targeting
        cam = GetComponentInParent<Camera>();
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    protected virtual void Shoot()
    {
        Debug.Log("current ammo: " + currentAmmo);
        
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (currentAmmo>0)
            {
                Instantiate(hitParticles, hit.point, Quaternion.identity);

                hit.collider.GetComponent<IDamageable>()?.TakeDamage(damage);

                if (currentAmmo==1)
                {
                    isEmpty=true;
                }
                currentAmmo--;
            }
        }
    }

    protected virtual void Reload()
    {
        int reloadCount = magSize - currentAmmo;

        for (int i=0; i<reloadCount; i++)
        {
            if (totalAmmo<=0)
            {
                break;
            }

            currentAmmo++;
            totalAmmo--;
            isEmpty=false;
        }
    }
}
