using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//////////////////////////////////
/// Script by Brian Dornbusch ///
////////// 2/27/2020 ////////////
////////////////////////////////
public class PlayerStates : MonoBehaviour {
    public bool isHidden;
    public bool isLit;
    public int speed = 3;

    public GameObject light;
    public Vector3 mousePos;
    public LayerMask lightInteractive;
    public LayerMask mouseRaylayer;



    void Start () {
        isHidden = false;
        isLit = true;
    }

    // Update is called once per frame
    void Update () {
        ///// Rotates light
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit mouseHit;
        if (Physics.Raycast (ray, out mouseHit, Mathf.Infinity, mouseRaylayer, QueryTriggerInteraction.Ignore)) {
            mousePos = mouseHit.point;
            mousePos.y = light.transform.position.y;
        }
        light.transform.LookAt (mousePos, Vector3.up);
        ////////

        // Determine which direction to rotate towards
       float x = Input.GetAxis ("Horizontal");
       float z = Input.GetAxis ("Vertical");

        transform.Translate (new Vector3 (x, 0, z) * Time.deltaTime * speed);

        if (Input.GetKeyDown (KeyCode.Q)) {

            isHidden = !isHidden;
        }

        if (Input.GetKeyDown (KeyCode.E)) {

            light.SetActive (!light.activeSelf);
            isLit = !isLit;
        }

        //// makes the Ghost go back to their start posistion while the light shines on them. 
        if (isLit) {
            float lightRange = light.GetComponent<Light> ().range;
            Debug.DrawRay (light.transform.position, light.transform.forward * lightRange, Color.blue, .1f);
            Collider[] hits = Physics.OverlapCapsule (light.transform.position + light.transform.forward, light.transform.position + light.transform.forward * (lightRange - 1), .55f, lightInteractive, QueryTriggerInteraction.Ignore);
            foreach (Collider hit in hits) {
                GhostController GC = hit.GetComponent<GhostController>();
                if(GC != null){
                    GC.HitBylight();
                    //GC.stunned = true;
                }
            }
        }
    }

}