using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoopManager : MonoBehaviour
{    
    //This script was created by Dan Urbanczyk

    //============================
    public bool alive; // this might be used later in enemy UI but not currently

    public Text hidingText;
    //=============================
    public bool isLit;
    public Text litText;
    [SerializeField]public Slider sliderHealth; // Gives script access to the Health slider
    [SerializeField]public Text healthCount; // This represents the number inside the Health bar
    [SerializeField]public Slider sliderOil; // Gives script access to the Oil slider
    [SerializeField]public Text oilCount; // This represents the number inside the Oil bar
    [SerializeField]public  Slider sliderStamina; // Gives script access to the Stamina slider
    [SerializeField]public Text staminaCount; // This represents the number inside the Stamina Bar
    CultistController cultistController;
    GhostController ghostController;
    // Update is called once per frame
    void Update()
    {
        //=====================OIL========================     
        GameObject playerOil = GameObject.Find("Player");
        Lantern lanternScript = playerOil.GetComponent<Lantern>();
        sliderOil.value = lanternScript.currentOil;
        oilCount.text = "" + lanternScript.currentOil.ToString("0");
        if(lanternScript.lantern == true){
            isLit = true;
            litText.text = "Lantern: ON";
        }
        else{
            litText.text = "Lantern: OFF";
            isLit = false;
        }
        //=================Health===================================
        GameObject playerHealth = GameObject.Find("Player");
        Health healthScript = playerHealth.GetComponent<Health>();
        sliderHealth.value = healthScript.currentHealth;
        healthCount.text = "" + healthScript.currentHealth.ToString("0");
        //======================Sprint=============================
        GameObject playerStamina = GameObject.Find("Player");
        Sprint sprintScript = playerStamina.GetComponent<Sprint>();
        sliderStamina.value = sprintScript.currentStamina;
        staminaCount.text = "" + sprintScript.currentStamina.ToString("0");
        //================================================================
        if(cultistController.playerSighted == true){
            hidingText.text = "Detected";
        }
        else{
            hidingText.text = "Hidden";
        }
        if(ghostController.playerSighted == true){
            hidingText.text ="Detected";
        }
        else{
            hidingText.text = "hidden";
        }
    }
}
