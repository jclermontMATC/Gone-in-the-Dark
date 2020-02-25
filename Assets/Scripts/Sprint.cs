﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andy Xiong 2/17/20
public class Sprint : MonoBehaviour
{
    [SerializeField] public float maxStamina = 100;
    [SerializeField] private float currentStamina;
    [SerializeField] private float regenStamina = 5;
    [SerializeField] private int speed = 5;

    // Update is called once per frame
    void Start(){
        currentStamina = maxStamina;
    }
    void Update(){
        if (Input.GetKeyDown("left shift")){
           speed = speed + 2;
        }

        if (currentStamina < maxStamina){
            Regenerate();
        }
    }
    void Regenerate(){
        currentStamina = Mathf.Min(currentStamina + regenStamina, maxStamina);
    }
}