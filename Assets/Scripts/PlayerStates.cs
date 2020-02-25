using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public bool isHidden;
    public bool isLit;
    public GameObject light;

    

    void Start()
    {
        isHidden = false;
        isLit= true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Q)){

            isHidden = !isHidden;
        }

        if(Input.GetKeyDown(KeyCode.E)){
            
           light.SetActive(!light.activeSelf);
            isLit = !isLit;
        }
    }
}
