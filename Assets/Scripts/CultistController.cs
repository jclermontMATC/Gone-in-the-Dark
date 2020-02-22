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
     public List<Transform> waypoints;
    [SerializeField, HideInInspector] List<Vector3> convertedWaypoints;
    int currentWaypoint = 0;
    [SerializeField, HideInInspector] Vector3 destination;
    [SerializeField] float sight = 6f;
    [SerializeField] float patrolSpeed = 2f;
    [SerializeField] float chaseSpeed = 3;
    float distThreshold = 0.1f;
    [SerializeField,HideInInspector] SphereCollider sphereOfSight;
    [SerializeField] float attackDistance;
    [SerializeField] float attackSpeed;
    [SerializeField] int attackDamage = 30;

    public bool stunned;
    public bool playerSighted;

    public bool alerted;
    Vector3 alertPosistion;
   [SerializeField,HideInInspector]  LayerMask playerMask;

    void Start () {
        //Find Components
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

        //Methods base on current state
        switch (currentState) {
            case State.patrol:
                Patrol ();
                break;
            case State.chase:
                Chase ();
                break;
            default:
                break;
        }
    }

    void OnTriggerStay (Collider other) {
        if (other.CompareTag ("Player")) {
            Vector3 dir = (other.transform.position - transform.position).normalized;
            Debug.DrawLine (transform.position, transform.position + dir * sight, Color.red, .1f);
            RaycastHit hit;
            if (Physics.Raycast (transform.position, dir, out hit, playerMask)) {
                //Debug.Log(hit.collider.name);
                if (hit.collider.CompareTag ("Player")) {
                    if (!playerStates.isHidden && playerStates.isLit) {
                        playerSighted = true;
                    } else {
                        playerSighted = false;
                    }
                }
            }
        }
    }

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

    void Chase () {
        agent.speed = chaseSpeed;
        destination = player.transform.position;
        agent.SetDestination (destination);
    }

    public void Addwaypoint () {
        GameObject go = Instantiate (Waypoint, this.transform.position, Quaternion.identity);
        go.transform.parent = transform;
        waypoints.Add (go.transform);
    }
}