using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CultistController : MonoBehaviour {
    enum State {
        idle,
        patrol,
        stunned,
        attack,
        chase
    }

    [SerializeField]
    State currentState;
    GameObject player;
    PlayerStates playerStates;
    [HideInInspector] public GameObject Waypoint;
    NavMeshAgent agent;
    [HideInInspector] public List<Transform> waypoints;
    [SerializeField, HideInInspector] List<Vector3> convertedWaypoints;
    int currentWaypoint = 0;
    [SerializeField, HideInInspector] Vector3 destination;
    [SerializeField] float sight = 6f;
    [SerializeField] float patrolSpeed = 2f;
    [SerializeField] float chaseSpeed = 3;
    float distThreshold = 0.1f;
    [SerializeField, HideInInspector] SphereCollider sphereOfSight;
    [SerializeField] float attackDistance;
    public float attackerDistance;
    [SerializeField] float attackSpeed;
    [SerializeField] private float attackTimer;
    [SerializeField] int attackDamage = 30;
    public bool stunned;
    public bool playerSighted;
    public bool alerted;
    Vector3 alertPosistion;
    [SerializeField, HideInInspector] LayerMask rayMask;

    [SerializeField] private int stunnedTime = 2;
    private float stunTimer;
    void Start () {
        //Find Components
        sphereOfSight = GetComponent<SphereCollider> ();
        sphereOfSight.radius = sight;
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

        //While player is not sighted , state is patrol
        if (!playerSighted) {
            currentState = State.patrol;
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

            default:
                break;
        }
    }
    //// used to determin if the player is in sight range then if they are hidden or the latern is lit
    void OnTriggerStay (Collider other) {
        if (stunned) { return; }
        if (other.CompareTag ("Player")) {
            Vector3 dir = (other.transform.position - transform.position).normalized;
            Debug.DrawLine (transform.position, transform.position + dir * sight, Color.red, .1f);
            RaycastHit hit;
            if (Physics.Raycast (transform.position, dir, out hit, rayMask)) {
                //  Debug.Log (hit.collider.name);
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

    void Attack () {
        if (attackTimer > attackSpeed) {
            if (!stunned && playerSighted) {
                Debug.Log ("ATTACK!!");
                attackTimer = 0;
            }
        }
        if (Vector3.Distance (transform.position, player.transform.position) <= 1f) {
            agent.speed = 0;
        } else {
            agent.speed = chaseSpeed;
        }

    }
    void Stun () {
        playerSighted = false;
        agent.speed = 0;
        stunTimer += Time.deltaTime;
        if (stunTimer > stunnedTime) {
            stunned = false;
            stunTimer = 0;
        }

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