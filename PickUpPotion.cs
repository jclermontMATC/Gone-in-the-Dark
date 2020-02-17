using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPotion : MonoBehaviour {
    //heals player 25
    //oil 25
    [SerializeField] private int potionHealth = 25;
    [SerializeField] private int potionOil = 25;

    void OnTriggerEnter(Collider other) {
        //detects pick up
        if (other.gameObject.CompareTag("Player")) {
           //destroy the pick up from hierarchy 
           Destroy(this.gameObject);
		}
	}
}
