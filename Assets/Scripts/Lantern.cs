using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class Lantern : MonoBehaviour 
{ 
    // Created by Mike Lecus 
 
 
    public bool lantern = false;   // lantern on or off
    [SerializeField] public float currentOil;      // oil the player has currently 
    [SerializeField] private float maxOil = 100f;   // max oil of lantern 
    [SerializeField] private float minOil = 0f;     // minimum amount of oil 

    [SerializeField] private float setOil = 1f;     // the set amount of oil being used when lantern is on

    private PlayerStates playerStates; //Reference to the PlayerStates component

    // Start is called before the first frame update 
    void Start() {
        playerStates = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStates>();
        currentOil = maxOil;    // sets the lantern to full at start 
    }

    public void AddOil(int points) {
        currentOil = Mathf.Min(currentOil + points, maxOil);
    }

    // Update is called once per frame 
    void Update() {
 
        if (lantern == true && currentOil > 0) {    // if player has oil and uses the lantern, the oil fuel starts to be used 
            currentOil =  Mathf.Max(currentOil - (setOil * Time.deltaTime), 0);
        } 
 
        if (currentOil <= 0) {  // when the player runs out of oil the lantern turns off 
            lantern = false;
            playerStates.SetLanternState(false);
        }
    }

    public void SetState(bool state) {
        lantern = state;
    }
} 
