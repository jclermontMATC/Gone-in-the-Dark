using UnityEngine;
using UnityEngine.Events;

// Require that some sort of Collider is attached to the object
[RequireComponent(typeof(Collider))]
public class StepStone : MonoBehaviour {
    // Unity Event provides a simple way to link other objects to the stepstone without explicitly defining the connections in code.
    public UnityEvent OnStep; // This is public so it can be accessed from other scripts if needed.

    private void OnTriggerEnter(Collider other) { // Check for when something enters the step stone's collision box.
        OnStep.Invoke(); // Invoke the event, triggering whatever is linked in the Inspector tab.

        transform.Translate(new Vector3(0, -0.01f, 0));
    }

    private void OnTriggerExit() {
        transform.Translate(new Vector3(0, 0.01f, 0));
    }
}
