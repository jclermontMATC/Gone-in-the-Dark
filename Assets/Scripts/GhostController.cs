using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//////////////////////////////////
/// Script by Brian Dornbusch ///
////////// 2/27/2020 ////////////
////////////////////////////////
public class GhostController : MonoBehaviour {
    /// A.I. State
    enum State {
        idle,
        stunned,
        attack,
        chase
    }
    /// the current state of the cultist
    [SerializeField] State currentState;
    //Referancing the player object
    GameObject player;
    /// used for isHidden and isLit variables 
    PlayerStates playerStates;
    // referance to the NavMesh Agent variables
    NavMeshAgent agent;
    // for the various destinations throughout the A.I states
    [SerializeField, HideInInspector] Vector3 destination;
    [SerializeField] Animator animator;
    Vector3 prevPos;
    Vector3 move = Vector3.zero;



    #region  Idle Variables
    private Vector3 idlePos;
    [SerializeField] float speed = 1;
    #endregion
    #region Chase Variables
    // [HideInInspector] 
     public bool playerSighted;
    [SerializeField] float sightDistance = 6f;
    [SerializeField] float chaseSpeed = 3;
    [SerializeField, HideInInspector] SphereCollider sphereOfSight;
    [SerializeField] LayerMask rayMask;
    #endregion

    #region Attack Variables
    [SerializeField] float attackDistance;
    [HideInInspector] float attackerDistance;
    [SerializeField] float attackRate = 2;
    private float attackTimer;
    [SerializeField] int attackDamage = 30;
    [HideInInspector] public Vector3 knockbackDir;
    public float knockback =100f;
    #endregion

    #region Stunned Variables
    [HideInInspector] public bool stunned;
    [SerializeField] private int stunnedDuration = 2;
    private float stunTimer;

    #endregion

    void Start () {
        //Find Components
        sphereOfSight = GetComponent<SphereCollider> ();
        sphereOfSight.radius = sightDistance;
        player = GameObject.FindGameObjectWithTag ("Player");
        playerStates = player.GetComponent<PlayerStates> ();
        agent = GetComponent<NavMeshAgent> ();
        animator = GetComponentInChildren<Animator>();

        // Intiaitate state
        currentState = State.idle;
        idlePos = transform.position;
        // Convert the Local positions of the waypoints to to world posistions
    }

    // Update is called once per frame
    void Update () {

        move = transform.position - prevPos;
        animator.SetFloat("FloatX", move.x);
        animator.SetFloat("FloatZ", move.z);
        prevPos = transform.position;

        #region  Controls the different states 
        if (!playerSighted) {
            currentState = State.idle;
        } else {
            currentState = State.chase;
        }

        if (stunned) {
            currentState = State.stunned;
        }

        attackerDistance = Vector3.Distance (transform.position, player.transform.position);
        if (Vector3.Distance (transform.position, player.transform.position) <= attackDistance) {
            currentState = State.attack;
        }

        if (playerSighted) {
            attackTimer += Time.deltaTime;
        } else {
            attackTimer = 0;
        }

        //Methods base on current state
        switch (currentState) {
            case State.idle:
                idle ();
                break;
            case State.chase:
                Chase ();
                break;
            case State.attack:
                Attack ();
                break;
            case State.stunned:
                Stun ();
                break;
            default:
                break;
        }
    }

    void idle () {
        destination = idlePos;
        agent.speed = speed;
        agent.SetDestination (destination);
    }
    #endregion
    //// used to determin if the player is in sight range then if they are hidden or the latern is lit
    void OnTriggerStay (Collider other) {
        if (stunned) { return; }
        if (other.CompareTag ("Player")) {
            Vector3 dir = (other.transform.position - transform.position).normalized;
            Debug.DrawLine (transform.position, transform.position + dir * sightDistance, Color.red, .1f);
            RaycastHit hit;
            if (Physics.Raycast (transform.position, dir, out hit, rayMask)) {
                if (hit.collider.CompareTag ("Player")) {
                    if (!playerStates.isHidden) { // && !playerStates.isLit
                        playerSighted = true;
                    } else {
                        playerSighted = false;
                    }
                } else {
                    playerSighted = false;
                }
            }
        }
    }
    void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Player")) {
            playerSighted = false;
        }
    }
    /////chases down the player
    void Chase () {
        agent.speed = chaseSpeed;
        destination = player.transform.position;
        agent.SetDestination (destination);
    }
    //// Attack the player when its in range
    void Attack () {
        knockbackDir = player.transform.position - transform.position;
        knockbackDir.y = 0.2f;
        if (attackTimer > attackRate) {
            if (!stunned && playerSighted) {
                Debug.Log ("ATTACK");
                Vector3 attackDir = transform.position - player.transform.position;
                Debug.Log(attackDir);
                if(attackDir.z < -0.5)
                    animator.SetTrigger("AttackNorth");

                if (attackDir.z > 0.5)
                    animator.SetTrigger("AttackSouth");

                if (attackDir.x < -0.5)
                    animator.SetTrigger("AttackEast");

                if (attackDir.x > 0.5)
                    animator.SetTrigger("AttackWest");

                int attackHit = UnityEngine.Random.Range(0, 1);
                if (attackHit == 0) {

                    player.GetComponent<Rigidbody>().AddForce(knockbackDir * knockback,ForceMode.Impulse);
                player.GetComponent<Health> ().ChangeHealth (-attackDamage);

                }
                attackTimer = 0;
            }
        }
        if (Vector3.Distance (transform.position, player.transform.position) <= 1f) {
            agent.SetDestination (transform.position);
            agent.speed = 0;
        } else {
            agent.speed = chaseSpeed;
        }

    }
    /// stops the cultist when they are stunned. A simple Boolean "Stunned" activates this
    void Stun () {
        playerSighted = false;
        agent.speed = 0;
        stunTimer += Time.deltaTime;
        if (stunTimer > stunnedDuration) {
            stunned = false;
            stunTimer = 0;
        }
    }

    public void HitBylight(){
        currentState = State.idle;
        playerSighted = false;
        agent.SetDestination(idlePos);
    }

}