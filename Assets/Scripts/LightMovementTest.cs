using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovementTest : MonoBehaviour {
    void Update() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(x, 0, y) * Time.deltaTime);
    }
}
