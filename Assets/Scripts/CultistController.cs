using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CultistController : MonoBehaviour {
    /// A.I. State
    enum State {
        idle,
        patrol,
        stunned,
        attack,
        chase,
        alerted
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

#region Patrol Variables
    [SerializeField] float patrolSpeed = 2f;
    [HideInInspector] public GameObject Waypoint;
    [HideInInspector] public List<Transform> waypoints;
    // these waypoints are the real waypoints from Local to World
    [SerializeField, HideInInspector] List<Vector3> convertedWaypoints;
    int currentWaypoint = 0;
    // Distance from the destination to go to the next waypoint
    float distThreshold = 0.1f;
#endregion 

#region Chase Variables
    [HideInInspector] public bool playerSighted;
    [SerializeField] float sightDistance = 6f;
    [SerializeField] float chaseSpeed = 3;
    [SerializeField, HideInInspector] SphereCollider sphereOfSight;
    [SerializeField] LayerMask rayMask;
#endregion

#region Attack Variables
    [SerializeField] float attackDistance;
    float attackerDistance;
    [SerializeField] float attackRate;
    private float attackTimer;
    [SerializeField] int attackDamage = 30;
    Vector3 knockbackDir;
    [SerializeField] float knockback = 8f;
#endregion

#region Stunned Variables
    [HideInInspector] public bool stunned;
    [SerializeField] private int stunnedDuration = 2;
    private float stunTimer;

#endregion

#region  Alterted Variables
    [SerializeField] float alertedSpeed = 2;
    [HideInInspector] public bool alerted;
    [HideInInspector] public Vector3 alertPosistion;

#endregion


    void Start () {
        //Find Components
        sphereOfSight = GetComponent<SphereCollider> ();
        sphereOfSight.radius = sightDistance;
        player = GameObject.FindGameObjectWithTag ("Player");
        playerStates = player.GetComponent<PlayerStates> ();
        agent = GetComponent<NavMeshAgent> ();
        // Intiaitate state
        currentState = State.idle;
        // Convert the Local positions of the waypoints to to world posistions
        for (int i = 0; i < waypoints.Count; i++) {
            convertedWaypoints.Add (waypoints[i].position);
        }
    }

    // Update is called once per frame
    void Update () {

    #region  Controls the different states 
        if (!playerSighted) {
            currentState = State.patrol;
        } else {
            currentState = State.chase;
        }
        if (stunned) {
            currentState = State.stunned;
        }
        if (alerted) {
            currentState = State.alerted;
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
            case State.patrol:
                Patrol ();
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
            case State.alerted:
                Alerted ();
                break;
            default:
                break;
        }
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
                    if (!playerStates.isHidden && playerStates.isLit) {
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
    /// Patrols along a path of waypoints
    void Patrol () {

        agent.speed = patrolSpeed;
        destination = convertedWaypoints[currentWaypoint];
        agent.SetDestination (destination);
        if (Vector3.Distance (transform.position, destination) <= distThreshold) {
            currentWaypoint++;
            if (currentWaypoint >= convertedWaypoints.Count) {
                currentWaypoint = 0;
            }
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
                player.GetComponent<Rigidbody>().AddForce(knockbackDir.normalized * knockback,ForceMode.Impulse);
                player.GetComponent<Health> ().TakeDamage (attackDamage);
                attackTimer = 0;
            }
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
    //// controls when the cultist is alerted by say a pile of leaves. works in conjunction with the Debri Prefab
    void Alerted () {
        destination = alertPosistion;
        agent.speed = alertedSpeed;
        agent.SetDestination (destination);
        if (Vector3.Distance(transform.position, alertPosistion)< 1f) {
            StartCoroutine(waitAfterAlerted());
        }
    }
    IEnumerator waitAfterAlerted(){
        yield return new WaitForSeconds(Random.Range(1,3));
        alerted = false;
    }
    ////// Inspector stuff////////////////
    //Adds waypoint for the Patrol state
    ////////////////////////////////////
    public void Addwaypoint () {
        GameObject go = Instantiate (Waypoint, this.transform.position, Quaternion.identity);
        go.transform.parent = transform;
        waypoints.Add (go.transform);
    }
}