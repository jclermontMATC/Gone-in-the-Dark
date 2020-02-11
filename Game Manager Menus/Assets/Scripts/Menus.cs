using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [SerializeField] GameObject restart;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject returnMenu;
    [SerializeField] GameObject quitGame;
    bool isPaused = false;
    private void Update() {
        if (Input.GetButtonDown("Pause")) {
            if (!isPaused) {
                Time.timeScale = 0f;
                returnMenu.gameObject.SetActive(true);
                quitGame.gameObject.SetActive(true);
                pause.gameObject.SetActive(true);
                restart.gameObject.SetActive(true);
                isPaused = true;
            } else {
                Time.timeScale = 1f;
                returnMenu.gameObject.SetActive(false);
                quitGame.gameObject.SetActive(false);
                pause.gameObject.SetActive(false);
                restart.gameObject.SetActive(false);
                isPaused = false;
            }
        }
    }
    public void StartGame() {   //Loads level 1
        SceneManager.LoadScene(1); //Change 1 to Level 1 scene number
    }
    public void QuitGame() {    //Closes game
        Application.Quit();
    }
    public void ReturnMenu() {  //Returns to main menu
        SceneManager.LoadScene(0);    //Change 0 to Main Menu scene number
    }
    public void Credits() {     //Load Credits
        SceneManager.LoadScene(0);  //Change 0 to Credits scene number
    }
    public void Settings() {    //Load Ssettings
        SceneManager.LoadScene(2);  // Change 0 to Settings scene number
    }
}
