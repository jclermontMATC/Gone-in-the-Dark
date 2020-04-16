using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andy Xiong 2/17/20
public class Sprint : MonoBehaviour
{
    [SerializeField] public float maxStamina = 100;
    [SerializeField] public float currentStamina;
    [SerializeField] private float regenStamina = 1f;
    [SerializeField] private int speed = 3;

    // Update is called once per frame
    void Start(){
        currentStamina = maxStamina;
    }
    void Update(){
       if (Input.GetKey(KeyCode.LeftShift) && (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0f || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0f)) { 
           speed = 7;
           currentStamina = currentStamina - 0.1f;
        } else if (currentStamina < maxStamina){
            Regenerate();
            speed = 3;
        }
        currentStamina = Mathf.Max(currentStamina, 0f);
        
    


       float x = Input.GetAxis ("Horizontal");
       float z = Input.GetAxis ("Vertical");

        transform.Translate (new Vector3 (x, 0, z) * Time.deltaTime * speed);
    }
    void Regenerate(){
        currentStamina = Mathf.Min(currentStamina + regenStamina/50, maxStamina);
    }
}