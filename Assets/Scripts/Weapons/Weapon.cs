using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    
    [SerializeField][Tooltip("Total ammo excluding content of mag")]protected int totalAmmo=14; 
    [SerializeField][Tooltip("How many bullets does the mag take")]protected int magSize=7;
    [SerializeField][Tooltip("How much damage does each bullet do")]protected int damage=1;
    [SerializeField][Tooltip("Maximum range of weapon")]protected float weaponRange = 100f;
    
    protected int currentAmmo; // ammo in current mag
    protected bool isEmpty; // is the mag empty?
    protected Camera cam; //variable that holds parent camera (should be player camera)
    protected Vector3 rayHitPosition; 
    [Space(10)][SerializeField] protected Transform hitParticles;
    [SerializeField] protected Transform firingPoint;

    protected WaitForSeconds shotDuration = new WaitForSeconds(0.02f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible

    [SerializeField] protected LineRenderer laserLine;                                        // Reference to the LineRenderer component which will display our laserline
    [SerializeField] protected LayerMask raycastIgnoreLayer;
    protected bool invertMask = false;


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
        if (!isEmpty && currentAmmo>0)
        {
            //Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;

            laserLine.SetPosition(0, firingPoint.position);

            LayerMask newMask = ~(invertMask ? ~raycastIgnoreLayer.value : raycastIgnoreLayer.value);

            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, weaponRange, newMask))
            {
                
                laserLine.SetPosition(1, hit.point);
                Instantiate(hitParticles, hit.point, Quaternion.identity);

                hit.collider.GetComponent<IDamageable>()?.TakeDamage(damage);
            }
            else
            {
                // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
                laserLine.SetPosition(1, rayOrigin + (cam.transform.forward * weaponRange));
            }

            StartCoroutine(ShotEffect());

            if (currentAmmo == 1)
            {
                isEmpty = true;
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

    private IEnumerator ShotEffect()
    {
        // Play the shooting sound effect
        //gunAudio.Play();

        // Turn on line renderer
        laserLine.enabled = true;

        //Wait for .x seconds
        yield return shotDuration;

        // Deactivate line renderer after waiting
        laserLine.enabled = false;
    }
}
