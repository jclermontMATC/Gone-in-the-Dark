using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float AttackRate = 1.0f;

    [SerializeField] float attackTimer;

    public GameObject AttackRight;
    public GameObject AttackLeft;
    public GameObject AttackDown;
    public GameObject AttackUp;

    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.D) && attackTimer >= AttackRate)
        {
            animator.SetTrigger("AttackRight");
            Debug.Log("Attacked");
            attackTimer = 0f;
        }
        if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.A) && attackTimer >= AttackRate)
        {
            animator.SetTrigger("AttackLeft");
            Debug.Log("Attacked");
            attackTimer = 0f;
        }
        if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.W) && attackTimer >= AttackRate)
        {
            animator.SetTrigger("AttackUp");
            Debug.Log("Attacked");
            attackTimer = 0f;
        }
        if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.S) && attackTimer >= AttackRate)
        {
            animator.SetTrigger("AttackDown");
            Debug.Log("Attacked");
            attackTimer = 0f;
        }

    }
}
