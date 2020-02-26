using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour {
    public bool isHidden;
    public bool isLit;
    public GameObject light;
    public int speed = 3;
    public Vector3 leftRayDirection, rightRayDirection;
    float x, z;
    void Start () {
        isHidden = false;
        isLit = true;
    }

    // Update is called once per frame
    void Update () {

        x = Input.GetAxis ("Horizontal");
        z = Input.GetAxis ("Vertical");

        transform.Translate (new Vector3 (x, 0, z) * Time.deltaTime * speed);

        if (Input.GetKeyDown (KeyCode.Q)) {

            isHidden = !isHidden;
        }

        if (Input.GetKeyDown (KeyCode.E)) {

            light.SetActive (!light.activeSelf);
            isLit = !isLit;
        }
    }

    void OnDrawGizmosSelected () {

        float totalFOV = 45.0f;
        float rayRange = 10.0f;
        float halfFOV = totalFOV / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis (-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis (halfFOV, Vector3.up);
        if (z > 0) {
            leftRayDirection = leftRayRotation * transform.forward;
            rightRayDirection = rightRayRotation * transform.forward;
        } else if (z < 0) {
            leftRayDirection = leftRayRotation * -transform.forward;
            rightRayDirection = rightRayRotation * -transform.forward;
        }
        if (x > 0) {
            leftRayDirection = leftRayRotation * transform.right;
            rightRayDirection = rightRayRotation * transform.right;
        } else if(x < 0){
            leftRayDirection = leftRayRotation * -transform.right;
            rightRayDirection = rightRayRotation * -transform.right;
        }

        Gizmos.DrawRay (transform.position, leftRayDirection * rayRange);
        Gizmos.DrawRay (transform.position, rightRayDirection * rayRange);
    }
}