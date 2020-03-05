using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IDamageable
{
    [SerializeField] protected int currentHealth;
    
    public void TakeDamage(int damageAmount)
    {
        //subtract damage amount
        currentHealth -= damageAmount;

        //Check if health has fallen below zero
        if (currentHealth <= 0) 
        {
            //if health has fallen below zero, deactivate it 
            gameObject.SetActive (false);
        }
    }
}
