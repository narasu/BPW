using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    [SerializeField][Tooltip("Total ammo excluding content of mag")]protected int totalAmmo=0; 
    [SerializeField][Tooltip("How many bullets does the mag take")]protected int magSize=0;
    [SerializeField][Tooltip("How much damage does each bullet do")]protected int damage=0;
    protected int currentAmmo; // ammo in current mag
    protected bool isEmpty; // is the mag empty?
    
    protected Camera cam;
    protected Vector3 rayHitPosition;
    
    protected virtual void Start()
    {
        //Weapon starts with an empty mag and will immediately reload
        isEmpty=true;
        currentAmmo=0;
        Reload();

        cam = GetComponentInParent<Camera>();
    }

    protected virtual void Update()
    {
        if (!isEmpty && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    protected virtual void Shoot()
    {
        Debug.Log("current ammo: " + currentAmmo);
        if (currentAmmo>0)
        {
            if (currentAmmo==1)
            {
                isEmpty=true;
            }
            currentAmmo--;
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

    protected virtual void Aim()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            rayHitPosition = hit.point;
        }
    }
}
