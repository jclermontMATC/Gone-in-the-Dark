using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class Lantern : MonoBehaviour 
{ 
    // Created by Mike Lecus 
 
 
    public static bool lantern = false;   // lantern on or off
    [SerializeField] public static float currentOil;      // oil the player has currently 
    [SerializeField] private float maxOil = 100f;   // max oil of lantern 
    [SerializeField] private float minOil = 0f;     // minimum amount of oil 

    [SerializeField] private float setOil = 1f;     // the set amount of oil being used when lantern is on
     
 
    // Start is called before the first frame update 
    void Start() { 
        currentOil = maxOil;    // sets the lantern to full at start 
    } 
 
    // Update is called once per frame 
    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {  // "Q" on keyboard turns lantern on or off 
            Debug.Log("Pressed"); 
            lantern = !lantern;
        } 
 
        if (lantern == true && currentOil > 0) {    // if player has oil and uses the lantern, the oil fuel starts to be used 
            currentOil =  Mathf.Max(currentOil - (setOil * Time.deltaTime), 0);
        } 
 
        if (currentOil <= 0) {  // when the player runs out of oil the lantern turns off 
            lantern = false; 
        }
    } 
} 
