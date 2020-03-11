using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected int health;

    public void TakeDamage(int damageAmount)
    {
        //subtract damage amount
        health -= damageAmount;

        //Check if health has fallen below zero
        if (health <= 0)
        {
            //if health has fallen below zero, deactivate it 
            gameObject.SetActive(false);
        }
    }
}
