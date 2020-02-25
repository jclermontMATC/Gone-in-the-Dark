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
    //these don"t do anything yet, they are here mostly to help with formatting as of now
    //private bool livingStatus;  Unsure if this will need to be in this script
    public static bool isHidden = true;
    public Text hidingText;
    //=============================
    public static bool isLit;
    public Text litText;

    public Slider sliderHealth; // Gives script access to the Health slider
    public Text healthCount; // This represents the number inside the Health bar
    public Slider sliderOil; // Gives script access to the Oil slider
    public Text oilCount; // This represents the number inside the Oil bar
    public  Slider sliderStamina; // Gives script access to the Stamina slider
    public Text staminaCount; // This represents the number inside the Stamina Bar
    /* placeholder*/ private float stamina;


    void Start(){
        Lantern.currentOil = 100;
    }

    // Update is called once per frame
    void Update()
    {

        //these are used to set a visible number for the UI bars
        sliderHealth.value  = PlayerHealthTest.currentHealth;// HealthScript.currentHealth;
        sliderOil.value = Lantern.currentOil;
        sliderStamina.value = stamina;// SprintScript.currentStamina;
        healthCount.text = "" + PlayerHealthTest.currentHealth;// HealthScript.currentHealth;
        oilCount.text = "" + Lantern.currentOil.ToString("0");
        staminaCount.text = "" + stamina.ToString("0");

        //===================================================
        if(Input.GetKey(KeyCode.LeftShift)){
            sliderStamina.value = sliderStamina.value - 1;
        }
        else if(stamina <= 100){
            sliderStamina.value++;
        }
        //===================================================
        if (Lantern.lantern == true){
            isLit = true;
            litText.text = "Lantern: ON";
        }
        else{
            litText.text = "Lantern: OFF";
            isLit = false;
        }
        //
        if(isHidden == true){
            hidingText.text = "HIDDEN";
        }
        else{
            hidingText.text = "DETECTED";
        }
    }
    
}

}

