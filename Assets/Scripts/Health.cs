using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{   [SerializeField] private int maxHealth = 100;
    [SerializeField] public int currentHealth;
    public bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount){
        currentHealth -=amount;
        if(currentHealth <= 0){
            isDead = true;
        }
    }
}
