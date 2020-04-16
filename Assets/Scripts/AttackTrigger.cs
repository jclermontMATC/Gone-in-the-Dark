using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andy Xiong
public class AttackTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Cultist")){
            other.GetComponent<CultistController>().stunned = true;
        }
    }
}
