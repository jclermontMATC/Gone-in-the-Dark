using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueController : MonoBehaviour
{
    [SerializeField] private string[] combination = new string[3];
    [SerializeField]private string[] statue_order;
    public bool[] statuestateon;

    public int j;

    private int n;

    // Start is called before the first frame update
    void Start()
    {
        statuestateon = new bool[3];
        statuestateon[0] = false;
        statuestateon[1] = false;
        statuestateon[2] = false;
        statue_order = new string[3];
        j = 0;

        n = Random.Range(0, 6);
        PlayerPrefs.SetInt("Riddle", n);

        if (PlayerPrefs.GetInt("Riddle", 0) == 1) {
            combination[0] = "Statue";
            combination[1] = "Statue 2";
            combination[2] = "Statue 3";
        } else if (PlayerPrefs.GetInt("Riddle", 0) == 2) {
            combination[0] = "Statue";
            combination[1] = "Statue 3";
            combination[2] = "Statue 2";
        } else if (PlayerPrefs.GetInt("Riddle", 0) == 3) {
            combination[0] = "Statue 2";
            combination[1] = "Statue";
            combination[2] = "Statue 3";
        } else if (PlayerPrefs.GetInt("Riddle", 0) == 4) {
            combination[0] = "Statue 2";
            combination[1] = "Statue 3";
            combination[2] = "Statue";
        } else if (PlayerPrefs.GetInt("Riddle", 0) == 5) {
            combination[0] = "Statue 3";
            combination[1] = "Statue";
            combination[2] = "Statue 2";
        } else if (PlayerPrefs.GetInt("Riddle", 0) == 6) {
            combination[0] = "Statue 3";
            combination[1] = "Statue 2";
            combination[2] = "Statue";
        }

    }

    private void Update()
    {
        //when all statue states are turn on, will issue a check to see if combination and input statue are the same. 
        if (statuestateon[0] == true && statuestateon[1] == true && statuestateon[2] == true)
        {
            CombinationCheck();
        }

    }

    public void CombinationCheck()//Checks to see if arrays match
    {
        if (combination[0] == statue_order[0] && combination[1] == statue_order[1] && combination[2] == statue_order[2]) // if array elements match the end of the level opens up
        {
            Debug.Log("Open");
            GameObject.Find("Statue").GetComponent<Statue>().Match();
            GameObject.Find("Statue 2").GetComponent<Statue>().Match();
            GameObject.Find("Statue 3").GetComponent<Statue>().Match();
        }
        else{ //reset statue states and clears statue order array
            statuestateon[0] = false;
            statuestateon[1] = false;
            statuestateon[2] = false;
            statue_order[0] = null;
            statue_order[1] = null;
            statue_order[2] = null;
            statue_order = null ;//clears input combination;
            GameObject.Find("Statue").GetComponent<Statue>().ReverseState();
            GameObject.Find("Statue 2").GetComponent<Statue>().ReverseState();
            GameObject.Find("Statue 3").GetComponent<Statue>().ReverseState();
            j = 0;
            statue_order = new string[3];
        }

    }

    public void InputStatue(string _name)//Input names into the statue_order array
    {

        if (statue_order[j] != null)
        {

            j++;
            statue_order[j] = _name;
            
        }

        else
        {
            statue_order[j] = _name;
        }
        
        Debug.Log(statue_order[j]);
    }

    public void StatueStates(bool currentState)//changes statue state to on
    {
        if (statuestateon != null)
        {

            statuestateon[j] = currentState;
            
        }
        else
        {
            statuestateon[j] = currentState;
        }
    }
}
