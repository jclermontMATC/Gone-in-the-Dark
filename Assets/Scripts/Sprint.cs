using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andy Xiong 2/17/20
public class Sprint : MonoBehaviour
{
    [SerializeField] public float maxStamina = 100;
    [SerializeField] private float currentStamina;
    [SerializeField] private float regenStamina = 5;
    //[SerializeField] private int speed = 5;
    public PlayerController player;

    // Update is called once per frame
    void Start()
    {
        player = GetComponent<PlayerController>();
        currentStamina = maxStamina;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.speed = 5f;
            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            player.speed = 3f;
        }



            if (currentStamina < maxStamina)
        {
            Regenerate();
        }
    }
    void Regenerate()
    {
        currentStamina = Mathf.Min(currentStamina + regenStamina, maxStamina);
    }
}