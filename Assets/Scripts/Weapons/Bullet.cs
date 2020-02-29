using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private float speed = 10f;
    private Vector3 velocity;
    private int damage;

    Rigidbody rb;
    
    void Start()
    {
        velocity = transform.forward * speed;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(velocity);
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }
}
