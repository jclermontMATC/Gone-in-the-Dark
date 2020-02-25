using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace GameManagement{
    public class PlayerStates : MonoBehaviour
{
    //This script was created by Dan Urbanczyk

    //============================
    public bool alive; // this might be used later in enemy UI but not currently
    public static bool isHidden = true;
    public Text hidingText;
    //=============================
    public static bool isLit;
    public Text litText;
    // ===========================
    [SerializeField]private Slider sliderHealth; // Gives script access to the Health slider
    [SerializeField]private Text healthCount; // This represents the number inside the Health bar
    [SerializeField]private Slider sliderOil; // Gives script access to the Oil slider
    [SerializeField]private Text oilCount; // This represents the number inside the Oil bar
    [SerializeField]private  Slider sliderStamina; // Gives script access to the Stamina slider
    [SerializeField]private Text staminaCount; // This represents the number inside the Stamina Bar

    // Update is called once per frame
    void Update()
    {

        //these are used to set a visible number for the UI bars
        sliderHealth.value  = Health.currentHealth;// HealthScript.currentHealth;
        sliderOil.value = Lantern.currentOil; 
        sliderStamina.value = Sprint.currentStamina;// SprintScript.currentStamina;
        healthCount.text = "" + Health.currentHealth.ToString("0");// HealthScript.currentHealth;
        oilCount.text = "" + Lantern.currentOil.ToString("0");
        staminaCount.text = "" + Sprint.currentStamina.ToString("0");
        //===================================================
        if (Lantern.lantern == true){
            isLit = true;
            litText.text = "Lantern: ON";
        }
        else{
            litText.text = "Lantern: OFF";
            isLit = false;
        }
        if(isHidden == true){
            hidingText.text = "HIDDEN";
        }
        else{
            hidingText.text = "DETECTED";
        }
    }
    
}

}

