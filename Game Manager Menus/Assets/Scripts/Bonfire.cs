using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bonfire : MonoBehaviour
{    
    public void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        SaveSystem.SavePlayer(player);
    }
}
