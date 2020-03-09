using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andy Xiong 2/17/20
public class Sprint : MonoBehaviour
{
    [SerializeField] public float maxStamina = 100;
    [SerializeField] public float currentStamina;
    [SerializeField] private float regenStamina = 1f;
    [SerializeField] private int speed = 5;

    // Update is called once per frame
    void Start(){
        currentStamina = maxStamina;
    }
    void Update(){
        if (Input.GetKey(KeyCode.LeftShift)){
           speed = speed + 2;
           currentStamina--;
        } else if (currentStamina < maxStamina){
            Regenerate();
        }
        currentStamina = Mathf.Max(currentStamina, 0f);
    }
    void Regenerate(){
        currentStamina = Mathf.Min(currentStamina + regenStamina/50, maxStamina);
    }
}