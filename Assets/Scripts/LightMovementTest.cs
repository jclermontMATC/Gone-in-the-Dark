using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovementTest : MonoBehaviour {
    private Camera camera;

    void Start() {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void Print() {
        Debug.Log("Stepstone works");
    }

    void Update() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        RaycastHit hit;

        Ray lookAt = camera.ScreenPointToRay(Input.mousePosition);
        transform.position += new Vector3(x, 0, y) * Time.deltaTime;
        
        if (Physics.Raycast(lookAt, out hit, LayerMask.GetMask("Ground"))) {
            Vector3 finalPoint = hit.point;
            finalPoint.y = transform.position.y;

            transform.LookAt(finalPoint);
        }
    }
}
