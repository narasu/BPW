using UnityEngine;

public class AssaultRifle : Weapon
{
    [Space(10)][SerializeField][Tooltip("How often can this weapon fire (rate in seconds)")] private float fireRate = 0.5f;
    private float fireTimer = 0f;
    private bool canFire; //does fire rate timer currently allow for firing
    protected override void Update()
    {
        base.Update();

        if (canFire && Input.GetMouseButton(0))
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
