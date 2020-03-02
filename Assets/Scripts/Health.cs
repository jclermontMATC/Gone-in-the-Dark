using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andy Xiong 2/11/20
public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] public float currentHealth;
    [SerializeField] bool isDead = false;

    // Start is called before the first frame update
    void Start(){
        currentHealth = maxHealth;
        Debug.Log("Current health is " + maxHealth);
    }

    private void Update(){
        
    }

    public void ChangeHealth(int health) {
        currentHealth = Mathf.Min(currentHealth + health, maxHealth);
        if (currentHealth <= 0 && !isDead)
        {
            Debug.Log("Health is " + currentHealth);
            Death();
        }
    }

    void Death(){
        isDead = true;
        Destroy(gameObject);
        }

    /*void OnCollsionEnter(){
        if (gameObject.tag == "Player"){
            currentHealth = currentHealth + 5;
        } else if (currentHealth == 0){
            currentHealth = currentHealth + 2;
        }

        Debug.Log("Destroying game Object");
        Destroy(this.gameObject);
    }*/
}

