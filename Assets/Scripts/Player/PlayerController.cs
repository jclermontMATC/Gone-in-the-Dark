using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;    // giving the player a character controller

    [SerializeField] private float speed = 5; //player move speed

    private Vector3 moveDirection = Vector3.zero;   // creating the Vector 3 for movement (x,y,z points)

    void Start() { characterController = GetComponent<CharacterController>();   // adding the CharacterController component
    }

    void Update() { moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));  // moving the character in directions
        moveDirection *= speed; // adding player move speed to moving
        moveDirection = Vector3.ClampMagnitude(moveDirection, speed);  //for diagnanol speeds

        characterController.Move(moveDirection * Time.deltaTime);   // adding Time.deltaTime
    }
}

