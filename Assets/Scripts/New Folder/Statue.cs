using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour, IInteract
{
    string _name;
    bool onState;
    public StatueController controller;

    // Start is called before the first frame update
    void Start()
    {
        _name = this.gameObject.name;//get the name of the gameobject this script is connected to
        onState = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()//changes color and state to show it's been interacted with
    {
        onState = true;
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        controller.InputStatue(_name);
        controller.StatueStates(onState);


    }

    public void ReverseState()//returns to default state
    {
        onState = false;
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;

    }

    public void Match()//changes color if combination is inputted correctly
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
    
}
