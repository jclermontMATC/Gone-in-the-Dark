using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertCultist : MonoBehaviour {
    [SerializeField] bool altertable = true;
    [SerializeField] float alertRadius = 10f;
    [SerializeField] LayerMask alertLayer;

    void OnTriggerEnter (Collider other) {

        if (!altertable || other.tag != "Player") { return; }

        Collider[] hits = Physics.OverlapSphere (transform.position, alertRadius, alertLayer);
        foreach (Collider hit in hits) {
            CultistController controller = hit.gameObject.GetComponent<CultistController> ();
            if (controller != null) {

                controller.alerted = true;
                controller.alertPosistion = this.transform.position;
            }
        }
    }
}
