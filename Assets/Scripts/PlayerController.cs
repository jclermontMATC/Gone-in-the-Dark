using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private CharacterController characterController;    // giving the player a character controller

    public float speed = 5; //player move speed

    private Vector3 moveDirection = Vector3.zero;   // creating the Vector 3 for movement (x,y,z points)

    private Vector3 facing = Vector3.zero;

    private Animator _animator;

    void Start() {
        _animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));  // moving the character in directions
        moveDirection *= speed; // adding player move speed to moving
        moveDirection = Vector3.ClampMagnitude(moveDirection, speed);  //for diagnanol speeds

        if (_animator == null) {
            Debug.Log("Hey");
            return;
        }

        if (moveDirection.magnitude > 0)
            facing = moveDirection;


        _animator.SetFloat("MovementMagnitude", moveDirection.magnitude / speed);

        Move(facing.x / speed, facing.z / speed);
        
    }

    private void Move(float x, float z)
    {
        _animator.SetFloat("X", x);
        _animator.SetFloat("Z", z);
    }
}

