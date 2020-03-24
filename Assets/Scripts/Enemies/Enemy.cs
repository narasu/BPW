using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected int health;
    [SerializeField] protected FieldOfView fov;

    protected virtual void Update()
    {
        if (fov.TargetSpotted())
        {
            Attack();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        //subtract damage amount
        health -= damageAmount;

        //Check if health has fallen below zero
        if (health <= 0)
        {
            //if health has fallen below zero, deactivate
            gameObject.SetActive(false);
        }
    }

    protected virtual void Attack()
    {
        Debug.Log("attacking");
    }
}
