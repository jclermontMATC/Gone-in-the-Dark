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
    NavMeshAgent agent;

    [SerializeField] 
    List<Transform> waypoints;
   [SerializeField] List<Vector3> convertedWaypoints;
    int currentWaypoint = 0;
    [SerializeField]Vector3 destination;
    float sight = 6f;
    float patrolSpeed = 2f;
    float chaseSpeed = 3;
    float distThreshold = 0.1f;

    float attackDistance;
    float attackSpeed;
    int attackDamage = 30;

    public bool stunned;
    bool playerSighted;

    public bool alerted;
    public Vector3 alertPosistion;

    void Start () {
        //Find Components
        player = GameObject.FindGameObjectWithTag ("Player");
        agent = GetComponent<NavMeshAgent> ();
        // Intiaitate state
        currentState = State.idle;
        // Convert the Local positions of the waypoints to to world posistions
        for (int i = 0; i < waypoints.Count; i++) {
            convertedWaypoints.Add(waypoints[i].position);
        }
    }

    // Update is called once per frame
    void Update () {
        //While player is not sighted , state is patrol
        if (!playerSighted)
        {
            currentState = State.patrol;
        }

        //Methods base on current state
        switch (currentState) {
            case State.patrol:
                Patrol ();
                break;
            case State.chase:
                Chase();
                break;
            default:
                break;
        }
    }

    void OnTriggerStay(Collider other) {
        
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

    void Chase(){


    }
}